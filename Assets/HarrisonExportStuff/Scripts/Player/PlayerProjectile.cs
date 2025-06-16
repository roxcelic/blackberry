using UnityEngine;
//inherits from projectile script, and it is the player's, cannot be parried. 
public class PlayerBullet : ProjectileScript
{
    protected override void Start()
    {
        movspeed = 50f;
        parry = false;
    }

}
