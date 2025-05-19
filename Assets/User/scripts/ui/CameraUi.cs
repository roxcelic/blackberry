using UnityEngine;

public class CameraUi : MonoBehaviour {
    
    // the camera target
    public MenuController MenuRoot;
    private Transform target;

    [Header("camera Config")]
    public float smoothSpeed = 5f;

    void Update() {
        target = MenuRoot.SelectedItem.transform;

        if (target != null) {
            Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);

            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * smoothSpeed);
        }
    }
}
