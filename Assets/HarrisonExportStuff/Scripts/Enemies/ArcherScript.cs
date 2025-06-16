using UnityEngine;
using UnityEngine.Timeline;
using System.Collections;
using System.Numerics;
public class ArcherScript : enemyclass{
    protected override void Start(){
        enmhealth = 6f;
        cooldown = 4f;
        movspeed = 4;
        normalspeed = 4;
        //sets stats and such
        base.Start();
        StartCoroutine(fire());
    }

    protected override void Update()
    {
        rb.MovePosition(transform.position + (transform.position - targetpos) * movspeed * Time.deltaTime);//moves enemy to that random point, and checks if it's still there? idk how to explain it.
        if (transform.position == targetpos)
        {
            ismoving = false;
        }
        base.Update();
    }

    public IEnumerator fire()//you made this I believe, I can't remember but it works!
    {
        while (true)
        {
            Relocate();
            RangedAttack();
            yield return new WaitForSeconds(2);
        }
    }


}
