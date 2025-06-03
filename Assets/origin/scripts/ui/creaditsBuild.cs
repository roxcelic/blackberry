using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

public class creaditsBuild : MonoBehaviour {
    public Dictionary<string, string> credits = new Dictionary<string, string>{
        ["memebers"] = "roxie,connor,harrison,ieuan",
        ["level design"] = "roxie",
        ["character design"] = "roxie,harrison",
        ["decoritve models"] = "roxie,ieuan",
        ["enviroment models"] = "roxie",
        ["ui -- main menu"] = "roxie",
        ["ui -- level loading screen"] = "roxie",
        ["ui -- death screen"] = "roxie",
        ["command system -- base"] = "roxie",
        ["command system -- commands"] = "roxie",
        ["command system -- ui"] = "roxie",
        ["input system"] = "roxie",
        ["input system documentation"] = "roxie",
        ["power up -- icons"] = "ieuan",
        ["power up -- design"] = "ieuan,roxie",
        ["power up -- programming"] = "roxie",
        ["power up -- ui"] = "roxie",
        ["proedural generation"] = "roxie",
        ["combat design R"] = "roxie",
        ["combat design H"] = "harrison",
        ["main game -- programming"] = "roxie",
        ["main game -- character programming"] = "roxie,harrison",
        ["main game -- seed system"] = "roxie",
        ["menace -- design"] = "roxie,harrison",
        ["menace -- combat"] = "roxie,harrison",
        ["menace -- animations"] = "roxie"
    };

    public Dictionary<string, float> creditedPercent = new Dictionary<string, float>();
    public Dictionary<string, int> creditedCount = new Dictionary<string, int>();

    public string fulltext;

    public TMP_Text creditsBox;

    void Start() {
        foreach(string Key in credits.Keys){
            fulltext += $"\n {Key}: {credits[Key]}";
            foreach(string person in credits[Key].Split(",")){
                if (creditedCount.ContainsKey(person))
                    creditedCount[person]++;
                else 
                    creditedCount.Add(person, 1);
            }
        }

        float totalwork = 0;
        foreach (string person in creditedCount.Keys)
            totalwork += creditedCount[person];

        foreach (string person in creditedCount.Keys)
            creditedPercent.Add(person, (creditedCount[person] / totalwork) * 100);

        creditsBox = GetComponent<TMP_Text>();

        creditsBox.text = fulltext;

        foreach (string Key in creditedPercent.Keys)
            Debug.Log($"{Key}: {creditedPercent[Key]}%");
    }
}