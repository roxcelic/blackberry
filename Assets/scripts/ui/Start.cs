using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using TMPro;

public class Start : MonoBehaviour {
    public TMP_InputField seed;

    public void StartGame() {
        string seedText = seed.text;

        if (seedText != "") {
            while (seedText.Length != 6)
                seedText += "0";
        } else {
            seedText = "outiss";
        }

        PlayerPrefs.SetString("seed", seedText);
        PlayerPrefs.SetInt("floor", 1);

        SceneManager.LoadScene("level");
    }
}
