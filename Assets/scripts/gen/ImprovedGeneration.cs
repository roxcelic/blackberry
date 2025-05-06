using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

public class ImprovedGeneration : MonoBehaviour {
    [Header("Data")]
    public bool start;
    public bool generated;

    [Header("Seed")]
    public string seed = "ffffff";
    private long trueSeed = new long();
    public int floor;
    public int room;

    [Header("MeshData")]
    [SerializeField] public List<GameObject> floors = new List<GameObject>();
    [SerializeField] public List<GameObject> roofs = new List<GameObject>();
    [SerializeField] public List<GameObject> walls = new List<GameObject>();
    [SerializeField] public List<GameObject> wallsansdoor = new List<GameObject>();
    [SerializeField] public List<GameObject> decors = new List<GameObject>();

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
        public static GameObject player;
        public static GameObject roof;

        [SerializeField]
        public static class walls {
            public static GameObject North;
            public static GameObject East;
            public static GameObject South;
            public static GameObject West;
        }

        public static List<GameObject> Decorations;
        public static List<GameObject> Enemys;
    }

    [Header("player")]
    public GameObject player;

    void Start() {
        // get floor and seed
        seed = PlayerPrefs.HasKey("seed") ? PlayerPrefs.GetString("seed") : seed;
        floor = PlayerPrefs.HasKey("floor") ? PlayerPrefs.GetInt("floor") : floor;

        // fix seed
        seed = generation.seed.Check(seed);
        trueSeed = generation.seed.Convert(seed);
    
        // starting
        StartCoroutine(startGen());
    }

    // a function to spawn a gameObject
    public GameObject SpawnObject(List<GameObject> objectsToSpawn, int choice = 0, Vector3 rotation = new Vector3(), Vector3 localPosition = new Vector3(), string name = ""){
        long trueseedroom = getTrueSeed();
        
        int indexing = generation.utils.randomIndex(objectsToSpawn.Count, choice, trueseedroom) - 1;

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
            roomConfig.walls.North = SpawnObject(walls, 1, new Vector3(90f, 0f, 90f), new Vector3(0, tmpheight, 0), "north");
            roomConfig.walls.South = SpawnObject(walls, 1, new Vector3(90f, 0f, 270f), new Vector3(0, tmpheight, 0), "south");

            // variable walls
            if (EndRoom)
                roomConfig.walls.East = SpawnObject(walls, 1, new Vector3(-90f, 0f, 180f), new Vector3(tmpwidth, 0, 0), "east");
            
            if (StartRoom){
                roomConfig.player = Instantiate(player, transform);
                roomConfig.player.transform.position = (roomConfig.floor.transform.position * -1);
                
                roomConfig.walls.West = SpawnObject(walls, 1, new Vector3(-90f, 0f, 0f), new Vector3(-tmpwidth, 0, 0), "west");
            } else {
                roomConfig.walls.West = SpawnObject(wallsansdoor, 1, new Vector3(-90f, 0f, 0f), new Vector3(0, 0, 0), "west");
            } 

        // spawn roof
        roomConfig.roof = SpawnObject(roofs, 1, new Vector3(90f, 0f, 0f), new Vector3(0, tmpheight, 0), "roof");
        
        // spawn Decor
        SpawnRandom(decors);

        // finish

        if (roomConfig.player)
            roomConfig.player.SetActive(true);

        generated = true;
    }

    // a function to get the true seed
    public long getTrueSeed(){
        long trueseedfloor = generation.seed.incrementFloor(trueSeed, room);
        long trueseedroom = generation.seed.incrementRoom(trueseedfloor, floor);

        return trueseedroom;
    }
}