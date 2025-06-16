using UnityEngine;
using System.Collections;
//base projectile script. the player projectile and the archer menace projectile inherits from this
public class ProjectileScript : MonoBehaviour
{
    protected float movspeed = 50.0f;//default thing

    protected bool parry = true;//determines if a projectile can be parried or "repelled". Currently, the enemy projectile can be repelled. I want to add more enemies, so im keeping this in later for future. 
    protected float lastparried;
    public GameObject parryeffect;//sparks or whatever, when player parries it
    public int dmg = 2;//inital, default damage 

    protected virtual void Start()
    {
        lastparried = Time.time;
        Destroy(gameObject, 10);//this is here because unfortunatly i couldnt figure out how to make the projectile destroy if it hits terrain, without also deleteing itself because I had a bug
    }
    protected void Update()
    {
        transform.position += transform.forward * movspeed * Time.deltaTime; //moves projectile forwards. When the projectile is spawned, it should be facing the intended direction from the shooter.
    }

    void OnTriggerEnter(Collider colobj)
    {
        if (colobj.gameObject.tag != "Effects")//to make sure it doesn't delete itself when it hits the parry effect on the way back from being parried if that makes sense
        {
            if (colobj.gameObject.tag == "Player" && (Time.time > lastparried + 1f))
            {
                if (colobj.gameObject.GetComponent<PlayerControllerH>() != null)
                {
                    if (colobj.gameObject.GetComponent<PlayerControllerH>().playerstate == PlayerControllerH.PlayerState.Defending)
                    {
                        if (parry == true && Time.time > lastparried + 0.1f)
                        {
                            movspeed = movspeed * -1;
                            lastparried = Time.time;
                        }
                        Instantiate(parryeffect, transform.position, Quaternion.identity);
                    }
                    else
                    {
                        colobj.gameObject.GetComponent<PlayerControllerH>().playertookdamage(dmg);
                        lastparried = Time.time;
                        Destroy(gameObject);

                    }
                }
            }
            else if (colobj.gameObject.tag == "Enemy" && (Time.time > lastparried + 1f))
            {
                if (colobj.gameObject.GetComponent<enemyclass>() != null)
                {
                    colobj.gameObject.GetComponent<enemyclass>().takedamage(dmg, "ranged");
                    Destroy(gameObject);
                }

            }

        }
    }
}

