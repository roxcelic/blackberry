using UnityEngine;

public class DecorControl : MonoBehaviour {
    public Quaternion roationLock;

    void OnEnable() {
        transform.GetComponent<Animator>().Play("base-decor-rise");
    }

    void Update() {
        if (transform.rotation != roationLock) transform.rotation = roationLock;
    }
}