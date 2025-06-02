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
                    Time.timeScale = 1f;
                    GameObject loadingScreen = GameObject.Find("Canvas").transform.Find("powerups").gameObject;
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