using UnityEngine;

public class pointer : MonoBehaviour {
    public GameObject player;
    public GameObject Target;

    void Update() {
        if (player == null || Target == null){
            Destroy(transform.gameObject);
            return;
        }

        // positions
        Vector2 tL = new(Target.transform.position.x, Target.transform.position.y);
        Vector2 pL = new(player.transform.position.x, player.transform.position.y);

        Vector2 b = tL;
        Vector2 a = pL;

        float radionThing = (((a.x + b.x) * (a.y + b.y)) / sys.math.hyp(a)) * Mathf.Deg2Rad;
        float zangel = (Mathf.Acos(radionThing));


        // my angel
        zangel *= Mathf.Rad2Deg;
        zangel *= 4;
        zangel -= 90f;

        if (tL.x < pL.x) zangel = 360 - zangel;

        if (zangel >= 0 && zangel <= 360) {
            Quaternion target = Quaternion.Euler(0, 0, zangel);
            transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * 5f);   
        }
    }
}