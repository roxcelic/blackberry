using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

public class worldGen : MonoBehaviour {
    public GameObject room;
    public GameObject Player;
    public bool experement;
    public GameObject loadingMenu;
    public GameObject KillBox;
    public Vector3 worldTransform;
    public Vector3 worldRotation;
    public List<GameObject> rooms;
    public int roomcount = 1;
    
    private int count = 0;

    private ImprovedGeneration vroomvroom;

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
                ["Right"] = new List<Vector3> { new Vector3(0, 0, 0), new Vector3(25, 0, 0) },
                ["Forward"] = new List<Vector3> { new Vector3(0, -90, 0), new Vector3(0, 0, 25) },
            }
        },
        {
            new Dictionary<string, List<Vector3>> {
                ["Left"] = new List<Vector3> { new Vector3(0, 0, 0), new Vector3(-25, 0, 0) },
                ["Backwards"] = new List<Vector3> { new Vector3(0, 90, 0), new Vector3(0, 0, -25) },
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
        rooms.Add(SpawnRoom("end", false, true));

        StartCoroutine(startGen());
    }

    public GameObject SpawnRoom(string name = "", bool StartRoom = false, bool EndRoom = false){
        GameObject newRoom = Instantiate(room, transform);
        newRoom.transform.position = new Vector3(0, 0, 0);

        newRoom.GetComponent<ImprovedGeneration>().StartRoom = StartRoom;
        newRoom.GetComponent<ImprovedGeneration>().EndRoom = EndRoom;

        newRoom.GetComponent<ImprovedGeneration>().experement = experement;

        newRoom.name = name;

        return newRoom;
    }

    IEnumerator startGen() {
        count = 0;
        foreach (GameObject room in rooms) {
            room.GetComponent<ImprovedGeneration>().room = count;
            count++;
        }

        for(int i = 0, count = transform.childCount; i < count; i++) {
            vroomvroom = transform.GetChild((transform.childCount - 1) - i).GetComponent<ImprovedGeneration>();
            vroomvroom.start = true;
            yield return new WaitUntil(() => vroomvroom.generated);
        }

        count = 0;
        foreach (GameObject room in rooms){
            Dictionary<string, List<Vector3>> tmpPosition = worldPositioningDirections[generation.utils.randomIndex(worldPositioningDirections.Count, 4, getTrueSeed(count))];

            List<Vector3> directionData = tmpPosition[new List<string> (tmpPosition.Keys) [generation.utils.randomIndex(tmpPosition.Keys.Count, 4, getTrueSeed(count))]];

            while (usedPositions.Contains(directionData[1]) || usedPositions.Contains(worldTransform + directionData[1])) {
                directionData = tmpPosition[new List<string> (tmpPosition.Keys) [generation.utils.randomIndex(tmpPosition.Keys.Count, 4, getTrueSeed(count))]];
                count++;
            }

            room.transform.position = worldTransform;
            
            room.transform.rotation =  Quaternion.Euler(worldRotation);
            worldRotation = directionData[0];
            worldTransform += directionData[1];

            usedPositions.Add(directionData[0]);

            count++;
        }

        yield return new WaitForSeconds(1f);

        KillBox.transform.localScale = new Vector3(KillBox.transform.localScale.x + (Mathf.Abs(worldTransform.x) / 50), KillBox.transform.localScale.y, KillBox.transform.localScale.z + (Mathf.Abs(worldTransform.z) / 50));
        KillBox.transform.position = new Vector3(KillBox.transform.position.x + (Mathf.Abs(worldTransform.x) / 2), KillBox.transform.position.y, KillBox.transform.position.z + (Mathf.Abs(worldTransform.z) / 2));

        loadingMenu.GetComponent<Animator>().Play("loadingScreenExit");
        
        foreach (Transform child in KillBox.transform)
            child.GetComponent<KillBox>().started = true;

        PlayerPrefs.SetString("generated", "true");
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
