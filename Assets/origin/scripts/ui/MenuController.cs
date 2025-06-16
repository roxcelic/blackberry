using UnityEngine;
using UnityEngine.UI;

using TMPro;

using System;
using System.Collections;
using System.Collections.Generic;

public class MenuController : MonoBehaviour {

    [Header("Selection")]
    public int SelectedX;
    public int SelectedY;
    public GameObject SelectedItem;
    public MenuItems SelectedMenuItem;
    public bool active;
    public bool canMove = true;
    public bool center = false;

    [Header("return")]
    public bool canReturn = true;
    public MenuController returnMenu;
    public CameraUi sceneCamera;

    [SerializeField] private int MaxX;
    [SerializeField] private int MaxY;

    // log values
    private int PrevSelectedX;
    private int PrevSelectedY;
    
    private TMP_Text Text;
    private Dictionary<string, eevee.config> Controls;

    void Start() {
        //reset time
        Time.timeScale = 1f;

        MaxY = transform.childCount - 1;
        MaxX = transform.GetChild(SelectedY).childCount - 1;
    
        SelectedX = MaxX / 2;

        SelectedItem = transform.GetChild(SelectedY).GetChild(SelectedX).gameObject;

        Text = GameObject.Find("Canvas/Image/DebugText").GetComponent<TMP_Text>();
    }

    void Update() {
        UpdateSelection();
    }

    public void UpdateSelection(bool down = false, bool up = false) {
        if (active && canMove){
            bool vertical = false;
            // inputs
                // horizontal
                if (eevee.input.Collect("MenuLeft") && SelectedX > 0)
                    SelectedX--;
                if (eevee.input.Collect("MenuRight") && SelectedX < MaxX)
                    SelectedX++;


                // vertical
                if (eevee.input.Collect("MenuUp") && SelectedY > 0){
                    SelectedY--;
                    vertical = true;
                    up = true;
                }
                if (eevee.input.Collect("MenuDown") && SelectedY < MaxY){
                    SelectedY++;
                    vertical = true;
                    down = true;
                }

                if (eevee.input.Collect("MenuBack") && canReturn){
                    returnMenu.active = true;
                    sceneCamera.MenuRoot = returnMenu.GetComponent<MenuController>();
                    active = false;
                }

            // update the max left and right
            MaxX = transform.GetChild(SelectedY).childCount - 1;

            if (vertical && center)
                SelectedX = MaxX / 2;

            // lock the selection
            if (SelectedX > MaxX)
                SelectedX = MaxX;

            // updating selected object
            if (
                (SelectedX != PrevSelectedX) ||
                (SelectedY != PrevSelectedY)
            ) SelectedItem = transform.GetChild(SelectedY).GetChild(SelectedX).gameObject;

            if (!SelectedItem.activeSelf && up || down) {
                if (down) {
                    if (SelectedY >= MaxY && !SelectedItem.activeSelf) {
                        UpdateSelection(true, false);
                    } else {
                        SelectedY++;
                        UpdateSelection();
                    }
                } else if (up) {
                    if (SelectedY <= 0 && !SelectedItem.activeSelf) {
                        UpdateSelection(false, true);
                    } else {
                        SelectedY--;
                        UpdateSelection();
                    }
                }
            }

            // updating log values
            PrevSelectedX = SelectedX;
            PrevSelectedY = SelectedY;

            SelectedMenuItem = SelectedItem.GetComponent<MenuItems>();

            Controls = eevee.Qlock.extractr();

            Text.text = $"press [Keyboard: {(KeyCode)Controls["MenuSelect"].KEYBOARD_code}] or [Gamepad: {Controls["MenuSelect"].CONTROLLER_name}] to Select";
            Text.text += $"\n Keyboard: {(KeyCode)Controls["MenuBack"].KEYBOARD_code}] or [Gamepad: {Controls["MenuBack"].CONTROLLER_name}] to go back";
        } else if (active) {
            Text.text = $"press [Keyboard: {(KeyCode)Controls["MenuLeft"].KEYBOARD_code}] or [Gamepad: {Controls["MenuLeft"].CONTROLLER_name}] to increase value";
            Text.text += $"\n Keyboard: {(KeyCode)Controls["MenuRight"].KEYBOARD_code}] or [Gamepad: {Controls["MenuRight"].CONTROLLER_name}] to decrease value";
        }
    }
}
