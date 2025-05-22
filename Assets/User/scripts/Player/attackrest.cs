using UnityEngine;

public class attackrest : MonoBehaviour {
    public string eviltag;
    public float damage = 1;

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

                break;
            case "enemy":
                target.GetComponent<MainMenace>().Damage(damage);
                transform.parent.GetComponent<PlayerController>().Heal(damage / 4f);

                break;
        }
    }

}