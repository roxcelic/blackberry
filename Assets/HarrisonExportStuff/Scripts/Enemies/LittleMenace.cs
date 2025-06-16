using UnityEngine;
//smaller menace, annoying, fast, sorta like a baby zombie
public class RunningMenace : enemyclass
{
    protected override void Start()
    {
        enmhealth = 2f;
        normalspeed = 6f;
        movspeed = 6f;
        //stats
        base.Start();
    }

    protected override void Update()
    {
        transform.LookAt(playerref.transform.position);
        Chase();
        base.Update();
    }
}
