using UnityEngine;

using System;
using System.Collections;

public class cameraControlll : MonoBehaviour {
    [Header("camera config")]
    public Transform target;
    public Vector3 offset = new Vector3(0, 5, -10);

    [Header("follow config")]
    public float smoothSpeed = 0.125f;
    public float collisionBuffer = 0.2f;
    public LayerMask obstacleLayers;

    // private data
    private Vector3 desiredPosition;

    void LateUpdate() {
        if (!target) 
            return;

        desiredPosition = target.position + offset;

        RaycastHit hit;
        Vector3 direction = desiredPosition - target.position;
        float distance = direction.magnitude;

        if (Physics.Raycast(target.position, direction.normalized, out hit, distance, obstacleLayers))
            desiredPosition = hit.point - direction.normalized * collisionBuffer;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.LookAt(target);
    }
}
