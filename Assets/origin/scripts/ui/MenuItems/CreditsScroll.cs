using UnityEngine;

public class CreditsScroll : ButtonIsh {
    public Vector3 move;

    protected override void Update() {
        base.Update();

        if (base._Selected == transform.gameObject){
            transform.GetChild(0).localPosition = Vector3.Lerp(transform.GetChild(0).localPosition, transform.GetChild(0).localPosition + move, Time.deltaTime * smoothSpeed);
        }
    }
}