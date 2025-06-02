using UnityEngine;

public class ChangePlayerPrefValue : ButtonIsh {

    public string playerPrefName;

    [Header("sprites")]
    public Sprite activeSprite;
    public Sprite inactiveSprite;

    protected override void Start() {
        base.Start();

        if(PlayerPrefs.GetString(playerPrefName, "false") == "true"){
            base.spriteRenderer.sprite = activeSprite;
            base.Selected = true;
        } else {
            base.spriteRenderer.sprite = inactiveSprite;
            base.Selected = false;
        }
    }

    protected override void Action() {
        base.Action();

        if (base.Selected){
            base.spriteRenderer.sprite = activeSprite;
            PlayerPrefs.SetString(playerPrefName, "true");
        } else {
            base.spriteRenderer.sprite = inactiveSprite;
            PlayerPrefs.SetString(playerPrefName, "false");
        }

    } 


}