using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections;
using System.Collections.Generic;

public class PowerUpIcons : MonoBehaviour {
    public List<string> iconNames = new List<string>();
    public List<Sprite> iconIcons = new List<Sprite>();

    public Dictionary<string, Sprite> icons() {
        Dictionary<string, Sprite> iconsTMP = new Dictionary<string, Sprite>();

        int count = 0;
        foreach (string name in iconNames) {
            iconsTMP.Add(name, iconIcons[count]);
            count++;
        }

        return iconsTMP;
    }
}