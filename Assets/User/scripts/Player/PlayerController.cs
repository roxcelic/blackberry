using UnityEngine;

using System;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {
    [Header("Movement")]
    public float moveSpeed = 5f;

    public Vector2 Force;
    public Vector3 Velocity = Vector3.zero;

    public bool SmoothMovement = true;
    public bool CanMove = true;


    [Range(0, .3f)] public float MovementSmoothing = .05f;

    [Header("Componenets")]
    public Rigidbody rb;
    public GameObject attackComp;
    public Animator anim;

    [Header("attack")]
    public bool hit;
    public bool attackPierce;

    public float BaseAttackStaminaMax;
    public float BaseAttackRecoveryRate;
    public float BaseAttackStamina;
    public float BaseAttackCost;
    public float baseAttackDamage;

    [Header("attack-meter")]
    public float PlayerSpecificMeterMax;
    public float PlayerSpecificMeter;
    public float PlayerSpecificMeterRecoveryRate;
    public float PlayerSpecificMeterUseRate;
    public float PlayerSpecificAttackDamage;


    [Header("stats")]
    public bool guarding;
    public int room = 0;

    public float maxHealth;
    public float health;
    public float GuardModifier;
    public int speed;
    public int defence;

    // cashs
        // movement smoothing
        [Range(0, .3f)] private float MovementSmoothingStateCache;

    // states
    private float lastAnim;

    protected virtual void OnEnable() {
        transform.GetChild(transform.childCount - 1).GetComponent<attackrest>().damage = baseAttackDamage;
    }

    protected virtual void Start() {
        // grab components
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        // initiate variables
        health = maxHealth;
        PlayerSpecificMeter = PlayerSpecificMeterMax;
        BaseAttackStamina = BaseAttackStaminaMax;

        // coroutines
        StartCoroutine(Recover());
    }

    protected virtual void Update() {
        // calculate force
        Force = new Vector2(eevee.input.CheckAxis("right", "left"), eevee.input.CheckAxis("up", "down"));

        if (Force.x != 0 && Force.y != 0 && Force.x == Force.y)
            Force /= 2;

        // apply movement
        if (CanMove)
            ApplyMovement();
        
        if (eevee.input.Collect("attack"))
            Attack();

        if (eevee.input.Check("guard"))
            guarding = true;
        else
            guarding = false;

        // animations
        if (Force.x == 0f && Force.y == -1f){
            anim.Play("Player-Walk-B");
        } else if (Force.x == 0f && Force.y == 1f){
            anim.Play("Player-idle");
        } else if (lastAnim != Force.x){
            switch (Force.x){
                case -1:
                    anim.Play("Player-Flop-L");

                    break;
                case 0:
                    anim.Play("Player-idle");

                    break;
                case 1:
                    anim.Play("Player-Flop-R");

                    break;
            }

            lastAnim = Force.x;
        }

    }

    void ApplyMovement() {
        float tmpspeed = 1 + (speed / 50);
    
        if (SmoothMovement) {
            Vector3 targetVelocity = new Vector3(Force.x * moveSpeed * tmpspeed, rb.linearVelocity.y, Force.y * moveSpeed * tmpspeed);
            rb.linearVelocity = Vector3.SmoothDamp(rb.linearVelocity, targetVelocity, ref Velocity, MovementSmoothing);
        } else {
            rb.AddForce(new Vector3(Force.x * moveSpeed * tmpspeed, 0f, Force.y * moveSpeed * tmpspeed));
        }
    }

    void Attack() {
        if (BaseAttackStamina >= BaseAttackCost){
            attackComp.SetActive(true);
            BaseAttackStamina-=BaseAttackCost;
        }
    }

    public void Die() {
        //Debug.Log("you fucking died");
    }

    public void Damage(float damage) {
        if (guarding)
            health -= (damage / (1 + (defence / 50)) / GuardModifier);
        else
            health -= damage / (1 + (defence / 50));

        if (health <= 0)
            Die();
    }

    public void Heal(float health) {
        health += health;

        if (health >= maxHealth)
            health = maxHealth;
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
            PlayerSpecificMeter += PlayerSpecificMeterRecoveryRate;
            BaseAttackStamina += BaseAttackRecoveryRate;

            if (PlayerSpecificMeter > PlayerSpecificMeterMax)
                PlayerSpecificMeter = PlayerSpecificMeterMax;
            
            if (BaseAttackStamina > BaseAttackStaminaMax)
                BaseAttackStamina = BaseAttackStaminaMax;

            yield return new WaitForSeconds(0.5f);
        }
    }
}