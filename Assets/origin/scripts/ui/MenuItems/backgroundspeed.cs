using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

public class backgroundspeed : SliderIsh {
    [Header("backgroundspeed config")]
    public string SelectedCharacter = "_";
    
    protected override void Start() {
        base.Start();
        base.Selection = PlayerPrefs.GetInt("skyboxspeed", 0);

        base.Max = 100_000_000_0;
    }

    protected override void write() {
        Info.text = base.Selection.ToString();

        if (base.Selection == 0)
            Info.text = SelectedCharacter;
        
        PlayerPrefs.SetInt("skyboxspeed", base.Selection);
    }
    
}