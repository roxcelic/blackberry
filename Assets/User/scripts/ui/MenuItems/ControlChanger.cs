using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using TMPro;

public class ControlChanger : MenuItems {
    public eevee.config config;
    public int Type;

    public List<ButtonControl> current_pressed_buttons = new List<ButtonControl>();
    public List<string> current_pressed_names = new List<string>();

    void FixedUpdate() {
        if (Gamepad.current != null) {
            current_pressed_buttons = Gamepad.current.allControls.OfType<ButtonControl>()
                .Where(control => control.isPressed)
                .ToList();

            current_pressed_names = current_pressed_buttons.ConvertAll<string>(control => control.displayName);
        }
    }

    protected override void Start() {
        base.Start();

        string version = Type == 1 ? "Key" : "Controller Button";

        base.Name = $"{version} Change for {config.displayName}";
        base.Description = $"The {version} change for {config.displayName}";
    }

    protected override void Action() {
        base.MenuRoot.canMove = false;
        switch (Type) {
            case 1:
                StartCoroutine(waitForInputKeys());
                
                break;
            case 2:
                StartCoroutine(waitForInputGamepad());

                break;
        }
    }

    public IEnumerator waitForInputKeys() {
        yield return 0;

        while (!Input.anyKeyDown) {
            yield return null;
        }

        foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode))) {
            if (Input.GetKeyDown(kcode)) {
                config.KEYBOARD_code = (int)kcode;
                transform.GetChild(0).GetComponent<TMP_Text>().text = $"Keyboard Key: {(KeyCode)config.KEYBOARD_code}";
                break;
            }
        }

        base.MenuRoot.canMove = true;
        eevee.inject.OverWrite(config);
    }

    public IEnumerator waitForInputGamepad() {
        yield return new WaitForSeconds(0.25f);
        if (Gamepad.current != null) {
            yield return new WaitUntil(() => current_pressed_names.Count > 0);
            config.CONTROLLER_name = current_pressed_names[0];
            transform.GetChild(0).GetComponent<TMP_Text>().text = $"Controller Button: {config.CONTROLLER_name}";
        }
        base.MenuRoot.canMove = true;
        eevee.inject.OverWrite(config);
    }
}