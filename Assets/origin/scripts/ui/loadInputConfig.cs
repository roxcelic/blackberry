using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class loadInputConfig : MonoBehaviour {
    public eevee.config config;
    public TMP_Text text;

    void OnEnable() {
        text.text = $"change the control for {config.displayName}";

        transform.GetChild(1).GetComponent<ControlChanger>().config = config;
            transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = $"Keyboard Key: {(KeyCode)config.KEYBOARD_code}";
        transform.GetChild(2).GetComponent<ControlChanger>().config = config;
            transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = $"Controller Button: {config.CONTROLLER_name}";

        foreach (Transform child in transform) {
            child.GetComponent<MenuItems>().UnselectedOpacity = .25f;
        }
    }
}
