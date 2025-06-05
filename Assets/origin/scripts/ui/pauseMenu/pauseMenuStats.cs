using UnityEngine;

public class pauseMenuStats : PauseMenuItem {
    public GameObject Stats;

    protected override void Start() {
        base.Start();
    }

    protected override void Update() {
        base.Update();
    }

    protected override void HoverAction() {
        Stats.SetActive(true);
    }
    protected override void HoverActionLeave() {
        Stats.SetActive(false);
    }
}