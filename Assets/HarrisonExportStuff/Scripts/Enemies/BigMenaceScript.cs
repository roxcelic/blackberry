using UnityEngine;
//tankier, stronger menace
public class BigMenace : enemyclass
{
    protected override void Start()
    {
        enmhealth = 14;
        normalspeed = 6f;
        movspeed = 6f;
        //stats setting
        base.Start();
    }

    protected override void Update()
    {
        Chase();
        base.Update();
    }
}
