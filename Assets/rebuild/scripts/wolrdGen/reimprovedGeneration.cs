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
    [SerializeField] public List<GameObject> decors = new List<GameObject>();
    [SerializeField] public List<GameObject> enemys = new List<GameObject>();
    [SerializeField] public GameObject the_watcher;

    [Header("typeData")]
    public float verticalOffset;
    public float scale;
    public float safeborder;
    public bool StartRoom;
    
    [SerializeField]
    public class roomConfig {
        public static GameObject floor;
        public static GameObject enemys;
        public static GameObject player;
        public static GameObject the_watcher;

        public static List<GameObject> Decorations;
        public static List<GameObject> enemies;
    }

    [Header("player")]
    public GameObject[] player;
    
    void Start() {
        // get floor and seed
        seed = PlayerPrefs.HasKey("seed") ? PlayerPrefs.GetString("seed") : seed;
        floor = PlayerPrefs.HasKey("floor") ? PlayerPrefs.GetInt("floor") : floor;

        // fix seed
        seed = generation.seed.Check(seed);
        trueSeed = generation.seed.Convert(seed);

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

        if (name == "") name = ChosenObject.name;
        ChosenObject.name = name;

        return ChosenObject;
    }

    public GameObject returnObject(List<GameObject> objectsToSpawn, int choice = 0){
        long trueseedroom = getTrueSeed();
        
        int indexing = generation.utils.randomIndex(objectsToSpawn.Count, choice, trueseedroom);

        return objectsToSpawn[indexing];
    }

    public void SpawnRandom(List<GameObject> beans, string name = "") {
        string stringseed = getTrueSeed().ToString();

        int index = (int)generation.utils.charToFloat(stringseed[1]);
        float val = generation.utils.charToFloat(stringseed[index]) + generation.utils.charToFloat(stringseed[generation.utils.opositeIndex(index, trueSeed)]);

        int loop = (int)val/2;
    
        while (loop >= 0) {
            GameObject newBean = SpawnObject(beans, loop);

            Bounds bounds = roomConfig.floor.GetComponent<Collider>().bounds;
                bounds.min *= scale;
                bounds.max *= scale;

            newBean.transform.position = new Vector3(
                UnityEngine.Random.Range(bounds.min.x + safeborder, bounds.max.x - safeborder),
                verticalOffset + newBean.transform.position.y,
                UnityEngine.Random.Range(bounds.min.z + safeborder, bounds.max.z - safeborder)
            );

            Debug.Log(bounds);
            Debug.Log(newBean.transform.position);

            newBean.name = $"{name}-{loop}-{newBean.name}";

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

        // spawn floor
        roomConfig.floor = SpawnObject(floors, 1, new Vector3(90f, 0f, 0f), new Vector3(), "floor");

        // spawn player
            if (StartRoom){
                roomConfig.player = Instantiate(player[PlayerPrefs.GetInt("playerType", 0)], transform);
                roomConfig.player.transform.position = (roomConfig.floor.transform.position + new Vector3(0, 1, 0));

                transform.parent.GetComponent<bestWorldGen>().Player = roomConfig.player;
            }

        // spawn Decor
        SpawnRandom(decors, "decor");
        SpawnRandom(enemys, "enemy");

        // spawn the watcher
        roomConfig.the_watcher = Instantiate(the_watcher, transform);
        roomConfig.the_watcher.name = "the watcher";

        // finish

        if (roomConfig.player)
            roomConfig.player.SetActive(true);

        generated = true;
    }

    // a function to get the true seed
    public long getTrueSeed(){
        long trueseedfloor = generation.seed.incrementFloor(trueSeed, floor);
        long trueseedroom = generation.seed.incrementRoom(trueseedfloor, room);

        return trueseedroom;
    }
}
