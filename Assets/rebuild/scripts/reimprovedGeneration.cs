using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

public class reimprovedGeneration : MonoBehaviour {
    [Header("Data")]
    public bool start;
    public bool generated;
    public int WorldType;

    [Header("Seed")]
    public string seed = "ffffff";
    private long trueSeed = new long();
    public int floor;
    public int room;

    [Header("MeshData")]
    [SerializeField] public List<GameObject> floors = new List<GameObject>();
    [SerializeField] public List<GameObject> wallsansdoor = new List<GameObject>();
    [SerializeField] public List<GameObject> decors = new List<GameObject>();
    [SerializeField] public List<GameObject> enemys = new List<GameObject>();
    [SerializeField] public GameObject EnemySpawner;

    [Header("typeData")]
    public float width;
    public float height;
    public float scale;
    public float worldscale;
    public float safeborder;

    [Header("config")]
    public bool StartRoom;
    public bool EndRoom;
    
    [SerializeField]
    public class roomConfig {
        public static GameObject floor;
        public static GameObject enemys;
        public static GameObject player;
        public static GameObject roof;

        [SerializeField]
        public static GameObject West;

        public static List<GameObject> Decorations;
        public static List<GameObject> enemies;
    }

    public GameObject gate;

    [Header("player")]
    public GameObject[] player;
    
    [Header("alt")]
    public GameObject[] altPlayer;
    [SerializeField] public List<GameObject> altfloors = new List<GameObject>();
    [SerializeField] public List<GameObject> altroofs = new List<GameObject>();
    [SerializeField] public List<GameObject> altwallsansdoor = new List<GameObject>();
    [SerializeField] public List<GameObject> altdecors = new List<GameObject>();
    [SerializeField] public List<GameObject> altenemys = new List<GameObject>();


    void Start() {
        // get floor and seed
        seed = PlayerPrefs.HasKey("seed") ? PlayerPrefs.GetString("seed") : seed;
        floor = PlayerPrefs.HasKey("floor") ? PlayerPrefs.GetInt("floor") : floor;

        // fix seed
        seed = generation.seed.Check(seed);
        trueSeed = generation.seed.Convert(seed);

        // world type
        WorldType = PlayerPrefs.HasKey("worldType") ? PlayerPrefs.GetInt("worldType") : WorldType;
        if (WorldType == 1) {
            player = altPlayer;
            floors = altfloors;
            wallsansdoor = altwallsansdoor;
            decors = altdecors;
            enemys = altenemys;
        }
    
        // starting
        if (PlayerPrefs.GetInt("floor", 0) % 5 != 0 || PlayerPrefs.GetInt("floor", 0) == 0)
        StartCoroutine(startGen());
    }

    // a function to spawn a gameObject
    public GameObject SpawnObject(List<GameObject> objectsToSpawn, int choice = 0, Vector3 rotation = new Vector3(), Vector3 localPosition = new Vector3(), string name = ""){
        long trueseedroom = getTrueSeed();
        
        int indexing = generation.utils.randomIndex(objectsToSpawn.Count, choice, trueseedroom);
        
        GameObject ChosenObject = Instantiate(objectsToSpawn[indexing], objectsToSpawn[indexing].transform.position, objectsToSpawn[indexing].transform.rotation);
        if (rotation !=  Vector3.zero)
            ChosenObject.transform.eulerAngles = rotation;

        ChosenObject.transform.parent = transform; 
        ChosenObject.transform.localScale *= scale;
        if (localPosition != Vector3.zero)
            ChosenObject.transform.localPosition += localPosition;
        ChosenObject.name = name;

        return ChosenObject;
    }

    public GameObject SpawnSingleObject(GameObject objectsToSpawn, int choice = 0, Vector3 rotation = new Vector3(), Vector3 localPosition = new Vector3(), string name = ""){

        GameObject ChosenObject = Instantiate(objectsToSpawn, objectsToSpawn.transform.position, objectsToSpawn.transform.rotation);
        if (rotation !=  Vector3.zero)
            ChosenObject.transform.eulerAngles = rotation;

        ChosenObject.transform.parent = transform; 
        if (localPosition != Vector3.zero)
            ChosenObject.transform.localPosition += localPosition;
        ChosenObject.name = name;

        return ChosenObject;
    }

    public GameObject returnObject(List<GameObject> objectsToSpawn, int choice = 0){
        long trueseedroom = getTrueSeed();
        
        int indexing = generation.utils.randomIndex(objectsToSpawn.Count, choice, trueseedroom);

        return objectsToSpawn[indexing];
    }

    public void SpawnRandom(List<GameObject> beans) {
        string stringseed = getTrueSeed().ToString();

        int index = (int)generation.utils.charToFloat(stringseed[1]);
        float val = generation.utils.charToFloat(stringseed[index]) + generation.utils.charToFloat(stringseed[generation.utils.opositeIndex(index, trueSeed)]);

        int loop = (int)val/2;
    
        while (loop >= 0) {
            GameObject newBean = SpawnObject(beans, loop );

            Bounds bounds = roomConfig.floor.GetComponent<Collider>().bounds;
                bounds.min *= scale;
                bounds.max *= scale;

            newBean.transform.position = new Vector3(
                UnityEngine.Random.Range(bounds.min.x + safeborder, bounds.max.x - safeborder),
                height + newBean.transform.position.y,
                UnityEngine.Random.Range(bounds.min.y + safeborder, bounds.max.y - safeborder)
            );

            newBean.transform.eulerAngles = new Vector3(-90f, 0, UnityEngine.Random.Range(0, 360));

            loop--;
        }
    }

    public List<GameObject> ListRandom(List<GameObject> beans) {
        List<GameObject> randomObjects = new List<GameObject>();
        
        string stringseed = getTrueSeed().ToString();

        int index = (int)generation.utils.charToFloat(stringseed[1]);
        float val = generation.utils.charToFloat(stringseed[index]) + generation.utils.charToFloat(stringseed[generation.utils.opositeIndex(index, trueSeed)]);

        int loop = (int)val/2;
    
        while (loop >= 0) {
            randomObjects.Add(returnObject(beans, loop));
            
            loop--;
        }

        return randomObjects;
    }

    // where the world will be generated
    IEnumerator startGen() {
        // wait until can start generating
        yield return new WaitUntil(() => start);

        // initalise variables
        float tmpheight = height * scale;
        float tmpwidth = width * scale;

        // spawn floor
        roomConfig.floor = SpawnObject(floors, 1, new Vector3(90f, 0f, 0f), new Vector3(), "floor");

        // spawn walls
            if (StartRoom){
                roomConfig.player = Instantiate(player[PlayerPrefs.GetInt("playerType", 0)], transform);
                roomConfig.player.transform.position = (roomConfig.floor.transform.position + new Vector3(0, 1, 0));

                transform.parent.GetComponent<worldGen>().Player = roomConfig.player;
            } else {
                roomConfig.West = SpawnObject(wallsansdoor, 1, new Vector3(-90f, 0f, 0f), new Vector3(0, 0, 0), "west");
            } 

            gate = roomConfig.West;

        // spawn Decor
        SpawnRandom(decors);
        SpawnRandom(enemys);

        // finish

        if (roomConfig.player)
            roomConfig.player.SetActive(true);

        roomConfig.enemys.SetActive(true);
        roomConfig.enemys.GetComponent<EnemySpawn>().enemys = ListRandom(enemys);

        if (StartRoom)
            roomConfig.enemys.GetComponent<EnemySpawn>().Forceactivated = true;

        generated = true;
    }

    // a function to get the true seed
    public long getTrueSeed(){
        long trueseedfloor = generation.seed.incrementFloor(trueSeed, floor);
        long trueseedroom = generation.seed.incrementRoom(trueseedfloor, room);

        return trueseedroom;
    }
}
