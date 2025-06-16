using UnityEngine;
//Ariana, peak? its basically inheriting stuff from PlayerController, which is the "parent" class, idk what you call it
public class Ariana : PlayerControllerH {

    protected override void Start() {
        playerhealth = 12.0f;
        healthbar.maxValue = 12f;
        healthbar.value = playerhealth;
        //for the healthbar, initial stats
        steambar.maxValue = 10f;
        steambar.value = 10f;
        steambar.value = steam;
        //for the steambar, initial stats
        startplayerspeed = 15;
        playerspeed = 15;
        base.Start();
        Debug.Log("Started successfully");
    }

}
