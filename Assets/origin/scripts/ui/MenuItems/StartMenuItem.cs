using UnityEngine;

public class StartMenuItem : MenuItems {
    public MenuController NextMenu;
    public CameraUi Camera;

    protected override void Action() {
        NextMenu.active = true;
        Camera.MenuRoot = NextMenu.GetComponent<MenuController>();
        base.MenuRoot.active = false;
    }
}
