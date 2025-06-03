using UnityEngine;

using System;
using System.Collections;

public class LoadPrevSeed : MonoBehaviour {
    public GameObject seedHolder;
    public seedPicker expSample;
    public string seed;

    private int indexing;

    void Start() {
        seed = PlayerPrefs.GetString("seed", "______");
        seedHolder = transform.gameObject;
        expSample = seedHolder.transform.GetChild(0).GetComponent<seedPicker>();

        // Array.IndexOf(expSample.characterConversion, );
        indexing = 0;
        Debug.Log(seed);
        foreach (char seedItem in seed) {
            string tmpSeedItem = seedItem.ToString();
            if (tmpSeedItem == "_") 
                seedHolder.transform.GetChild(indexing).GetComponent<seedPicker>().Selection = 0;
            else
                seedHolder.transform.GetChild(indexing).GetComponent<seedPicker>().Selection = Array.IndexOf(expSample.characterConversion, tmpSeedItem) + 1;

            indexing++;
        }
    }
}