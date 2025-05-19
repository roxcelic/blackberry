using UnityEngine;

public class loadMenuManager : MonoBehaviour {
    public void Disable() {
        gameObject.SetActive(false);
    }
    public void Enable() {
        gameObject.SetActive(true);
    }
}