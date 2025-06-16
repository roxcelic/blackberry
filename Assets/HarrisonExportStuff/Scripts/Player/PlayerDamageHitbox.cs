using UnityEngine;
//players hitbox, its a seperate prefab from the enemies because I thought it'd be easier
public class PlayerDamageHitbox : MonoBehaviour
{
    public int dmg = 2;
    void Start()
    {
        Destroy(gameObject, 0.25F);//only so it exists for a short while- I want to do this so the effect persists- could probably be done better
    }
    void OnTriggerEnter(Collider colobj){
        if (colobj.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit something ");
            if (colobj.gameObject.GetComponent<enemyclass>() != null)
            {
                colobj.gameObject.GetComponent<enemyclass>().takedamage(dmg,"melee");
            }
        }
    }
}
