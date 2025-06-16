using UnityEngine;

public class EnemyDamageHitbox : MonoBehaviour
{

    public int dmg = 2;
    protected float lasthit;
    public GameObject hiteffect;
    void Start()
    {
        Destroy(gameObject, 0.25f);
    }
    void OnTriggerEnter(Collider colobj){
        Instantiate(hiteffect, transform.position,  Quaternion.Euler(-90, 0, 0));
        if (colobj.gameObject.tag == "Player" && Time.time > lasthit + 0.1f)
        {
            if (colobj.gameObject.GetComponent<PlayerControllerH>() != null)
            {
                Instantiate(hiteffect, transform.position, Quaternion.identity);
                colobj.gameObject.GetComponent<PlayerControllerH>().playertookdamage(dmg);
                lasthit = Time.time;
                Destroy(gameObject);
            }
        
        }
    }
}
