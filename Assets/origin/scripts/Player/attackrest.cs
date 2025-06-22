using UnityEngine;

public class attackrest : MonoBehaviour {
    public string eviltag;
    public float damage = 1;
    public nockbackTest nock;

    void OnEnable() {
        CheckRadius();
    }

    public void Disable() {
        transform.gameObject.SetActive(false);
    }

    void CheckRadius() {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, transform.localScale.x / 2);

        foreach (Collider collider in hitColliders){
            GameObject gameObject = collider.gameObject;
            if (gameObject.tag == eviltag)
                DealDamage(gameObject);
        }
    }

    void DealDamage(GameObject target){
        switch (eviltag) {
            case "Player":
                target.GetComponent<PlayerController>().Damage(damage);
                nock.DamageDone = true;
                
                break;
            case "enemy":
                target.GetComponent<enemyRebuild>().Damage(damage);
                transform.parent.GetComponent<PlayerController>().Heal(damage);
                nock.DamageDone = true;

                break;
        }
    }

}