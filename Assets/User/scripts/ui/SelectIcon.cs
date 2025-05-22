using UnityEngine;

public class SelectIcon : MonoBehaviour {
    [Header("components")]
    public SpriteRenderer spriteRenderer;

    [Header("sprites")]
    public Sprite[] iconsyayy;

    [Header("config")]
    public int Chance = 100;

    void Start() {
        // grab components
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (Random.Range(0, Chance) == 0)
            spriteRenderer.sprite = iconsyayy[Random.Range(0, iconsyayy.Length - 1)];
    }

}