using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.Collections;

public class PlayButton : MenuItems {
    [Header("PlayButton Config")]
    public int WorldType;
    public int playerType;
    public GameObject loadingScreen;
    public GameObject seedMenu;

    protected override void Start() {
        base.Start();

        loadingScreen = GameObject.Find("Canvas").transform.Find("loadingScreen").gameObject;
        seedMenu = transform.parent.parent.GetChild(transform.parent.parent.childCount - 1).gameObject;
    }

    protected override void Action() {
        // configable settings
        PlayerPrefs.SetInt("worldType", WorldType);
        PlayerPrefs.SetInt("playerType", playerType);

        PlayerPrefs.SetString("powerups", "");
        // PlayerPrefs.SetInt("powerupMode", WorldType);
        PlayerPrefs.SetInt("powerupMode", 0);

        StartCoroutine(loadworld());
    }

    public IEnumerator loadworld() {
        loadingScreen.SetActive(true);
        loadingScreen.GetComponent<Animator>().Play("loadingScreenEntrence");

        PlayerPrefs.SetInt("floor", 0);

        AnimatorStateInfo currentStateInfo = loadingScreen.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        float animLength = currentStateInfo.length;

        yield return new WaitForSeconds(animLength);

        string seed = "";
        foreach (Transform seedItem in seedMenu.transform) {
            string seedItemCharacter = seedItem.GetComponent<seedPicker>().SelectedCharacter;
            seed = $"{seed}{(seedItemCharacter == "_" ? "0" : seedItemCharacter)}";
        }

        if (seed == "000000")
            seed = GenerateRandomSeed();

        PlayerPrefs.SetString("seed", seed);
        Debug.Log($"new seed: {seed}");
        SceneManager.LoadScene("betterWorld");
    }

    public string GenerateRandomSeed() {
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        char[] stringChars = new char[6];
        System.Random random = new System.Random();

        for (int i = 0; i < stringChars.Length; i++)
            stringChars[i] = chars[random.Next(chars.Length)];

        return new String(stringChars);
    }
}