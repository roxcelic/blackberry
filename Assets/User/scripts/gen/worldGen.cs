using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

public class worldGen : MonoBehaviour {
    public GameObject room;
    public bool experement;
    public GameObject loadingMenu;
    public GameObject KillBox;
    public Vector3 worldTransform;
    public List<GameObject> rooms;
    public int roomcount = 1;
    
    private int count = 0;

    private ImprovedGeneration vroomvroom;
    
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

        foreach (GameObject room in rooms){
            room.transform.position = worldTransform;
            worldTransform -= new Vector3(-25f, 0f, 0f);
            count++;
        }

        yield return new WaitForSeconds(1f);

        KillBox.transform.localScale = new Vector3(KillBox.transform.localScale.x + (Mathf.Abs(worldTransform.x) / 50), KillBox.transform.localScale.y, KillBox.transform.localScale.z);
        KillBox.transform.position = new Vector3(KillBox.transform.position.x + (Mathf.Abs(worldTransform.x) / 2), KillBox.transform.position.y, KillBox.transform.position.z);

        loadingMenu.GetComponent<Animator>().Play("loadingScreenExit");
        
        foreach (Transform child in KillBox.transform)
            child.GetComponent<KillBox>().started = true;

        PlayerPrefs.SetString("generated", "true");
    }
}
