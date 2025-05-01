using UnityEngine;

public class cameraControlll : MonoBehaviour {
    public float collisionSensitivity = 0.5f;
    public Transform target;
    public Vector3 Distance;

    void Start() {
        target = GameObject.Find("Player(Clone)").transform;
    }

    void LateUpdate() {
        RaycastHit hit;

        if (Physics.Raycast(target.position, transform.position - target.position, out hit, Vector3.Distance(transform.position, target.position + Distance))) {
            Vector3 newCameraPosition = hit.point + (transform.position - target.position).normalized * collisionSensitivity;
            transform.position = newCameraPosition;
        }
    }
}
