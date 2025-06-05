using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenuQuit : PauseMenuItem {

    protected override void Start() {
        base.Start();
    }

    protected override void Update() {
        base.Update();
    }

    protected override void Press() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}