using UnityEngine;

public class pauseMenuManager : MonoBehaviour {
    public int selected;
    private int lastSelected;
    public int min;
    public int max;

    public GameObject held;

    void Start() {
        max = transform.childCount - 1;

        selected = max / 2;

        held = transform.GetChild(selected).gameObject;
        held.GetComponent<PauseMenuItem>().Hover();
    }

    void Update() {
        if (eevee.input.Collect("MenuLeft")) selected--;
        if (eevee.input.Collect("MenuRight")) selected++;

        selected = Mathf.Clamp(selected, min, max);

        if (lastSelected != selected) {
            held.GetComponent<PauseMenuItem>().Leave();
            held = transform.GetChild(selected).gameObject;
            held.GetComponent<PauseMenuItem>().Hover();
        }

        lastSelected = selected;
    }

}