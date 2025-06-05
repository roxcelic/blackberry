using UnityEngine;

public class pauseMenuResume : PauseMenuItem {
    public GameObject Menu;

    protected override void Start() {
        base.Start();
    }

    protected override void Update() {
        base.Update();
    }

    protected override void Press() {
        Time.timeScale = 1f;
        Menu.GetComponent<Animator>().Play("pauseMenuFall");
    }
}
