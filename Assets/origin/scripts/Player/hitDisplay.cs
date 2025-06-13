using UnityEngine;
using UnityEngine.UI;

using System;

using TMPro;

public class hitDisplay : MonoBehaviour {
    [Header("components")]
    public Rigidbody rb;
    public TMP_Text text;

    [Header("config")]
    public Vector3 initial_force;
    public float damage;

    public bool color = true;
    public Color positive;
    public Color negative;

    void Start() {
        // initialise components
        rb = GetComponent<Rigidbody>();

        if (color) {
            if (damage >= 0) text.color = positive;
            else text.color = negative;
        }

        text.text = Math.Round(damage, 2).ToString();

        rb.AddForce(initial_force, ForceMode.Impulse);
    }

    public void Destroy() {
        Destroy(transform.gameObject);
    }
}