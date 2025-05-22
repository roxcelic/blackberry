using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

public class PowerUpMenuController : MonoBehaviour {
    public worldGen World;
    public List<GameObject> powerUpOptions = new List<GameObject>();
    public int selectedOption;
    public int PrevSelectedOption;
    public PowerUpIcons icons;
    public bool changingFloor = false;
    public bool hasChosen = false;

    void Update() {
        selectedOption = eevee.input.CheckAxis("right", "left");
        selectedOption++;

        if (PrevSelectedOption != selectedOption){
            unselectAll();

            powerUpOptions[selectedOption].GetComponent<PowerUp>().Selected = true;
        
            PrevSelectedOption = selectedOption;
        }

        if (eevee.input.Grab("MenuSelect") && !hasChosen && powerUpOptions[selectedOption].GetComponent<PowerUp>().Active) {
            PlayerPrefs.SetString("powerups", PlayerPrefs.GetString("powerups", "") + $",{powerUpOptions[selectedOption].GetComponent<PowerUp>().powerupName}");
            hasChosen = true;
            StartCoroutine(loadNextFloor());
        }
        if (eevee.input.Grab("MenuBack") && !hasChosen && powerUpOptions[selectedOption].GetComponent<PowerUp>().Active) {
            hasChosen = true;
            StartCoroutine(loadNextFloor());
        }
    }

    void unselectAll() {
        foreach (GameObject option in powerUpOptions)
            option.GetComponent<PowerUp>().Selected = false;
    }

    void OnEnable() {
        Debug.Log("loading power ups");
        transform.GetComponent<Animator>().Play("fade-in-powerups");
        
        World.Player.SetActive(false);
    
        // load Power ups
        List<string> currentPowerUps = new List<string>();
        foreach (string powerup in PlayerPrefs.GetString("powerups", "").Split(","))
            if (powerup != "") currentPowerUps.Add(powerup);

        Dictionary<string, powerups.PowerUp> PossiblePowerUps = powerups.full.act;

        foreach (string key in currentPowerUps)
            PossiblePowerUps.Remove(key);

        List<string> powerupKeys = new List<string>();

        foreach (string item in PossiblePowerUps.Keys)
            powerupKeys.Add(item.ToString());

        List<powerups.PowerUp> SelectedPowerUps = new List<powerups.PowerUp>();

        for (int i = 1; i <= 3; i++) {
            if (powerupKeys.Count == 0) break;

            powerups.PowerUp currentPu;
            string PUkey = powerupKeys[UnityEngine.Random.Range(0, powerupKeys.Count - 1)];
            currentPu = powerups.full.act[PUkey];
            currentPu.Name = PUkey;
            SelectedPowerUps.Add(currentPu);
            powerupKeys.Remove(PUkey);
        }

        switch (SelectedPowerUps.Count) {
            case 0:
                powerUpOptions[0].GetComponent<PowerUp>().Active = false;
                powerUpOptions[1].GetComponent<PowerUp>().Active = false;
                powerUpOptions[2].GetComponent<PowerUp>().Active = false;

                hasChosen = true;
                StartCoroutine(loadNextFloor());

                break;
            case 1:
                powerUpOptions[0].GetComponent<PowerUp>().Active = false;
                LoadPowerUp(powerUpOptions[1].GetComponent<PowerUp>(), SelectedPowerUps[0]);
                powerUpOptions[2].GetComponent<PowerUp>().Active = false;

                break;
            case 2:
                LoadPowerUp(powerUpOptions[0].GetComponent<PowerUp>(), SelectedPowerUps[0]);
                powerUpOptions[1].GetComponent<PowerUp>().Active = false;
                LoadPowerUp(powerUpOptions[2].GetComponent<PowerUp>(), SelectedPowerUps[1]);

                break;
            default:
                LoadPowerUp(powerUpOptions[0].GetComponent<PowerUp>(), SelectedPowerUps[0]);
                LoadPowerUp(powerUpOptions[1].GetComponent<PowerUp>(), SelectedPowerUps[1]);
                LoadPowerUp(powerUpOptions[2].GetComponent<PowerUp>(), SelectedPowerUps[2]);

                break; 
        }
    
    Debug.Log("loaded power ups");
    }

    void LoadPowerUp(PowerUp viewer, powerups.PowerUp PU) {
        viewer.powerupName = PU.Name;
        viewer.powerupDescription = PU.Description;

        if (icons.icons().ContainsKey(PU.Name))
            viewer.Icon.sprite = icons.icons()[PU.Name];
    }

    public IEnumerator loadNextFloor() {
        if (changingFloor) yield break;
        transform.GetComponent<Animator>().Play("fade-out-powerups");
    
        changingFloor = true;
        
        PlayerPrefs.SetInt("floor", PlayerPrefs.GetInt("floor", 0) + 1);

        GameObject loadingScreen = GameObject.Find("Canvas").transform.Find("loadingScreen").gameObject;
     
        loadingScreen.SetActive(true);
        loadingScreen.GetComponent<Animator>().Play("loadingScreenEntrence");

        AnimatorStateInfo currentStateInfo = loadingScreen.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        float animLength = currentStateInfo.length;

        yield return new WaitForSeconds(animLength);

        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        
    }
}