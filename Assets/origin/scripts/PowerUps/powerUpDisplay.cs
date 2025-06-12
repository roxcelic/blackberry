using UnityEngine;
using UnityEngine.UI;

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class powerUpDisplay : MonoBehaviour {
    [Header("config")]
    public int maxItems;
    public Vector3 itemOffset;
    public Vector3 rowOffset;
    public Vector3 iconDimensions;

    [Header("components")]
    public List<GameObject> Display;
    public PowerUpIcons iconsHold;

    [Header("parts")]
    public List<string> powerups;

    void Start() {
        powerups = PlayerPrefs.GetString("powerups").Split(",").ToList();

        while (powerups.Contains("")) powerups.Remove("");

        foreach (string item in powerups)
            Debug.Log($"active powerups: {item}");

        foreach (GameObject item in Display) {
            if (powerups.Count -1 >= 0) {
                if(iconsHold.icons().ContainsKey(powerups[0])) item.GetComponent<Image>().sprite = iconsHold.icons()[powerups[0]];
                powerups = powerups.Skip(1).ToList();
            } else {
                item.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            }
        }
    }
}