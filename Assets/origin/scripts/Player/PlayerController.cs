using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {
    [Header("Movement")]
    public float moveSpeed = 5f;

    public Vector2 Force;
    public Vector3 Velocity = Vector3.zero;
    public float jumpGravity = 5f;
    public float maxJumps = 1f;
    public float jumps;

    public bool SmoothMovement = true;
    public bool CanMove = true;


    [Range(0, .3f)] public float MovementSmoothing = .05f;

    [Header("Componenets")]
    public Rigidbody rb;
    public GameObject attackComp;
    public Animator anim;
    public TMP_InputField commandBar;
    public GameObject commandHolder;
    public GameObject pauseMenu;
    public GameObject pointerPrefab;
    public GameObject pointerHold;
    public GameObject hitDisplay;

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
    public float healModifier = .25f;
    public float GuardModifier;
    public float speed;
    public float defence;

    public bool dead;

    [Header("gravity")]
    public float groundPoundGravity = 50f;

    [Header("extra")]
    // pointers
    public bool pointer;

    // dash
    public bool dash;
    public bool dashing = false;
    [SerializeField] public float dashForceModifier = 5f;

    // void damage
    public bool fallDamage = false;
    public bool voidLifeSteal = false;

    // jump
    public bool canJump = false;

    // auto attack
    public bool autoAttack = false;

    // cashs
        // movement smoothing
        [Range(0, .3f)] private float MovementSmoothingStateCache;

    // states
    private float lastAnim;

    // powerups
    [Header("powerUps")]
    public List<string> act;

    protected virtual void OnEnable() {
        attackComp.GetComponent<attackrest>().damage = baseAttackDamage;
        jumps = maxJumps;
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

        // powerups
        string[] powerupsHOLD = PlayerPrefs.GetString("powerups", "").Split(",");
        foreach (string key in powerups.full.truePowerups().Keys) Debug.Log(key);
        
        foreach (string powerup in powerupsHOLD) {
            Debug.Log(powerup);

            if (powerup != ""){
                act.Add(powerup);
                
                if (powerups.full.truePowerups().ContainsKey(powerup)) {
                    Debug.Log($"applying {powerup}");
                    powerups.full.truePowerups()[powerup].action(this);
                    Debug.Log($"applied {powerup}");
                }
            }
        }
    }

    protected virtual void Update() {
        // if dead
        if (dead) {
            if (eevee.input.Collect("MenuSelect"))
                SceneManager.LoadScene("MainMenu");

            // end update loop if dead
            return;
        }

        // dont do anything while paused
        if (Time.timeScale == 0)
            return;

        // health fix
        if (health > maxHealth)
            health = maxHealth;

        // calculate force
        Force = new Vector2(eevee.input.CheckAxis("right", "left"), eevee.input.CheckAxis("up", "down"));

        if (Force.x != 0 && Force.y != 0 && Force.x == Force.y)
            Force /= 2;

        // apply movement
        if (CanMove)
            ApplyMovement();
        
        if (eevee.input.Collect("attack") || autoAttack)
            Attack();
        
        // ground pound
        if (eevee.input.Grab("ground-pound")) {
            rb.linearVelocity -= new Vector3(0, groundPoundGravity, 0);
        }

        // jump
        if (eevee.input.Grab("jump") && jumps >= 1 && canJump) {
            rb.linearVelocity += new Vector3(0, jumpGravity, 0);
            jumps--;
        }

        if (eevee.input.Collect("commandBarOpen") && PlayerPrefs.GetString("dev", "false") == "true")
            openCommandBar();
        
        if (eevee.input.Collect("pause"))
            openPauseMenu();

        if (dash && eevee.input.Collect("dash"))
            Dash();

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

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("floor"))
            jumps = maxJumps;
    }

    void ApplyMovement() {
        float tmpspeed = 1 + (speed / 50);
        if(tmpspeed <= 0) tmpspeed = Mathf.Clamp(Mathf.Abs(tmpspeed), 0.1f, 10000) * -1f;
        else tmpspeed = Mathf.Clamp(Mathf.Abs(tmpspeed), 0.1f, 10000);
    
        if (SmoothMovement) {
            Vector3 targetVelocity = new Vector3(Force.x * moveSpeed * tmpspeed, rb.linearVelocity.y, Force.y * moveSpeed * tmpspeed);
            rb.linearVelocity = Vector3.SmoothDamp(rb.linearVelocity, targetVelocity, ref Velocity, MovementSmoothing);
        } else {
            rb.AddForce(new Vector3(Force.x * moveSpeed * tmpspeed, 0f, Force.y * moveSpeed * tmpspeed));
        }
    }

    void openCommandBar() {
        if (!commandHolder.activeSelf){
            Time.timeScale = 0f;
            commandHolder.SetActive(true);
            commandBar.ActivateInputField();
        }
    }

    void openPauseMenu() {
        if (!pauseMenu.activeSelf) {
            pauseMenu.SetActive(true);
            pauseMenu.GetComponent<Animator>().Play("pauseMenuRise");
        }
    }

    void Attack() {
        if (BaseAttackStamina >= BaseAttackCost && !attackComp.activeSelf){
            attackComp.SetActive(true);
            BaseAttackStamina-=BaseAttackCost;
        }
    }

    public virtual void increaseRoomIndex() {
        room++;
    }

    public void Die() {
        if (!dead) {
            GameObject loadingScreen = GameObject.Find("Canvas").transform.Find("deathScreen").gameObject;
            loadingScreen.SetActive(true);
            loadingScreen.GetComponent<Animator>().Play("deathscreenfadein");
            dead = true;
        }
    }

    public void Dash() {
        if (dashing) return;

        dashing = true;
        Vector3 dashForce = new Vector3(Force.x * dashForceModifier, rb.linearVelocity.y, Force.y * dashForceModifier);
        rb.AddForce(dashForce, ForceMode.Impulse);
        StartCoroutine(Dashing());
    }

    public void Damage(float damage, string type = "") {
        if (type == "void" && fallDamage) return;

        if (guarding)
            health -= (damage / (1 + (defence / 50)) / GuardModifier);
        else
            health -= damage / (1 + (defence / 50));

        SpawnHitDisplay(-(damage / (1 + (defence / 50))));

        if (health <= 0)
            Die();
    }

    public virtual void Heal(float Heal, bool modified = false) {
        if (modified) health += Heal;
        else health += Heal * healModifier;

        float displayHealth = modified ? Heal : Heal * healModifier;

        SpawnHitDisplay(displayHealth);

        if (health <= 0)
            Die();
    }

    public void SpawnHitDisplay(float damage) {
        GameObject tmpHitDisplay = Instantiate(hitDisplay);
        tmpHitDisplay.transform.SetParent(transform.parent);
        tmpHitDisplay.transform.localPosition = transform.localPosition;

        tmpHitDisplay.GetComponent<Rigidbody>().linearVelocity = rb.linearVelocity;
        tmpHitDisplay.GetComponent<hitDisplay>().damage = damage;
    }

    public void SpawnPointer(GameObject Target) {
        GameObject TMPointer = Instantiate(pointerPrefab);
        TMPointer.transform.parent = pointerHold.transform;
        
        TMPointer.GetComponent<pointer>().player = transform.gameObject;
        TMPointer.GetComponent<pointer>().Target = Target;

        TMPointer.transform.localPosition = Vector3.zero;
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
    public IEnumerator Dashing(){
        yield return new WaitForSeconds(0.5f);
        dashing = false;
    }
}