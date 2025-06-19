using UnityEngine;

public class test : MonoBehaviour {
    void Start() {
        Debug.Log(sys.var.test);
        sys.var.test = "hi the second time";
        Debug.Log(sys.var.test);
    }
}
