using UnityEngine;
using UnityEngine.UI;

using System;

using TMPro;

public class displayHealth : MonoBehaviour {
    public PlayerController player;
    public MainMenace Enemy;
    public TMP_Text display;
    public type classing;

    public enum type {
        Player,
        Enemy
    }

    void Update() {
        switch (classing.ToString()){
            case "Player":
                display.text = ((int)player.health).ToString();

                break;
            case "Enemy":
                display.text = ((int)Enemy.health).ToString();

                break;
        }
    }
}
