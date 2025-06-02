using UnityEngine;

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using TMPro;

public class Commands : MonoBehaviour {
    public TMP_InputField textInput;
    public int selectedCommand = 0;
    public List<string> usedCommands = new List<string>();

    void Start() {
        // idk
        textInput = GetComponent<TMP_InputField>();
    }

    void Update() {
        if (eevee.input.Collect("nextCommand")) {
            if (usedCommands.Count > 0) {
                if (selectedCommand == 0)
                    selectedCommand = usedCommands.Count - 1;
                else
                    selectedCommand--;

                textInput.text = usedCommands[selectedCommand];
            }
        }

        if (eevee.input.Collect("lastCommand")) {
            if (usedCommands.Count > 0) {
                if (selectedCommand == usedCommands.Count - 1)
                    selectedCommand = 0;
                else
                    selectedCommand++;

                textInput.text = usedCommands[selectedCommand];
            }
        }
    }

    public void RunCommand() {
        // text management
        commandGLI.execute.ViaString(textInput.text);
        usedCommands.Add(textInput.text);
        textInput.text = "";

        selectedCommand = 0;

        // reselecting the input field
        textInput.ActivateInputField();
    }
}