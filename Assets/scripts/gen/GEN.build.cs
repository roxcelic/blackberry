using System;
using System.Collections.Generic;

using UnityEngine;

using generation;
using eevee;

public class GENbuild : MonoBehaviour{
    [Header("Seed")]
    public string seed = "ffffff";
    private long trueSeed = new long();

    [Header("MeshData")]
    [SerializeField] public List<GameObject> floors = new List<GameObject>();
    [SerializeField] public List<GameObject> roofs = new List<GameObject>();
    [SerializeField] public List<GameObject> walls = new List<GameObject>();
    [SerializeField] public List<GameObject> wallsansdoor = new List<GameObject>();
    [SerializeField] public List<GameObject> decors = new List<GameObject>();

    [Header("TypeData")]
    public int width;
    public int height;
    public int scale;
    public float worldscale;
    public int safeborder;

    public int floor;
    public int room;

    public bool spawEnterencewall;
    public bool endRoom;

    void Start(){
        seed = PlayerPrefs.HasKey("seed") ? PlayerPrefs.GetString("seed") : seed;
        floor = PlayerPrefs.HasKey("floor") ? PlayerPrefs.GetInt("floor") : floor;

        // fix seed
        seed = generation.seed.Check(seed);
        trueSeed = generation.seed.Convert(seed);

        Load();
    }

    void Update() {
        if (eevee.input.Check("q"))
            reset();
    }

    public void reset() {
        // abortion
        foreach(Transform child in transform)
            Destroy(child.gameObject);

        Load();
    }


    public void Load(){
        int tmpheight = height * scale;
        int tmpwidth = width * scale;

        // floors yayyyy
        GameObject floor = SpawnObject(floors);

        // roof.
        GameObject roof = SpawnObject(roofs);
        roof.transform.position = new Vector3(roof.transform.position.x, roof.transform.position.y + (tmpheight / worldscale), roof.transform.position.z);

        // walls????????????
        SpawnObject(walls);
        GameObject secondwall = SpawnObject(walls, 1);
        secondwall.transform.position = new Vector3(secondwall.transform.position.x - (tmpwidth / worldscale), secondwall.transform.position.y, secondwall.transform.position.z);
    
        // walls with doors
        if (spawEnterencewall) {
            GameObject secondDoor = SpawnObject(walls, 1);
            secondDoor.transform.localRotation = Quaternion.Euler(new Vector3(secondDoor.transform.localRotation.x, secondDoor.transform.localRotation.y, 90));
        }
        
        GameObject sencondwallsansdoor;

        if (endRoom){
            sencondwallsansdoor = SpawnObject(walls);
            sencondwallsansdoor.transform.localRotation = Quaternion.Euler(new Vector3(sencondwallsansdoor.transform.localRotation.x, sencondwallsansdoor.transform.localRotation.y, -90));
        } else {
            sencondwallsansdoor = SpawnObject(wallsansdoor);
        }

        sencondwallsansdoor.transform.position = new Vector3(sencondwallsansdoor.transform.position.x, sencondwallsansdoor.transform.position.y, sencondwallsansdoor.transform.position.z - (width / worldscale));

        // spawn decor
        SpawnRandom(floor, decors);
    }

    public GameObject SpawnObject(List<GameObject> objects, int choice = 0) {
        long trueseedroom = getTrueSeed();

        int indexing = generation.utils.randomIndex(objects.Count, choice, trueseedroom) - 1;

        GameObject ChosenObject = Instantiate(objects[indexing], transform.position, objects[indexing].transform.rotation);
        ChosenObject.transform.parent = transform; 
        ChosenObject.transform.localScale *= scale;

        return ChosenObject;
    }

    public void SpawnRandom(GameObject floor, List<GameObject> objects){

        string stringseed = getTrueSeed().ToString();
        int index = (int)generation.utils.charToFloat(stringseed[1]);
        float val = generation.utils.charToFloat(stringseed[index]) + generation.utils.charToFloat(stringseed[generation.utils.opositeIndex(index, trueSeed)]);

        int loop = (int)val/2;

        while (loop >= 0) {
            GameObject newDecor = SpawnObject(objects, loop);
            Bounds bounds = floor.GetComponent<Collider>().bounds;
                bounds.min *= scale;
                bounds.max *= scale;


            bounds.min = new Vector3 (bounds.min.x - safeborder, newDecor.transform.position.y, bounds.min.z - floor.transform.position.z * (scale - 1) - safeborder);
            bounds.max = new Vector3 (bounds.max.x - safeborder, newDecor.transform.position.y, bounds.max.z - floor.transform.position.z * (scale - 1) - safeborder);

            newDecor.transform.position = new Vector3(
                UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
                0 + (bounds.max.y),
                UnityEngine.Random.Range(bounds.min.z, bounds.max.z)
            );

            newDecor.transform.localRotation = Quaternion.Euler(new Vector3(newDecor.transform.localRotation.x, newDecor.transform.localRotation.y, UnityEngine.Random.Range(0, 360)));

            loop--;
        }
    }

    public long getTrueSeed(){
        long trueseedfloor = generation.seed.incrementFloor(trueSeed, room);
        long trueseedroom = generation.seed.incrementRoom(trueseedfloor, floor);

        return trueseedroom;
    }
}