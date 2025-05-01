using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {

    // public variables
        // what isnt public 
    [Header("movement/health")]
    public float playerSpeed = 5.0f;
    public float playerHealth = 10F;

    [Header("componenets")]
    public CharacterController controller;  
    public GameObject otherobjref;
    public GameObject dmgbox;
    public Rigidbody temprb;


    [Header("player state")]
    public float spawnOffset = 1.0f;
    public float lastusedtime;
    public float swordcooldown = 1.0f;
    public string facingdir;
    public Vector3 weaponswing;
    public Quaternion atkrotation = Quaternion.Euler(90f, 0f, 0f); //y, x, z
    public SpriteRenderer playrend;
    public float deftime = 3F;
    public string playerstate;

    [Header("extra things i added")]
    private float startz;


    protected virtual void Start() {
        playerSpeed = 5.0f;
        controller = GetComponent<CharacterController>();

        startz = transform.position.z;
    }

    protected virtual void FixedUpdate() {
        float Horizontal = eevee.input.CheckAxis("left", "right");
        float Vertical = eevee.input.CheckAxis("down", "up");

        // a diagonal direction speed fix
        if (Horizontal != 0f && Vertical != 0f){
            Horizontal = Horizontal > 0 ? 0.5f : -0.5f;
            Vertical = Vertical > 0 ? 0.5f : -0.5f;
        }

        // movement
        Vector3 move = new Vector3(Horizontal, startz, Vertical);
        controller.Move(move * Time.deltaTime * playerSpeed);


        // This does seem like a large chunk for your work so i wouldnt like to change it but some ways to optimise it
            // make use of a switch case at least once please 🙏
            // assign variables if you are going to be grabbing a value more than once
            // the left click input will be customised later so please make it so that the input isnt tied to the action
        // there are ofc more but i have to edit some other scripts
        if (move != Vector3.zero) {
            gameObject.transform.forward = move;
            playerstate = "neutral";
            playrend.color = new Color(1f, 1f, 1f, 1f);
            playerSpeed = 5.0F;
        } else if (eevee.input.Check("guard") && move == Vector3.zero){
            playerstate = "guarding";
            playerSpeed = 0.0F;
            playrend.color = new Color(0f, 0f, 1f, 1f);
        } else if(eevee.input.Check("attack") && (Time.time > lastusedtime + swordcooldown) && false){
            Ray Mousepoint = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // hit
            if (Physics.Raycast(Mousepoint, out hit, 50f)) {
                Vector3 spritedir = (hit.point - transform.position).normalized;
                float angle = Mathf.Atan2(spritedir.y, spritedir.x) * Mathf.Rad2Deg;
                atkrotation = Quaternion.Euler(90f, angle, 0f);
                Vector3 SwordPos = transform.position + spritedir * spawnOffset;
                Instantiate(dmgbox, SwordPos, (atkrotation));
                lastusedtime = Time.time;
            }
        }

        //if (eevee.input.Check("pause"))
        //    SceneManager.LoadScene("MainMenu");


        // rotation lock, probably very inneficient
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        //Deftime();
    }

    // actions upon a collision with the player, 
        // on enter will make it so the enemy can be pushed against a wall and be prevented from attacking so
    void OnCollisionEnter(Collision otherobj) {
        if (otherobj.gameObject.tag == "Enemy" && (playerstate == "guarding")) {
            otherobjref = otherobj.gameObject;
            temprb = otherobjref.GetComponent<Rigidbody>();
            temprb.AddForce(-transform.forward * 50f, ForceMode.Impulse);
        }
    }


    void Deftime(){
        while ((playerstate == "guarding") && (deftime >= 0)){
            playrend.color = new Color(0f, 0f, 1f, 1f);
            deftime -= 1 * Time.deltaTime;
            playerSpeed = 0.0F;
            if (deftime <= 0){
                playerstate = "neutral";
                playrend.color = new Color(1f, 1f, 1f, 1f);
                playerSpeed = 5.0F;
            }
            if(playerstate != "guarding"){
                deftime = 3F;        
            }
        }
    }

    // player damage, add it so when you take damage you can input a variable so certain enemys could do more damage
    public void playertookdamage(float damage = 2.0f) {
        playerHealth -= damage;

        // playerdeath
        if(playerHealth <= 0){
            Debug.Log("Tried to Kill player");
        }
    }
}