using UnityEngine;

using System;
using System.Collections;

public class DoorBlock : MonoBehaviour {
    public Vector3 targetLower;

    public void RemoveBlockAid() {
        targetLower = new Vector3(transform.position.x, targetLower.y, transform.position.z);
        StartCoroutine(lower());
    }

    public IEnumerator lower() {
        while(transform.position.y - targetLower.y > 0.1f){
            transform.position = Vector3.Lerp(transform.position, targetLower, Time.deltaTime * 2.5f);

            yield return 0;
        }

        Destroy(transform.gameObject);
    }
}