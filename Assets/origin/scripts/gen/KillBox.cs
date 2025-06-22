using UnityEngine;

public class KillBox : MonoBehaviour {
    public GameObject world;
    public Collider col;
    public bool started = false;
    public float enemyDamage;

    void Start() {
        col = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider hitCollider) {
        if (started)
            ManageDeath(hitCollider.tag, hitCollider);
    }

    public void ManageDeath(string tag, Collider chosenOne){
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        switch(tag){
            case "enemy":
                if (players.Length != 0 && players[0].GetComponent<PlayerController>().voidLifeSteal) {
                    players[0].GetComponent<PlayerController>().Heal(chosenOne.GetComponent<enemyRebuild>().maxHealth);
                }

                chosenOne.GetComponent<enemyRebuild>().Damage(enemyDamage);

                break;
            case "Player":
                PlayerController player = chosenOne.GetComponent<PlayerController>();
                float damage = player.health / 4;
                damage = Mathf.Clamp(damage, player.maxHealth / 10, player.maxHealth);

                player.Damage(damage, "void");

                if (world.transform.childCount-1 >= player.room)
                    chosenOne.transform.position = world.transform.GetChild(player.room).position + new Vector3(0, 10, 0);
                else 
                    chosenOne.transform.position = world.transform.GetChild(world.transform.childCount-1).position + new Vector3(0, 10, 0);                    

                break;
            default:
                Debug.Log("i dont know what to do with this one boss");

                break;
        }
    }
}