using UnityEngine;

using System.Collections.Generic;

using eevee;

public class controlInjector : MonoBehaviour {

     public Dictionary<string, eevee.config> config = new Dictionary<string, eevee.config>() {
        // movement
        {
            "right", new eevee.config {
                displayName = "right",
                KEYBOARD_code = (int)KeyCode.D,
                CONTROLLER_name = "Left Stick Right"
            }
        },
        {
            "left", new eevee.config {
                displayName = "left",
                KEYBOARD_code = (int)KeyCode.A,
                CONTROLLER_name = "Left Stick Left"
            }
        },
        {
            "up", new eevee.config {
                displayName = "up",
                KEYBOARD_code = (int)KeyCode.W,
                CONTROLLER_name = "Left Stick Up"
            }
        },
        {
            "down", new eevee.config {
                displayName = "down",
                KEYBOARD_code = (int)KeyCode.S,
                CONTROLLER_name = "Left Stick Down"
            }
        },
        // attack
        {
            "guard", new eevee.config {
                displayName = "guard",
                KEYBOARD_code = (int)KeyCode.Q,
                CONTROLLER_name = "X"
            }
        },
        {
            "attack", new eevee.config {
                displayName = "attack",
                KEYBOARD_code = (int)KeyCode.Mouse0,
                CONTROLLER_name = "A"
            }
        },
        {
            "pause", new eevee.config {
                displayName = "pause",
                KEYBOARD_code = (int)KeyCode.Escape,
                CONTROLLER_name = "Start"
            }
        },
        // debug
        {
            "reset", new eevee.config {
                displayName = "reset",
                KEYBOARD_code = (int)KeyCode.R,
                CONTROLLER_name = "A"
            }
        },
    };
    
    void Start() {
        eevee.inject.install(config);
    }
}
