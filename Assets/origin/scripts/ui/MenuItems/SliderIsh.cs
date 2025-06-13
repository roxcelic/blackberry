using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

public class SliderIsh : MenuItems {
    [Header("SliderConfig")]
    public int Selection = 0;
    int privSelection = 0;
    public bool SelectedTrap;

    public int Min;
    public int Max;
    
    public TMP_Text Info;
    
    public Vector3 extrascale;

    public string Prefix;

    [Range(0, 5f)] public float modifier;

    protected override void Action() {
        SelectedTrap = !SelectedTrap;

        if (SelectedTrap) {
            base.SelectedScale += extrascale;
            base.MenuRoot.canMove = false;
        } else{
            base.MenuRoot.canMove = true;
            base.SelectedScale -= extrascale;
        }
    }

    protected override void Update() {
        base.Update();

        if (SelectedTrap){

            int modifier = eevee.input.CollectAxis("MenuRight", "MenuLeft");
            Selection += modifier;

            if (Selection < Min)
                Selection = Min;
            if (Selection > Max)
                Selection = Max;
            
            if (Selection != privSelection)
                write();
            
            privSelection = Selection;
        }
    }

    protected virtual void write() {
        string Hold = $"{Prefix} \n {Selection.ToString()}";
        base.Description = Hold;
        Info.text = Hold;
    }
}