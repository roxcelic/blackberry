using UnityEngine;
using UnityEngine.UI;

using System;
using System.IO;
using System.Linq;
using System.Collections;
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
        };

        public static Dictionary<string, powerups.PowerUp> basis = new Dictionary<string, powerups.PowerUp> {
            ["marmot power"] = new powerups.PowerUp{
                Description = "doubles the rate at which you heal",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    controller.healModifier *= 2;
                }
            },
            ["girl power"] = new powerups.PowerUp{
                Description = "enables dash and sets the dash force to 50f or doubles it",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    if (controller.dash) controller.dashForceModifier *= 2;
                    else controller.dashForceModifier = 50f;

                    controller.dash = true;
                }
            },
            ["you learn to jump"] = new powerups.PowerUp{
                Description = "enables a jump for the player",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    controller.canJump = true;
                }
            },
            ["compass"] = new powerups.PowerUp{
                Description = "allows the player to use pointers",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    controller.pointer = true;
                }
            }
        };

        public static Dictionary<string, powerups.PowerUp> act2 = new Dictionary<string, powerups.PowerUp> {
            ["superUltraGambling"] = new powerups.PowerUp{
                Description = "sets your defence to a random number between 0 and double your current defence",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    controller.defence = UnityEngine.Random.Range(0, controller.defence * 2);
                }
            },
            ["Greater Sword"] = new powerups.PowerUp{
                Description = "-25 speed + 25 base attack",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    controller.speed -= 25f;
                    controller.baseAttackDamage += 25f;
                }
            },
            ["Greatest Sword"] = new powerups.PowerUp{
                Description = "-50 speed +50 base attack",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    controller.speed -= 50f;
                    controller.baseAttackDamage += 50f;
                }
            },
            ["Fist and Steel"] = new powerups.PowerUp{
                Description = "-50 sped +5= defence +10 base attack",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    controller.speed -= 50f;
                    controller.defence += 50f;
                    controller.baseAttackDamage += 10f;
                }
            },
            ["Scholarly Mind"] = new powerups.PowerUp{
                Description = "-25 defence +10 base attack",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    controller.defence -= 25f;
                    controller.PlayerSpecificAttackDamage += 10f;
                }
            },
            ["Fight and Flight"] = new powerups.PowerUp{
                Description = "-25 defence +100 speed",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    controller.defence -= 25f;
                    controller.speed += 100f;
                }
            },
            ["Cowardice"] = new powerups.PowerUp{
                Description = "-25 defence + 200 speed",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    controller.defence -= 25f;
                    controller.speed += 200f;
                }
            },
            ["Spirit Crusher"] = new powerups.PowerUp{
                Description = "+50 player specific attack -50 base attack",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    controller.PlayerSpecificAttackDamage += 50f;
                    controller.baseAttackDamage += 50f;
                }
            },
            ["Thick Skin"] = new powerups.PowerUp{
                Description = "+50 defence",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    controller.defence += 50f;
                }
            },
            ["Places to be"] = new powerups.PowerUp{
                Description = "+100 speed",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    controller.speed += 100f;
                }
            },
            ["Hard-ish Mode"] = new powerups.PowerUp{
                Description = "sets health and max health to 25",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    controller.maxHealth = 25f;
                    controller.health = 25f;
                }
            },
            ["TimeDevice"] = new powerups.PowerUp{
                Description = "slows down time",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    Time.timeScale = 0.5f;
                }
            },


            

            // ieuans
            ["chestplate"] = new powerups.PowerUp{
                Description = "+50 defence -25 speed",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    controller.defence += 50f;
                    controller.speed -= 25f;
                }
            },
            ["speedPotion"] = new powerups.PowerUp{
                Description = "+66 speed",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    controller.speed += 66f;
                }
            },
            ["  "] = new powerups.PowerUp{
                Description = "+25 max health",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    controller.maxHealth += 25f;
                }
            },
        };

        public static Dictionary<string, powerups.PowerUp> fun = new Dictionary<string, powerups.PowerUp> {
            ["Collide With Me"] = new powerups.PowerUp{
                Description = "removes your collider",
                action = (This) => {
                    Base(This);
                    This.GetComponent<Collider>().enabled = false;
                }
            },
            ["dontTouchMe"] = new powerups.PowerUp{
                Description = "removes the power ups",
                action = (This) => {
                    Base(This);
                    PlayerPrefs.SetString("powerups", "");
                }
            },
            ["Hard Mode"] = new powerups.PowerUp{
                Description = "max health = 1",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    controller.maxHealth = 1f;
                }
            },
            ["silly"] = new powerups.PowerUp{
                Description = "sets speed to 1000, allows you to dash and jump",
                action = (This) => {
                    Base(This);
                    PlayerController controller = This.GetComponent<PlayerController>();
                    controller.speed = 1000f;
                    controller.dash = true;
                    controller.canJump = true;
                }
            },
        };

        // ienumerators
        private static IEnumerator TestCoroutine() {
            yield return 0;
        } 


        public static Dictionary<string, powerups.PowerUp> truePowerups(){
            Dictionary<string, powerups.PowerUp> working_ref = full.act;

            foreach(string key in basis.Keys)
                working_ref[key] = basis[key];

            if (PlayerPrefs.GetString("expandedPowerUp", "false") == "true")
                foreach(string key in fun.Keys)
                    working_ref[key] = fun[key];
            
            if (PlayerPrefs.GetInt("powerupMode", 0) == 0)
                foreach(string key in act2.Keys)
                    working_ref[key] = act2[key];

            return working_ref;
        }
    }
}