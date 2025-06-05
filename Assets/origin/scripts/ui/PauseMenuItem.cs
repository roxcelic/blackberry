using UnityEngine;
using UnityEngine.UI;

public class PauseMenuItem : MonoBehaviour {
    public bool selected = false;

    [Header("sprites")]
    public Sprite selectedSprite;
    public Sprite unSelectedSprite;

    [Header("components")]
    public Image spriteRenderer;

    [Header("config")]
    public Vector3 growSize;
    public float smoothSpeed = 5f;

    protected virtual void Start() {
        spriteRenderer = GetComponent<Image>();
    }

    protected virtual void Update() {
        Vector3 tmpSizeGoal = growSize;

        if (!selected)
            tmpSizeGoal = new Vector3(1, 1, 1);

        transform.localScale = Vector3.Lerp(transform.localScale, tmpSizeGoal, smoothSpeed);

        if (selected && eevee.input.Collect("MenuSelect")) {
            Press();
        }
    }

    protected virtual void Press() {
        Debug.Log("no action injected");
    }

    public void Hover() {
        ChangeSprite(selectedSprite);
        selected = true;
        HoverAction();
    }

    public void Leave() {
        ChangeSprite(unSelectedSprite);
        selected = false;
        HoverActionLeave();
    }

    void ChangeSprite(Sprite newSprite) {
        spriteRenderer.sprite = newSprite;
    }

    protected virtual void HoverAction() {
        Debug.Log("no action injected");
    }
    protected virtual void HoverActionLeave() {
        Debug.Log("no action injected");
    }

}