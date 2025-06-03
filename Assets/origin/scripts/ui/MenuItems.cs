using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

public class MenuItems : MonoBehaviour {

    // management
    private bool Selected;
    public MenuController MenuRoot;

    [Header("config")]
    public bool unSelectable = false;
    public string Name;
    public string Description;

    public float smoothSpeed = 5f;
    public Vector3 SelectedScale;
    [Range(0, 180f)] public float RotationRange;
    [Range(0, 1f)] public float UnselectedOpacity = .25f;

    public Vector3 rotationAngle;
    public Vector3 realRotation;
    public bool rotate = true;
    public bool scale = true;
    public bool opacity = true;

    [Header("components")]
    public SpriteRenderer spriteRenderer;

    private Vector3 UnSelectedScale;
    private float opacityHold;

    protected virtual void Start() {
        MenuRoot = transform.parent.parent.GetComponent<MenuController>();

        if (rotate) {
            rotationAngle = new Vector3(
                0, 
                UnityEngine.Random.Range(360 - RotationRange, RotationRange), 
                UnityEngine.Random.Range(360 - RotationRange, RotationRange)
            );

            transform.rotation = Quaternion.Euler(rotationAngle);
        }

        UnSelectedScale = transform.localScale;
        
        // grab components
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (spriteRenderer != null)
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, UnselectedOpacity);
    }

    protected virtual void Update() {
        if (spriteRenderer != null)
            opacityHold = spriteRenderer.color.a;

        // if selected
        if (MenuRoot.SelectedItem == transform.gameObject) {
            // change scale
            if (scale)
                transform.localScale = Vector3.Lerp(transform.localScale, SelectedScale, Time.deltaTime * smoothSpeed);

            if (rotate){
                realRotation = Vector3.Lerp(transform.eulerAngles, Vector3.zero, Time.deltaTime * smoothSpeed / 2);
                transform.rotation = Quaternion.Euler(realRotation);
            }

            if (opacity && spriteRenderer != null) 
                opacityHold = Mathf.Lerp(opacityHold, 1, Time.deltaTime * smoothSpeed / 2);

        // if un selected and shrinking
        } else if (transform.localScale != UnSelectedScale) {
            if (rotate){
                realRotation = Vector3.Lerp(transform.eulerAngles, rotationAngle, Time.deltaTime * smoothSpeed / 2);
                transform.rotation = Quaternion.Euler(realRotation);
            }
            
            // reset scale
            if (scale)
                transform.localScale = Vector3.Lerp(transform.localScale, UnSelectedScale, Time.deltaTime * smoothSpeed);    
            
            if (opacity && spriteRenderer != null) 
                opacityHold = Mathf.Lerp(opacityHold, UnselectedOpacity, Time.deltaTime * smoothSpeed / 2);
        
        // if un selected
        }

        // stop memory leakage nerd
        if (realRotation.x > 1 || realRotation.x < -1)
            realRotation.x = 0;
        if (realRotation.y > 1 || realRotation.y < -1)
            realRotation.y = 0;
        if (realRotation.z > 1 || realRotation.z < -1)
            realRotation.z = 0;

        if ( MenuRoot.SelectedItem == transform.gameObject && MenuRoot.active){
            if (eevee.input.Collect("MenuSelect"))
                Action();
        }

        if (spriteRenderer != null)
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, opacityHold);
    }

    protected virtual void Action(){
        Debug.Log("no action injected");
    }
}
