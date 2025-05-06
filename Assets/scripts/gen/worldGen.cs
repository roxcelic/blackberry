using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

public class worldGen : MonoBehaviour {
    public GameObject room;
    public Vector3 worldTransform;
    public List<GameObject> rooms;

    private ImprovedGeneration vroomvroom;
    
    void Start() {
        PlayerPrefs.SetString("generated", "false");

        // spawn start room
        rooms.Add(SpawnRoom(true));

        // spawn middle rooms
        for (int i = 1; i <= 3; i++)
            rooms.Add(SpawnRoom());

        // spawn end room
        rooms.Add(SpawnRoom(false, true));

        StartCoroutine(startGen());
    }

    public GameObject SpawnRoom(bool StartRoom = false, bool EndRoom = false){
        GameObject newRoom = Instantiate(room, transform);
        newRoom.transform.position = new Vector3(0, 0, 0);

        newRoom.GetComponent<ImprovedGeneration>().StartRoom = StartRoom;
        newRoom.GetComponent<ImprovedGeneration>().EndRoom = EndRoom;

        return newRoom;
    }

    IEnumerator startGen() {
        Debug.Log("generation start");

        for(int i = 0, count = transform.childCount; i < count; i++) {
            vroomvroom = transform.GetChild(i).GetComponent<ImprovedGeneration>();
            vroomvroom.start = true;
            yield return new WaitUntil(() => vroomvroom.generated);
        }

        foreach (GameObject room in rooms){
            room.transform.position = worldTransform;
            worldTransform -= new Vector3(-25f, 0f, 0f);
        }

        Debug.Log("generation finished");
        PlayerPrefs.SetString("generated", "true");
    }
}
