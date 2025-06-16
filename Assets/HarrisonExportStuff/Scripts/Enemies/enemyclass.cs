//enemy class! main enemy class.
//has all the functions they need, lmk if it's easier. 
//all stats, too.
//its quite basic- has a lot of functions, each subclass? inheriting class? class that derives from this one, uses different function
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using eevee;
using System.Threading;
using UnityEngine.Timeline;


public class enemyclass : MonoBehaviour
{
    [Header("Movement/Health")]
    protected float enmhealth;//health variable
    protected float normalspeed;//for use like to refer back to what the movement speed is at the start
    protected float movspeed;
    protected Vector3 randompos;// for relocate function
    public int dmg;//damage they do
    protected Vector3 targetpos;

    protected bool ismoving = false;

    [Header("Ranged Combat")]
    public GameObject projectile;
    protected float lasthit;
    public GameObject blood;
    [Header("Melee Combat")]
    protected float cooldown = 1f;
    public GameObject attackhitbox;
    protected int dropchance;
    protected float staggertime = 1f;
    protected float lastusedtime;
    [Header("Components/References")]
    public Slider healthbar;
    public GameObject playerref;
    public Rigidbody rb;
    public GameObject offset;//a gameobject attatched to the enemies, its basically directly in front of them and what I use for melee and ranged spawning logic, probably inefficient
    public GameObject spawneffect;
    public GameObject healthdrop;


    protected virtual void Start()
    {
        Instantiate(spawneffect, transform.position, Quaternion.Euler(-90, 0, 0));//spawn in like, spawn effect. comment this out if you don't want it
        rb = gameObject.GetComponent<Rigidbody>();//gets rigidbody
        healthbar.value = enmhealth;
        playerref = GameObject.FindGameObjectWithTag("Player");//idk is this efficient? i thought so
    }
    protected virtual void Update()
    {
        if (Time.time > lasthit + 0.5f)//stagger thing
        {
            movspeed = normalspeed;
        }
        transform.LookAt(new Vector3(playerref.transform.position.x, transform.position.y, playerref.transform.position.z));//looks at player constantly, and then moves to their position. 
    
    }

    public void takedamage(int dmg, string damager)
    {
        Instantiate(blood, transform.position, Quaternion.identity);
        enmhealth -= dmg;
        healthbar.value = enmhealth;
        if (enmhealth <= 0)
        {
            dropchance = Random.Range(1, 4);
            // Debug.Log(dropchance);
            if (dropchance == 2)
            {
                Instantiate(healthdrop, transform.position, Quaternion.identity);//randomly decides if it wants to drop a health pickup upon death
            }
            Destroy(gameObject);
        }
        if (damager == "melee") {
            movspeed = 0;
            lasthit = Time.time;
        }
    }

    public void RangedAttack()//shoots thing. Will shoot at offset, and at enemy rotation. Used for Archer. Will shoot in direction they face, which is towards the player 
    {
        transform.LookAt(new Vector3(playerref.transform.position.x, 0, playerref.transform.position.z));
        Instantiate(projectile, offset.transform.position, transform.rotation);
    }

    public void NormalMeleeAttack()//standard melee attack, 2 second cooldown atm. basically just spawns in an attack hitbox at the offsets position. 
    {
        if (Time.time > lastusedtime + 1)
        {
            Vector3 attackpos = offset.transform.position;
            Instantiate(attackhitbox, attackpos, Quaternion.Euler(-90, 0, 90));
            lastusedtime = Time.time;
        }

    }
    public void SpinMeleeAttack()//Spin melee attack, 2 second cooldown atm. basically just spawns in an attack hitbox at the offsets position. 
    {
        if (Time.time > lastusedtime + 1)
        {
            Vector3 attackpos = transform.position;
            Instantiate(attackhitbox, attackpos, Quaternion.Euler(90, 0, 90));
            lastusedtime = Time.time;
        }

    }
    public void Relocate()//selects a random point within a circle (seemed easier than a sphere) and moves them too it. 
    {
        Vector3 randompoint = Random.insideUnitCircle * 7;
        randompoint.y = 0f;
        targetpos = transform.position + randompoint;
        ismoving = true;
    }
    public void Chase()//chase and pursue are just different flavours of "move to player and hit them", one for bigmenaces, one for smallerones
    {
        if (Vector3.Distance(transform.position, playerref.transform.position) > 2f)
        {
            rb.MovePosition(transform.position + transform.forward * movspeed * Time.deltaTime);
        }
        else
        {
            SpinMeleeAttack();
        }

    }
    public void Pursue()
    {
        if (Vector3.Distance(transform.position, playerref.transform.position) > 2f)
        {
            rb.MovePosition(transform.position + transform.forward * movspeed * Time.deltaTime);
        }
        else
        {
            NormalMeleeAttack();
        }

    }
}
