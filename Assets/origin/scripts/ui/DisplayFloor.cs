using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class DisplayFloor : MonoBehaviour {
    public TMP_Text displaybox;

    void Start() {
        displaybox = GetComponent<TMP_Text>();
        displaybox.text = PlayerPrefs.GetInt("floor", 0).ToString();
    }
}