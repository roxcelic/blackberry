using UnityEngine;

public class ariana_attackrest : MonoBehaviour {
    public float damage = 1;
    public bool attackPierce;
    private bool StartTracking = false;
    public nockbackTest nock;

    void OnEnable() {
        StartTracking = true;
    }

    void OnTriggerEnter(Collider other) {
        if (!StartTracking)
            return;

        if (other.tag == "enemy")
            DealDamage(other.gameObject);
        
        if (other.tag != "Player" && other.gameObject.layer == LayerMask.NameToLayer("Walls"))
            Destroy(gameObject);
    }

    void DealDamage(GameObject target){
        target.GetComponent<MainMenace>().Damage(damage);
        transform.parent.GetComponent<PlayerController>().Heal(damage);
        nock.DamageDone = true;

        if (!attackPierce)
            Destroy(gameObject);
    }
}