using UnityEngine;

public class enemyclass : MonoBehaviour {
    [Header("movement/health")]
    public float menacecooldown = 1f;
    public float enmhealth = 5.0F;
    public float enmmovspeed = 3.0F;
    public float enmatk = 3.0F;
    public int mindist = 1;
    public float lastusedtime;
    
    [Header("components")]
    public GameObject dmgsphere;
    public Rigidbody rb;
    public GameObject playerref;
    public GameObject playerobj;
    public string menacestate;
    public PlayerController arianascript;

    // called at the first frame of the scene
    protected virtual void Start() {
        arianascript = playerobj.GetComponent<Ariana>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // called every frame once per frame
    protected virtual void Update() {
        switch (arianascript.playerstate){
            case "neutral":
                ChasePlayer();

                break;
            case "combat":
                menacestate = "offensive";
                DefendSelf();

                break;
            case "guarding":
                if(Random.Range(1,2) == 2)
                    ChasePlayer();
                else
                    DefendSelf();

                // im not too sure what this does yet
                if (arianascript.playerstate == "neutral")
                    ChasePlayer();
            
                break;
        }

        // if stood stiil change state to neutral
        if (rb.linearVelocity != new Vector3(0,0,0))
            menacestate = "neutral";

        // why are you freezing the rotation like this
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    // blank method until a base movement is required
    public virtual void ChasePlayer() {}

    // 
    public void DefendSelf(){
        menacestate = "defending";
        enmmovspeed = 0;
    }

    // damaging function
    public void Mtookdmg(float damage = 2.0f){
        enmhealth -= 2.0f;
        enmmovspeed = 0;

        if(enmhealth <= 0)
            Destroy(gameObject);
    }

}
