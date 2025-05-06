using UnityEngine;

public class randomattackthign : MonoBehaviour {

    public float dmg = 2.0F;
    public attackType attack;

    public enum attackType {
        player,
        enemy
    };
    
    void Start() {
        Destroy(gameObject, 0.25F);
    }

    void OnTriggerEnter(Collider colobj){
        switch (attack.ToString()) {
            case "player":
                if(colobj.CompareTag("Enemy")){
                    Menace menacescript = colobj.GetComponent<Menace>();
                    if (menacescript != null)
                        menacescript.Mtookdmg(dmg);
                }

                break;
            case "enemy":
                if(colobj.CompareTag("Player")){
                    Ariana playerscript = colobj.GetComponent<Ariana>();
                    
                    if (playerscript != null)
                        playerscript.playertookdamage();
                }

                break;
        }
    }

}
