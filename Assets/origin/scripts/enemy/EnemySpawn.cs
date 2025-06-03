using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawn : MonoBehaviour {
    [SerializeField] public List<GameObject> enemys = new List<GameObject>();
    public Collider col;
    public bool activated = false;
    public bool Forceactivated = false;
    public int count = -1;
    public float safeborder = 5f;

    private GameObject world;

    void Start() {
        col = GetComponent<Collider>();

        if (Forceactivated && !activated)
            StartCoroutine(SpawnCreatures());

        world = transform.parent.parent.gameObject; 
    }

    void Update() {
        if (GameObject.FindGameObjectsWithTag("enemy").Length == 0 && activated){
            Collider[] hitColliders = Physics.OverlapBox(col.bounds.center, col.bounds.size / 2, Quaternion.identity, LayerMask.GetMask("Walls"));

            foreach (Collider hitCollider in hitColliders) {
                if (hitCollider.transform.parent.gameObject.GetComponent<DoorBlock>() != null) {
                    hitCollider.transform.parent.gameObject.GetComponent<DoorBlock>().RemoveBlockAid();
                }
            }

            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            if (players.Length != 0) {
                players[0].GetComponent<PlayerController>().room+=1;

                if (GetRoomCount() - 1< players[0].GetComponent<PlayerController>().room) {    
                    Time.timeScale = 1f;
                    GameObject loadingScreen = GameObject.Find("Canvas").transform.Find("powerups").gameObject;
                    loadingScreen.SetActive(true);
                } else {
                    Destroy(transform.gameObject);
                    transform.parent.parent.GetChild(transform.parent.GetSiblingIndex() + 1).Find("enemys").GetComponent<EnemySpawn>().DoTheRoar();
                }
            }
        }
    }

    public int GetRoomCount() {
        int basic = 0;

        foreach (Transform child in world.transform)
            basic++;

        return basic;
    }

    public void DoTheRoar() {
        StartCoroutine(SpawnCreatures());
    }

    public IEnumerator SpawnCreatures() {
        yield return new WaitForSeconds(1);

        foreach (GameObject bean in enemys){
            GameObject newBean = Instantiate(bean);
            newBean.transform.parent = transform;

            Bounds bounds = col.bounds;

            newBean.transform.position = Vector3.zero;

            newBean.transform.localPosition = new Vector3(
                0,
                1,
                0
            );

            Debug.Log($"spawn position = {newBean.transform.position}");
        }

        activated = true;
    }
}