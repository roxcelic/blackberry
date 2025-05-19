using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

public class MainMenace : MonoBehaviour {
    [Header("config")]
    public Vector3 safeDistance = new Vector3(0, 1000, 1000);
    public Transform target;
    public float moveSpeed = 5f;
    [Range(0, 5)] public int PlayerPadding;
    [Range(0, .3f)] public float MovementSmoothing = .05f;

    public bool SmoothMovement = true;
    
    [Header("status")]
    public Vector2 direction;
    public Vector3 Velocity = Vector3.zero;
    public bool active = false;

    [Header("Componenets")]
    public Rigidbody rb;
    public GameObject attackComp;

    [Header("stats")]
    public float maxHealth;
    public float health;
    public float speed;
    public float defence;

    [Header("attack")]
    public float BaseAttackStaminaMax;
    public float BaseAttackRecoveryRate;
    public float BaseAttackStamina;
    public float BaseAttackCost;

    void Start() {
        // grab components
        rb = GetComponent<Rigidbody>();

        // activate stats
        health = maxHealth;
        BaseAttackStamina = BaseAttackStaminaMax;

        // coroutines
        StartCoroutine(findPlayer());
        StartCoroutine(Recover());
    }

    void Update() {
        if (active) {
            Vector3 acting = target.position - transform.position;

            if (acting.x != 0)
                direction.x = acting.x > 0 ? 1 : -1;
            else
                direction.x = 0;
            
            if (acting.z != 0)
                direction.y = acting.z > 0 ? 1 : -1;
            else
                direction.y = 0;

            if (
                Mathf.Abs(acting.x) > PlayerPadding * 2 ||
                Mathf.Abs(acting.z) > PlayerPadding * 2
            )
                ApplyMovement();
            else
                Attack();
        }

        // should die
        if (
            transform.position.y > safeDistance.y || transform.position.y < -safeDistance.y ||
            transform.position.z > safeDistance.z || transform.position.z < -safeDistance.z
        ) Damage(maxHealth * (1 + (defence / 50)));
    }

    void ApplyMovement() {
        float tmpspeed = 1 + (speed / 50);
    
        if (SmoothMovement) {
            Vector3 targetVelocity = new Vector3(direction.x * moveSpeed * tmpspeed, rb.linearVelocity.y, direction.y * moveSpeed * tmpspeed);
            rb.linearVelocity = Vector3.SmoothDamp(rb.linearVelocity, targetVelocity, ref Velocity, MovementSmoothing);
        } else {
            rb.AddForce(new Vector3(direction.x * moveSpeed * tmpspeed, 0f, direction.y * moveSpeed * tmpspeed));
        }
    }

    void Attack() {
        if (BaseAttackStamina >= BaseAttackCost){
            attackComp.SetActive(true);
            BaseAttackStamina-=BaseAttackCost;
        }
    }

    public void Die() {
        Destroy(transform.gameObject);
    }

    public void Damage(float damage) {
        health -= damage / (1 + (defence / 50));

        if (health <= 0)
            Die();
    }

    public IEnumerator findPlayer() {
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Player").Length != 0);
        target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        active = true;
    }

    // ienumerators
    public void UnSmooth(float duration){
        SmoothMovement = false;
        StartCoroutine(delay(duration));
    }
    public IEnumerator delay(float duration){
        yield return new WaitForSeconds(duration);
        SmoothMovement = true;
    }

    public IEnumerator Recover(){
        while(true){
            BaseAttackStamina += BaseAttackRecoveryRate;

            if (BaseAttackStamina > BaseAttackStaminaMax)
                BaseAttackStamina = BaseAttackStaminaMax;

            yield return new WaitForSeconds(0.5f);
        }
    }
}