using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

public class nockbackTest : MonoBehaviour {

    [Header("stats")]
    public float explosionForce = 100;

    [Header("information")]
    public int cooldownTime = 1;
    public bool cooldown = true;
    public bool canExplode = true;
    public string targetTag;

    void OnTriggerStay(Collider collision) {
        if (collision.gameObject.CompareTag(targetTag) && (canExplode || !cooldown)){
            canExplode = false;
            
            Vector3 explosionPoint = transform.position;
            Vector3 playerPos = collision.gameObject.transform.position;

            Vector2 originalForce = new Vector2();
            originalForce.x = playerPos.x - explosionPoint.x;
            originalForce.y = playerPos.y - explosionPoint.y;             

            Vector2 forceDirection = new Vector2();
            forceDirection = originalForce * originalForce; 

            Vector2 forcePercent = new Vector2();
            float totalForce = forceDirection.x + forceDirection.y;

            forcePercent.x = Math.Abs(forceDirection.x / totalForce);
                if (originalForce.x < 0) forcePercent.x = forcePercent.x * -1;
            forcePercent.y = Math.Abs(forceDirection.y / totalForce);
                if (originalForce.y < 0) forcePercent.y *= -1;

            Vector3 forceApplied  = new Vector3();
            forceApplied.x = explosionForce * forcePercent.x;
            forceApplied.z = explosionForce * forcePercent.y;

            // impact frames
            // sys.system.impact(this, 0.25f, 0.2f);

            collision.gameObject.GetComponent<Rigidbody>().AddForce(forceApplied, ForceMode.Impulse);

            switch(targetTag) {
                case "Player":
                    collision.gameObject.GetComponent<PlayerController>().UnSmooth(1f);

                    break;
                case "enemy":
                    collision.gameObject.GetComponent<MainMenace>().UnSmooth(1f);

                    break;
            }

            canExplode = true;
        }
    }
}
