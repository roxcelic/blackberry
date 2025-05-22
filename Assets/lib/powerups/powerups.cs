using UnityEngine;
using System;
using System.Collections.Generic;

namespace powerups {
    [System.Serializable]
    public class PowerUp {
        public string Description;
        public string Name;
        public Action<MonoBehaviour> action;
    }

    public static class full {
        public static Action<MonoBehaviour> Base = (This) => {
            if (This.gameObject.tag != "Player") return;
        };

        public static Dictionary<string, powerups.PowerUp> act = new Dictionary<string, powerups.PowerUp> {
            ["superUltraGambling"] = new powerups.PowerUp{
                Description = "",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    controller.defence = UnityEngine.Random.Range(0, controller.defence * 2);
                }
            },
            ["dontTouchMe"] = new powerups.PowerUp{
                Description = "클릭",
                action = (This) => {
                    Base(This);
                    PlayerPrefs.SetString("powerups", "");
                }
            },
            ["Collide With Me"] = new powerups.PowerUp{
                Description = "<3",
                action = (This) => {
                    Base(This);
                    This.GetComponent<Collider>().enabled = false;
                }
            },

            // ieuans
            ["chestplate"] = new powerups.PowerUp{
                Description = "defense go up, speed go down, its a big chestplate what did you expect",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    controller.defence *= 1.5f;
                    controller.speed *= 0.75f;
                }
            },
            ["speedPotion"] = new powerups.PowerUp{
                Description = "take a wild guess",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    controller.speed *= 1.66f;
                }
            },
            ["heart"] = new powerups.PowerUp{
                Description = "better heart = better health",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    controller.maxHealth += 25f;
                }
            },
        };

    }
}