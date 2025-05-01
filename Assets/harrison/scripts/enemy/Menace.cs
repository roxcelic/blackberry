using UnityEngine;

public class Menace : enemyclass{

    // floor menace movement
    public override void ChasePlayer(){

        // institute required function
        base.ChasePlayer();

        // assign variables possibly?
            // id recommened using a helper function to change state, use a key and a switch case method for this
        enmmovspeed = 3.0F;
        menacestate = "neutral";

        // This does seem like it will be improved upon later
            // This is an inofficient way to do it since you are constantly refering to things like transform.position rather than assigning a variable to it which could lead to inconsistencys with data later on if more movement tech is added.
        if (Vector3.Distance (transform.position, playerref.transform.position) > 1f){
            transform.LookAt(playerref.transform);
            transform.position += rb.transform.forward * enmmovspeed * Time.deltaTime;
        }else if(Time.time > lastusedtime + menacecooldown){
            Instantiate(dmgsphere, (transform.position * 1.0f), Quaternion.identity);
            lastusedtime = Time.time;
        }
    }

}
