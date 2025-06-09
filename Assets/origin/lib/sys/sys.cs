using UnityEngine;

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace sys {
    public class data {
        // commands
            public static Dictionary<string, eevee.config> config = new Dictionary<string, eevee.config>() {
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
                    "dash", new eevee.config {
                        displayName = "dash",
                        KEYBOARD_code = (int)KeyCode.X,
                        CONTROLLER_name = "Right Trigger"
                    }
                },
                {
                    "attack", new eevee.config {
                        displayName = "attack",
                        KEYBOARD_code = (int)KeyCode.Return,
                        CONTROLLER_name = "Right Trigger"
                    }
                },
                {
                    "attack-L", new eevee.config {
                        displayName = "attack-L",
                        KEYBOARD_code = (int)KeyCode.LeftArrow,
                        CONTROLLER_name = ""
                    }
                },
                {
                    "attack-R", new eevee.config {
                        displayName = "attack-R",
                        KEYBOARD_code = (int)KeyCode.RightArrow,
                        CONTROLLER_name = ""
                    }
                },
                {
                    "attack-U", new eevee.config {
                        displayName = "attack-U",
                        KEYBOARD_code = (int)KeyCode.UpArrow,
                        CONTROLLER_name = ""
                    }
                },
                {
                    "attack-D", new eevee.config {
                        displayName = "attack-D",
                        KEYBOARD_code = (int)KeyCode.DownArrow,
                        CONTROLLER_name = ""
                    }
                },
                {
                    "pause", new eevee.config {
                        displayName = "pause",
                        KEYBOARD_code = (int)KeyCode.Escape,
                        CONTROLLER_name = "Start"
                    }
                },
                // menu
                {
                    "MenuLeft", new eevee.config {
                        displayName = "pause",
                        KEYBOARD_code = (int)KeyCode.LeftArrow,
                        CONTROLLER_name = "Left Stick Left"
                    }
                },
                {
                    "MenuRight", new eevee.config {
                        displayName = "MenuRight",
                        KEYBOARD_code = (int)KeyCode.RightArrow,
                        CONTROLLER_name = "Left Stick Right"
                    }
                },
                {
                    "MenuUp", new eevee.config {
                        displayName = "MenuUp",
                        KEYBOARD_code = (int)KeyCode.UpArrow,
                        CONTROLLER_name = "Left Stick Up"
                    }
                },
                {
                    "MenuDown", new eevee.config {
                        displayName = "MenuDown",
                        KEYBOARD_code = (int)KeyCode.DownArrow,
                        CONTROLLER_name = "Left Stick Down"
                    }
                },
                {
                    "MenuSelect", new eevee.config {
                        displayName = "MenuSelect",
                        KEYBOARD_code = (int)KeyCode.C,
                        CONTROLLER_name = "A"
                    }
                },
                {
                    "MenuBack", new eevee.config {
                        displayName = "MenuBack",
                        KEYBOARD_code = (int)KeyCode.X,
                        CONTROLLER_name = "A"
                    }
                },
                // debug
                {
                    "reset", new eevee.config {
                        displayName = "reset",
                        KEYBOARD_code = (int)KeyCode.R,
                        CONTROLLER_name = "Y"
                    }
                },
                {
                    "commandBarOpen", new eevee.config {
                        displayName = "commandBarOpen",
                        KEYBOARD_code = (int)KeyCode.Q,
                        CONTROLLER_name = "start"
                    }
                },
                {
                    "nextCommand", new eevee.config {
                        displayName = "nextCommand",
                        KEYBOARD_code = (int)KeyCode.UpArrow,
                        CONTROLLER_name = "Left Stick Up"
                    }
                },
                {
                    "lastCommand", new eevee.config {
                        displayName = "lastCommand",
                        KEYBOARD_code = (int)KeyCode.DownArrow,
                        CONTROLLER_name = "Left Stick Down"
                    }
                }
            };
    }
}