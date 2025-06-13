using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

public class StatsMenuLoad : MonoBehaviour {
    public PlayerController pc;
    public TMP_Text text;

    void OnEnable() {
        Dictionary<string, eevee.config> Controls = eevee.Qlock.extractr();
        string endText = "";

        // player stats
        endText += addHeader("player stats");
        endText += addText("speed", pc.speed.ToString());
        endText += addText("baseAttackDamage", pc.baseAttackDamage.ToString());
        endText += addText("room", pc.room.ToString());
        endText += addText("defence", pc.defence.ToString());
        endText += addText("health", pc.health.ToString());
        endText += addText("maxHealth", pc.maxHealth.ToString());
        endText += addText("PlayerSpecificAttackDamage", pc.PlayerSpecificAttackDamage.ToString());
        
        // world stats
        endText += addHeader("world");
        endText += addText("speed", PlayerPrefs.GetString("seed"));
        endText += addText("floor", PlayerPrefs.GetInt("floor").ToString());
        endText += addText("current room", pc.room.ToString());
        endText += addText("total rooms", (5 + (PlayerPrefs.GetInt("floor") * 2)).ToString());
        endText += addText("menaces left", GameObject.FindGameObjectsWithTag("enemy").Length.ToString());
        endText += addText("background speed", PlayerPrefs.GetInt("skyboxspeed", 1).ToString());

        // power ups
        endText += addHeader("power ups");
        endText += addText("current powerups", string.Join(", ", pc.act));
        endText += addText("expanded powerups", PlayerPrefs.GetString("expandedPowerUp"));
        endText += addText("power up mode", PlayerPrefs.GetInt("powerupMode").ToString());

        // dev
        endText += addHeader("dev");
        endText += addText("dev settings", PlayerPrefs.GetString("dev"));

        // controls
        endText += addHeader("controls");
        

        foreach (string key in Controls.Keys)
            endText += addText(Controls[key].displayName, $"\n    Key: {(KeyCode)Controls[key].KEYBOARD_code}\n    Gamepad: {Controls[key].CONTROLLER_name}");

        text.text = endText;
    }

    public string addText(string name, string content) {
        return $"{name}: {content} \n";
    }

    public string addHeader(string name) {
        return $"--- \n {name.ToUpper()} \n";
    }
}