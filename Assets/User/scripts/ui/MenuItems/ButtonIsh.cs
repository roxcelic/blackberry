using UnityEngine;

public class ButtonIsh : MenuItems {

    [Header("buttonConfig")]
    public bool Selected;

    protected override void Action() {
        Selected = !Selected;
    }
}