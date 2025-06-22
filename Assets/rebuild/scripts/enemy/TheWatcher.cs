using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

public class TheWatcher : MonoBehaviour {
    GameObject world;

    void Start() {
        world = transform.parent.parent.gameObject;

        StartCoroutine(watfornomobs());
    }

    IEnumerator watfornomobs() {
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("enemy").Length == 0);
        world.GetComponent<bestWorldGen>().Continue = true;
        GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerController>().room++;
    }

}
