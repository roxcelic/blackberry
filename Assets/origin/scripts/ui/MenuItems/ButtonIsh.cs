using UnityEngine;

public class ButtonIsh : MenuItems {

    [Header("buttonConfig")]
    public bool _Selected;

    protected override void Action() {
        _Selected = !_Selected;
    }
}