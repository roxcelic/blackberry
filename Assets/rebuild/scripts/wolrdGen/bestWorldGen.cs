using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

public class bestWorldGen : MonoBehaviour {
    public GameObject room;
    public GameObject Player;
    public GameObject loadingMenu;
    public GameObject KillBox;
    public Vector3 killBoxOffset;
    public Vector3 worldTransform;
    public Vector3 worldRotation;
    public List<GameObject> rooms;
    public int roomcount = 1;
    
    public bool Continue;

    private int count = 0;

    private reimprovedGeneration vroomvroom;

    [Header("tmpdata")]
    public worldPositioning test;
    
    public enum worldPositioning {
        Left,
        Right,
        Forward,
        Backwards
    };

    // directiontions
    public List<Dictionary<string, List<Vector3>>> worldPositioningDirections = new List<Dictionary<string, List<Vector3>>>{
        {
            new Dictionary<string, List<Vector3>> {
                ["Right"] = new List<Vector3> { new Vector3(0, 0, 0), new Vector3(16, 0, 0) },
                ["Forward"] = new List<Vector3> { new Vector3(0, -90, 0), new Vector3(0, 0, 16) },
            }
        }
    };


    public List<Vector3> usedPositions = new List<Vector3>();
    
    void Start() {
        roomcount += PlayerPrefs.GetInt("floor", 0) * 2;

        PlayerPrefs.SetString("generated", "false");

        // spawn start room
        rooms.Add(SpawnRoom("start", true));

        // spawn middle rooms
        for (int i = 1; i <= roomcount; i++)
            rooms.Add(SpawnRoom(i.ToString()));

        // spawn end room
        rooms.Add(SpawnRoom("end", false));

        StartCoroutine(startGen());
    }

    public GameObject SpawnRoom(string name = "", bool StartRoom = false){
        GameObject newRoom = Instantiate(room, transform);
        newRoom.transform.position = new Vector3(0, 0, 0);

        newRoom.GetComponent<reimprovedGeneration>().StartRoom = StartRoom;

        newRoom.name = name;

        return newRoom;
    }

    IEnumerator startGen() {
        count = 0;
        foreach (GameObject room in rooms) {
            room.GetComponent<reimprovedGeneration>().room = count;
            count++;
        }

        for(int i = 0, count = transform.childCount; i < count; i++) {
            vroomvroom = transform.GetChild((transform.childCount - 1) - i).GetComponent<reimprovedGeneration>();
        }

        count = 0;
        Dictionary<string, List<Vector3>> tmpPosition = worldPositioningDirections[generation.utils.randomIndex(worldPositioningDirections.Count, 4, getTrueSeed(count))];

        yield return new WaitForSeconds(1f);

        loadingMenu.GetComponent<Animator>().Play("loadingScreenExit");
        
        foreach (Transform child in KillBox.transform)
            child.GetComponent<KillBox>().started = true;

        PlayerPrefs.SetString("generated", "true");

        Continue = true;

        foreach (GameObject room in rooms) {
            yield return new WaitUntil(() => Continue);

            room.GetComponent<reimprovedGeneration>().start = true;
            Continue = false;

            yield return 0;

            // seperating ig idajsodijaosdjoaijsd

            List<Vector3> directionData = tmpPosition[new List<string> (tmpPosition.Keys) [generation.utils.randomIndex(tmpPosition.Keys.Count, 4, getTrueSeed(count))]];

            while (usedPositions.Contains(directionData[1]) || usedPositions.Contains(worldTransform + directionData[1])) {
                directionData = tmpPosition[new List<string> (tmpPosition.Keys) [generation.utils.randomIndex(tmpPosition.Keys.Count, 4, getTrueSeed(count))]];
                count++;
            }

            room.transform.position = new Vector3(worldTransform.x, room.GetComponent<roomRise>().StartValue, worldTransform.z);
            
            room.transform.rotation =  Quaternion.Euler(directionData[0]);
            worldTransform += directionData[1];

            usedPositions.Add(directionData[0]);

            count++;

            // update kill box size
            KillBox.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
            KillBox.transform.position += killBoxOffset;

            // rise
            room.GetComponent<roomRise>().start = true;
        } 
    }

    // a function to get the true seed
    public long getTrueSeed(int room){

        string seed = PlayerPrefs.HasKey("seed") ? PlayerPrefs.GetString("seed") : "";
        int floor = PlayerPrefs.HasKey("floor") ? PlayerPrefs.GetInt("floor") : 0;

        // fix seed
        seed = generation.seed.Check(seed);
        long trueSeed = generation.seed.Convert(seed);
        
        long trueseedfloor = generation.seed.incrementFloor(trueSeed, PlayerPrefs.GetInt("floor", 0));
        long trueseedroom = generation.seed.incrementRoom(trueseedfloor, room);

        return trueseedroom;
    }
}
