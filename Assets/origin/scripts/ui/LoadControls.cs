using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

public class LoadControls : MonoBehaviour {
    public Dictionary<string, eevee.config> Controls;

    public GameObject keyboards;

    private Vector3 spawnObjectAt = new Vector3(0f, -3.5f, 0f);

    void Start() {
        Controls = eevee.Qlock.extractr();

        foreach (string key in Controls.Keys) {
            GameObject currentObject = Instantiate(keyboards, transform.parent);
            currentObject.GetComponent<loadInputConfig>().config = Controls[key];
            currentObject.transform.localPosition = spawnObjectAt;
            currentObject.SetActive(true);

            spawnObjectAt -= new Vector3(0f, 1.5f, 0f);
        }
    }

    void Update() {
        
    }
}
