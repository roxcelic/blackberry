using UnityEngine;

public class CreditsScroll : MenuItems {
    public Vector3 move;

    protected override void Update() {
        base.Update();

        if (base.MenuRoot.SelectedItem == transform.gameObject){
            transform.GetChild(0).localPosition = Vector3.Lerp(transform.GetChild(0).localPosition, transform.GetChild(0).localPosition + move, Time.deltaTime * smoothSpeed);
        }
    }
}