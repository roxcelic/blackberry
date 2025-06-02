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
            ["Greater Sword"] = new powerups.PowerUp{
                Description = "see it's like a greatsword but greater",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    controller.speed -= 25f;
                    controller.baseAttackDamage += 25f;
                }
            },
            ["Greatest Sword"] = new powerups.PowerUp{
                Description = "this one is the same as prior but it's even greater",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    controller.speed -= 50f;
                    controller.baseAttackDamage += 50f;
                }
            },
            ["Fist and Steel"] = new powerups.PowerUp{
                Description = "ok listen i know its dated but i'm grasping at straws here, the defense makes sense i promise the hand are shielded and you're slower because they’re metal and that's heavy this idea makes sense i promise you",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    controller.speed -= 50f;
                    controller.defence += 50f;
                    controller.baseAttackDamage += 10f;
                }
            },
            ["Scholarly Mind"] = new powerups.PowerUp{
                Description = "see its funny cause it's that one thing",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    controller.defence -= 25f;
                    controller.PlayerSpecificAttackDamage += 10f;
                }
            },
            ["Fight and Flight"] = new powerups.PowerUp{
                Description = "because it raises ‘fight’ [damage] and ‘flight’ [speed] get it isn't that funny chat",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    controller.defence -= 25f;
                    controller.speed += 100f;
                }
            },
            ["Cowardice"] = new powerups.PowerUp{
                Description = "because it raises ‘fight’ [damage] and ‘flight’ [speed] get it isn't that funny chat",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    controller.defence -= 25f;
                    controller.speed += 200f;
                }
            },
            ["Spirit Crusher"] = new powerups.PowerUp{
                Description = "raises attack to simulate the defense of the enemies going down, due to them seeing the skull of a dead ally, hence the name",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    controller.PlayerSpecificAttackDamage += 50f;
                    controller.baseAttackDamage += 50f;
                }
            },
            ["Thick Skin"] = new powerups.PowerUp{
                Description = "i don't actually know what this will look like i just wanna call a defensive item “thick skin”",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    controller.defence += 50f;
                }
            },
            ["Places to be"] = new powerups.PowerUp{
                Description = "they don’t have time for all this!!",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    controller.speed += 100f;
                }
            },
            ["Hard-ish Mode"] = new powerups.PowerUp{
                Description = "because it's easier than making an actual difficulty setting",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    controller.maxHealth = 25f;
                }
            },


            

            // ieuans
            ["chestplate"] = new powerups.PowerUp{
                Description = "defense go up, speed go down, its a big chestplate what did you expect",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    controller.defence += 50f;
                    controller.speed -= 25f;
                }
            },
            ["speedPotion"] = new powerups.PowerUp{
                Description = "take a wild guess",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    controller.speed += 66f;
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

        public static Dictionary<string, powerups.PowerUp> fun = new Dictionary<string, powerups.PowerUp> {
            ["Collide With Me"] = new powerups.PowerUp{
                Description = "<3",
                action = (This) => {
                    Base(This);
                    This.GetComponent<Collider>().enabled = false;
                }
            },
            ["dontTouchMe"] = new powerups.PowerUp{
                Description = "클릭",
                action = (This) => {
                    Base(This);
                    PlayerPrefs.SetString("powerups", "");
                }
            },
            ["Hard Mode"] = new powerups.PowerUp{
                Description = "because it's easier than making an actual difficulty setting",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    controller.maxHealth = 1f;
                }
            },
        };

        public static Dictionary<string, powerups.PowerUp> truePowerups(){
            Dictionary<string, powerups.PowerUp> working_ref = act;

            if (PlayerPrefs.GetString("expandedPowerUp", "false") == "true")
                foreach(string key in fun.Keys)
                    act[key] = fun[key];

            return working_ref;
        }
    }
}