using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

public class seedPicker : SliderIsh {
    [Header("seedPicker config")]
    public string SelectedCharacter = "_";
    
    public string[] characterConversion = new string[]{
        "0",
        "1",
        "2",
        "3",
        "4",
        "5",
        "6",
        "7",
        "8",
        "9",
        "a",
        "b",
        "c",
        "d",
        "e",
        "f",
        "g",
        "h",
        "i",
        "j",
        "k",
        "l",
        "m",
        "n",
        "o",
        "p",
        "q",
        "r",
        "s",
        "t",
        "u",
        "v",
        "w",
        "x",
        "y",
        "z"
    };

    protected override void Start() {
        base.Start();

        base.Max = characterConversion.Length;
    }

    protected override void write() {
        string Hold = base.Selection > 0 ? characterConversion[base.Selection - 1] : "_";
        base.Description = Hold;
        Info.text = Hold;
        SelectedCharacter = Hold;
    }
    
}