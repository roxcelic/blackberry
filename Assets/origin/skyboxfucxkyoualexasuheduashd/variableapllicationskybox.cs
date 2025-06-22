using UnityEngine;

public class variableapllicationskybox : MonoBehaviour {
    public Renderer targetRenderer;
    public int defaultValue = 10;
    
    void Start() {
        targetRenderer.material.SetFloat("_speed", PlayerPrefs.GetInt("skyboxspeed", defaultValue) == 0 ? 0.5f : PlayerPrefs.GetInt("skyboxspeed", defaultValue));
    }
}
