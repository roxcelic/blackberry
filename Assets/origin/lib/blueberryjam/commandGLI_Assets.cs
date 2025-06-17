using UnityEngine;
using UnityEngine.UI;

using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using TMPro;

namespace commandGLI_Assets {
    public class data {
        // commands
        public static Dictionary<string, commandGLI.command> commandList = new Dictionary<string, commandGLI.command>{
            ["test"] = new commandGLI.command {
                Description = "a test",
                Help = "a simple test command",

                action = (string input, string fullCommand) => {
                    commandGLI.utils.Log("test");
                },

                expand = new Dictionary<string, commandGLI.command>{
                    ["test"] = new commandGLI.command {
                        Description = "a test",
                        Help = "a simple test command but better",

                        action = (string input, string fullCommand) => {
                            commandGLI.utils.Log("supertest");
                        }
                    },
                    ["spawnmonobehaviour"] = new commandGLI.command {
                        Description = "spawns a mono behaviour",
                        Help = "spawns a mono behaviour not much to it",

                        action = (string input, string fullCommand) => {
                            commandGLI.utils.Log("spawning a mono behaviour");
                            MonoBehaviour tmpMonoBehaviour = commandGLI.utils.GenerateMonoBehaviour("creating a mono behaviour");
                        }
                    },
                    ["ienumeratortest"] = new commandGLI.command {
                        Description = "runs a test ienumerator",
                        Help = "spawns a test ienumerator so it uses realtime",

                        action = (string input, string fullCommand) => {
                            MonoBehaviour tmpMonoBehaviour = commandGLI.utils.GenerateMonoBehaviour("test.ienumeratortest");
                            tmpMonoBehaviour.StartCoroutine(commandGLI_Assets.data.TestCoroutine(tmpMonoBehaviour));
                        }
                    }
                }
            },
            ["echo"] = new commandGLI.command {
                Description = "a test",
                Help = "a simple test command",
                Useage = "echo(item_to_echo)",

                action = (string input, string fullCommand) => {
                    commandGLI.utils.Log(input);
                },
            },
            ["god"] = new commandGLI.command {
                Description = "god",
                Help = "god",
                Useage = "god // god()",

                action = (string input, string fullCommand) => {
                    commandGLI.utils.GetPlayer().maxHealth = 1000;
                    commandGLI.utils.GetPlayer().health = 1000;
                    commandGLI.utils.GetPlayer().speed = 150;
                },
            },
            ["clear"] = new commandGLI.command {
                Description = "clears the terminal",
                Help = "clears the terminal",
                Useage = "clear // clear()",

                action = (string input, string fullCommand) => {
                    GameObject DisplayCommand = GameObject.FindGameObjectsWithTag("commands")[0];

                    if (DisplayCommand != null && DisplayCommand.GetComponent<TMP_Text>() != null)
                        DisplayCommand.GetComponent<TMP_Text>().text = "";
                }
            },
            ["help"] = new commandGLI.command {
                Description = "will return the help data for each command",
                Help = "a simple help command",
                Useage = "help(command to ask help for)",

                action = (string input, string fullCommand) => {
                    commandGLI.utils.Log(commandGLI.execute.retreiveCommand(input).Help, true, "help");
                    commandGLI.utils.Log("if that didnt help you try running listcommands", true, "help");
                },

                expand = new Dictionary<string, commandGLI.command>{
                    ["annoying_orange"] = new commandGLI.command {
                        Description = "...",
                        Help = "...",
                        Hidden = true,

                        action = (string input, string fullCommand) => {
                            commandGLI.utils.Log("he is here, run for your life");
                            Application.OpenURL("https://youtu.be/akT0wxv9ON8?si=Nb4LPGoxuupw9DzU");
                        }
                    }
                }
            },
            ["listcommands"] = new commandGLI.command {
                Description = "This does as it says really",
                Help = "will list all commands",
                Useage = "listcommands // listcommands()",

                action = (string input, string fullCommand) => {
                    commandGLI.utils.Log("---", true, "ListCommands", false);
                    string hold = commandGLI.utils.GatherData(commandGLI_Assets.data.commandList, commandGLI.var.prefix);

                    commandGLI.utils.Log(hold, false);
                    commandGLI.utils.Log("---", true, "ListCommands");
                }
            },
            ["exit"] = new commandGLI.command {
                Description = "closes the terminal",
                Help = "will close the terminal",
                Useage = "exit // exit()",

                action = (string input, string fullCommand) => {
                    // if (UnityEditor.EditorApplication.isPlaying)
                    //     UnityEditor.EditorApplication.isPlaying = false;
                    // else 
                    commandGLI.utils.Log("exiting terminal");
                        Time.timeScale = 1f;
                        GameObject.FindGameObjectsWithTag("commands")[2].SetActive(false);
                }
            },
            ["dio"] = new commandGLI.command {
                Description = "dio brandon",
                Help = "dio brandon",
                Useage = "dio // dio()",

                action = (string input, string fullCommand) => {
                    commandGLI.utils.Log("you know what to do");
                },
                expand = new Dictionary<string, commandGLI.command>{
                    ["theworldo"] = new commandGLI.command {
                        Description = "changes the timescale",
                        Help = "will change the timescale while the menu is open",

                        action = (string input, string fullCommand) => {
                            commandGLI.utils.Log($"changing timescale to: {input}");
                            Time.timeScale = float.Parse(input);
                            commandGLI.utils.Log("succesfully changed time scale");
                        }
                    }
                }
            },
            ["killallmenace"] = new commandGLI.command {
                Description = "will kill all menaces",
                Help = "kills all of the objects that derive from the class menace",
                Useage = "killallmenace // killallmenace()",

                action = (string input, string fullCommand) => {
                    GameObject[] menaces = GameObject.FindGameObjectsWithTag("enemy");

                    foreach (GameObject menace in menaces)
                        menace.GetComponent<MainMenace>().Die();
                }
            },
            ["forceend"] = new commandGLI.command {
                Description = "forces the end of the floor",
                Help = "will act as though you beat the floor",
                Useage = "forceend // forceend()",

                action = (string input, string fullCommand) => {
                    if (input == "") input = "null";
                    
                    Time.timeScale = 1f;
                    GameObject loadingScreen = GameObject.Find("Canvas").transform.Find("powerups").gameObject;
                    PlayerPrefs.SetString("tmpForcePowerup", input);
                    loadingScreen.SetActive(true);
                }
            },
            ["setfloor"] = new commandGLI.command {
                Description = "will set the floor then end the floor",
                Help = "will move you to whatever floor you desire",
                Useage = "setfloor(floornumber)",

                action = (string input, string fullCommand) => {
                    PlayerPrefs.SetInt("floor", Int32.Parse(input) );
                    Time.timeScale = 1f;
                    GameObject loadingScreen = GameObject.Find("Canvas").transform.Find("powerups").gameObject;
                    loadingScreen.SetActive(true);
                }
            },
            ["fishcurry"] = new commandGLI.command {
                Description = "...",
                Help = "...",
                Hidden = true,

                action = (string input, string fullCommand) => {
                    commandGLI.utils.Log("jack and the fishies");
                    Application.OpenURL("https://media.discordapp.net/attachments/1347315267969224857/1374392503574331492/SPOILER_bounce.gif?ex=683e5d03&is=683d0b83&hm=4d0a44a530228a060b6dccbebc33b821dc455d78305880f07719c8ce368180c3&=&width=556&height=922");
                },

                expand = new Dictionary<string, commandGLI.command>{
                    ["thesecond"] = new commandGLI.command {
                        Description = "oooh",
                        Help = "oooh",
                        Hidden = true,

                        action = (string input, string fullCommand) => {
                            commandGLI.utils.Log("smash?");
                            Application.OpenURL("https://media.discordapp.net/attachments/1347315267969224857/1378518107504771092/PXL_20250526_191500612.MP.jpg?ex=683e3608&is=683ce488&hm=cf51417c72411c191d1123bd6f312983737c69b54be0965dc07bd4521185b9c7&=&format=webp&width=691&height=922");
                        }
                    }
                }
            },
            ["setplayer"] = new commandGLI.command {
                Description = "set a value of the player",
                Help = "use this to set values of the player",

                action = (string input, string fullCommand) => {
                    commandGLI.utils.Log("please enter a value to change");
                },

                expand = new Dictionary<string, commandGLI.command>{
                    ["health"] = new commandGLI.command {
                        Description = "sets the player health",
                        Help = "sets the players health",
                        Useage = "setplayer.health(healthtoset)",

                        action = (string input, string fullCommand) => {
                            commandGLI.utils.GetPlayer().health = Int32.Parse(input);
                        }
                    },
                    ["maxhealth"] = new commandGLI.command {
                        Description = "sets the player max health",
                        Help = "sets the players max health",
                        Useage = "setplayer.health(healthtoset)",

                        action = (string input, string fullCommand) => {
                            commandGLI.utils.GetPlayer().maxHealth = Int32.Parse(input);
                        }
                    },
                    ["speed"] = new commandGLI.command {
                        Description = "sets the player speed",
                        Help = "sets the players speed",
                        Useage = "setplayer.speed(speed)",

                        action = (string input, string fullCommand) => {
                            commandGLI.utils.GetPlayer().speed = Int32.Parse(input);
                        }
                    },
                    ["defence"] = new commandGLI.command {
                        Description = "sets the player defence",
                        Help = "sets the players defence",
                        Useage = "setplayer.defence(defence)",

                        action = (string input, string fullCommand) => {
                            commandGLI.utils.GetPlayer().defence = Int32.Parse(input);
                        }
                    }
                }
            },
            ["addpowerup"] = new commandGLI.command {
                Description = "adds a power up to the player",
                Help = "add a power up to the player",
                Useage = "addpowerup(nameofpowerup)",

                action = (string input, string fullCommand) => {
                    PlayerPrefs.SetString("powerups", PlayerPrefs.GetString("powerups", "") + $",{input}");
                    commandGLI.utils.Log($"if {input} is a possible power up it will take effect next floor");
                }
            },
            ["removeallpowerups"] = new commandGLI.command {
                Description = "removes all power ups",
                Help = "removes all the powerups",
                Useage = "removeallpowerups // removeallpowerups()",

                action = (string input, string fullCommand) => {
                    PlayerPrefs.SetString("powerups", "");
                    commandGLI.utils.Log($"there are no more powerups on the player, will take effect next floor");
                }
            },
            ["removepowerup"] = new commandGLI.command {
                Description = "removes a specific power up",
                Help = "removes a specific powerup",
                Useage = "removepowerup(power up to remove)",

                action = (string input, string fullCommand) => {
                    PlayerPrefs.SetString("powerups", (String.Join(",", (PlayerPrefs.GetString("powerups", "").Split(",").ToList().Remove(input)))));
                }
            },
            ["listcurrentpowerups"] = new commandGLI.command {
                Description = "lists current powerups",
                Help = "lists all active powerups",
                Useage = "listcurrentpowerups // listcurrentpowerups()",

                action = (string input, string fullCommand) => {
                    commandGLI.utils.Log(PlayerPrefs.GetString("powerups", ""));
                }
            },
            ["listpossiblepowerups"] = new commandGLI.command {
                Description = "lists possible powerups",
                Help = "lists all possible powerups",
                Useage = "listpossiblepowerups // listpossiblepowerups()",

                action = (string input, string fullCommand) => {
                    commandGLI.utils.Log(String.Join(", ", powerups.full.truePowerups().Keys.ToList()));
                }
            }
        };

        // ienumerators go here, so like yay new feature?
        private static IEnumerator TestCoroutine(MonoBehaviour tmpMonoBehaviour) {
            commandGLI.utils.Log("started second wait");
            yield return new WaitForSeconds(1f);
            commandGLI.utils.Log("ended second wait");
            commandGLI.utils.clearMonoBehaviour(tmpMonoBehaviour);
        } 

        // a basic fail command
        public static commandGLI.command failCommand = new commandGLI.command {
            Description = "unknown command",
            Help = "unknown command",

            action = (string input, string fullCommand) => {
                commandGLI.utils.Log($"unable to find command: {fullCommand}");
            }
        };
    }
}