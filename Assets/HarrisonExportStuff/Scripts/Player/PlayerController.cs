//base playerclass. used for Ariana. 
using UnityEngine;
using eevee;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(CharacterController))]
public class PlayerControllerH : MonoBehaviour
{
    // I use lowercase for my variables. I can change this if you would like
    // The player has three states, attacking, defending and neutral. 
    // I use enums for these- Attacking, Defending, Neutral
    // public variables
    [Header("Stats")]
    protected float playerspeed;
    protected float startplayerspeed; //this is here when we want to return the player's speed to what it was at the start
    protected float playerhealth;
    protected float steam;//steam bar used for abilites
    protected float lastdash;//cooldown for dashing
    protected float lastdef;//cooldown for defence
    protected float deftime = 0.5F;//time between cooldown
    protected float firerate = 1f;//time between firing
    protected float lastusedtime;
    protected bool overdrivebool;//is overdriving?


    [Header("Components")]
    public Animator animator;
    public SpriteRenderer playrend;
    public Rigidbody rb;
    public int room;
    public GameObject overdriveeffect;
    public GameObject shieldsprite;
    public Slider healthbar;//UI for health
    public Slider steambar;//UI for steam

    [Header("Other References")]
    public GameObject projectileref; //this is the reference to the player's projectile.
    public GameObject hitboxref; //this is the reference to the player's melee hitbox.
    public Sprite defending;
    public Sprite attacking;//UI sprites for attacking, defending, neutral. I could have put these in a list probably but didn't wanna deal with referring to individual like indexes
    public Sprite neutral;
    public Image stateimage;//state image for the UI

    [Header("Melee Combat")]
    public float offsetval = 1.0f;//the offset at which the like melee hitbox spawns
    protected float swordcooldown = 0.5f;//melee sword thing. is the like time between attacks.
    protected Quaternion atkrotation;//used for melee
    protected bool candef = true;




    public enum PlayerState//this is an enum for the player's state. 
    {
        Defending,
        Attacking,
        Neutral,
    }
    public PlayerState playerstate;



    protected virtual void Start()
    {
        shieldsprite.GetComponent<SpriteRenderer>().enabled = false;//turning the shield image off, its used when player defends to give a visual image of like them defending??
        rb = gameObject.GetComponent<Rigidbody>();
        playerstate = PlayerState.Neutral;
        ChangeState();//setting player to neutral
        lastdash = Time.time;
    }

    protected virtual void Update()
    {

        if ((steam <= 10) && (playerstate == PlayerState.Neutral) && (overdrivebool == false))//normal status- if steam is less than ten, player is neutral and they aren't overdriving:
        {
            steam += Time.deltaTime;//gain steam 
            steambar.value = steam;
        }
        if ((steam > 0) && overdrivebool == true)//if steam is NOT 0 and the player is overdriving
        {
            steam -= Time.deltaTime * 2;
            steambar.value = steam;//decrease steam fast
            firerate = 0.1f;//tehee much less fire rate so they shoot FASST
            if (steam < 1)
            {
                overdrivebool = false;//resets it after
                playerstate = PlayerState.Neutral;
                firerate = 1f;
            }
        }
        if (steam >= 9)//changes bar colour based on how much steam is there; it is BLUEUEEEUEUE when they can overdrive
        {
            steambar.fillRect.GetComponent<Image>().color = new Color(0, 0, 1);
        }
        else if (steam < 9)
        {
            steambar.fillRect.GetComponent<Image>().color = new Color(0.6470588f, 0.1952568f, 0);
        }

        //movement stuff
        //basic eevee axis
        float Horizontal = eevee.input.CheckAxis("left", "right");
        float Vertical = eevee.input.CheckAxis("down", "up");
        // a diagonal direction speed fix
        if (Horizontal != 0f && Vertical != 0f)
        {
            Horizontal = Horizontal > 0 ? 0.5f : -0.5f;
            Vertical = Vertical > 0 ? 0.5f : -0.5f;
        }
    
        // actual movement
        //so this is the rb movement. it worked well for me. idk if this is the best method, i had some issues with linearvelocity but its there just in case
        Vector3 move = new Vector3(Horizontal * -1, 0, Vertical * -1);//it is offsetted by -1 cuz the input system is newer in the new version, and it reversed my inputs at the start
        rb.MovePosition(transform.position + move * playerspeed * Time.deltaTime);
        //rb.linearVelocity = move * playerspeed * Time.deltaTime;

        //this is here cuz its what I was using, just in case I wanted to use it again (for convinience)
        //controller.Move(move * Time.deltaTime * playerspeed);


        //this is like, a slight delay after the player has defending, so they can't just spam it
        if (Time.time > lastdef + 0.25f)
        {
            candef = true;
        }
        else
        {
            candef = false;
        }

        //
        if (move != Vector3.zero)
        { //if moving, player is neutral, isnt defending/attacking
            playerstate = PlayerState.Neutral;
            ChangeState();
            playrend.color = new Color(1f, 1f, 1f, 1f);
            shieldsprite.GetComponent<SpriteRenderer>().enabled = false;
            if (overdrivebool == true)
            {
                playerspeed = startplayerspeed * 2f;
            }
            else
            {
                playerspeed = startplayerspeed;
            }

        }//defending
        if (eevee.input.Check("HDefend") && candef == true && steam > 2f)
        {
            playerstate = PlayerState.Defending;
            ChangeState();
            playerspeed = startplayerspeed / 2;
            shieldsprite.GetComponent<SpriteRenderer>().enabled = true;//shows shield pixel thing
            playrend.color = new Color(0f, 0f, 1f, 1f);//makes blue
            lastdef = Time.time;
        }
        if (playerstate == PlayerState.Defending)
        {
            steam -= 4 * Time.deltaTime;
            steambar.value = steam;
            //reduces steam whilst you're defending so you cannot do it forever.
            if (steam < 0)
            {
                candef = false;
                playerstate = PlayerState.Neutral;
                ChangeState();
                playrend.color = new Color(1f, 1f, 1f, 1f);
                shieldsprite.GetComponent<SpriteRenderer>().enabled = false;
                //when it runs out
                lastdef = Time.time;
            }
        }

        //melee combat- works on raycasts, basically shoots a ray at the floor, if it hits something, the player "swipes" in that direction (spawns a hitbox in that direvtion)
        //got this method from some unity docs, tweaked it a lil ages ago
        //probably shit
        if (eevee.input.Check("HMeleeAttack") && (Time.time > lastusedtime + swordcooldown) && playerstate != PlayerState.Defending)
        {
            Ray mousepoint = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(mousepoint, out hit, 100f))
            {
                playerstate = PlayerState.Attacking;
                ChangeState();
                //Debug.Log("Hit something woah");
                Vector3 raydirection = (hit.point - transform.position).normalized;
                atkrotation = Quaternion.LookRotation(raydirection);
                Vector3 swordpos = transform.position + raydirection * offsetval;
                atkrotation = Quaternion.Euler(90, atkrotation.eulerAngles.y, 0);
                Instantiate(hitboxref, swordpos, atkrotation);
                lastusedtime = Time.time;//basic cooldown

            }
        }
        //ranged combat- similar to melee, costs steam to shoot, and shoots something that moves towards when the ray hit. I did this because I want to be able to shoot flying things in the air, in a 3D space
        //thats kinda obsolete now, as I didnt asdd flying enemies
        else if (eevee.input.Check("HRangedAttack") && (Time.time > lastusedtime + firerate) && steam > 1 && playerstate != PlayerState.Defending)
        {
            playerstate = PlayerState.Attacking;
            ChangeState();
            if (overdrivebool == true)
            {
                steam -= 0.05f;
                steambar.value = steam;//super cheap to do 
            }
            else if (overdrivebool == false)
            {
                steam -= 1;
                steambar.value = steam;
            }
            Ray Mousepoint = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(Mousepoint, out hit, 100f))
            {
                Vector3 raydirection = (hit.point - transform.position).normalized;
                float angle = Mathf.Atan2(raydirection.y, raydirection.x) * Mathf.Rad2Deg;
                atkrotation = Quaternion.LookRotation(raydirection, Vector3.up);
                Vector3 swordpos = transform.position + raydirection * offsetval;
                Instantiate(projectileref, swordpos, atkrotation);
                lastusedtime = Time.time;//basic cooldown
            }
        }
        else if (eevee.input.Check("HDash") && steam > 3f && (Time.time > lastdash + 0.5f))//dash, really simple, basic. lots of conditions
        {
            rb.AddForce(move * 1.5f, ForceMode.Impulse);//really basic and shit dash
            steam -= 3f;
            steambar.value = steam;
            lastdash = Time.time;
        }
        else if (eevee.input.Check("HOverdrive"))//this is Overdrive, basically makes it so steam drains fast, but players move fast and shoot faster. im thinking of making it so they take more damage in this state. 
        {
            Debug.Log("Pressed E");
            if (steam >= 9)
            {
                Instantiate(overdriveeffect, transform.position, Quaternion.identity);
                Debug.Log("Overdrive saljkdhasdjkfsdg");
                overdrivebool = true;
            }
        }

        // pause
        if (eevee.input.Check("pause")) SceneManager.LoadScene("MainMenu");

        // rotation lock, probably very inneficient
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
    public void playertookdamage(int dmg)//reduces health by parameter
    {
        playerstate = PlayerState.Neutral;
        ChangeState();//"shocks" them out of like their state
        playerhealth -= dmg;
        healthbar.value = playerhealth;
        if (playerhealth <= 0)
        {
            //Debug.Log("Tried to Kill player");//put whatever kill function here, obv this can be improved
            SceneManager.LoadScene(0);//wow amazing im so smart
        }
    }
    public void Heal()//heals player whenever they pick up a health pickup
    {
        playerhealth += 4;
        if (playerhealth > 12)
        {
            playerhealth = 12;
        }
        //Debug.Log(playerhealth);
        healthbar.value = playerhealth;
    }
    public void ChangeState()//very basic, changes image in UI to a different icon
    {
        if (playerstate == PlayerState.Defending)
        {
            stateimage.sprite = defending;
        }
        else if (playerstate == PlayerState.Attacking)
        {
            stateimage.sprite = attacking;
        }
        else
        {
            stateimage.sprite = neutral;
        }
    }
    public void RefuelSteam()//refuels steam. could use it whenever the player clears a room? thats what I used in my version
    {
        overdrivebool = false;
        if (steam < 7)
        {
            steam = 7;
            steambar.value = steam;
            playerstate = PlayerState.Neutral;
        }

    }
}