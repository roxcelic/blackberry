using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class PowerUp : MonoBehaviour {
    [Header("menu config")]
    public bool Selected;
    public bool Active;
    public Vector3 SelectedScale;
    public float smoothSpeed = 5f;

    [Header("powerup data")]
    public string powerupName;
    public string powerupDescription;
    public Image Icon;

    [Header("display")]
    public TMP_Text Title;
    public TMP_Text Description;

    void Start() {
        Title.text = powerupName;
    }

    void Update() {
        if (!Active)
            gameObject.SetActive(false);
        
        // size
        if (Selected) {
            transform.localScale = Vector3.Lerp(transform.localScale, SelectedScale, Time.deltaTime * smoothSpeed);
            Description.text = powerupDescription;
        } else {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1, 1), Time.deltaTime * smoothSpeed);
        }
    }
}