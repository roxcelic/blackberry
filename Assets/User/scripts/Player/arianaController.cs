using UnityEngine;

public class arianaController : PlayerController {
    [Header("ariana Config")]
    public GameObject ariana_attack;
    public float attackForce;

    protected override void Update() {
        base.Update();

        if (eevee.input.Collect("attack-L"))
            SpawnAttack(Vector3.left);
        if (eevee.input.Collect("attack-R"))
            SpawnAttack(Vector3.right);
        if (eevee.input.Collect("attack-U"))
            SpawnAttack(Vector3.forward);
        if (eevee.input.Collect("attack-D"))
            SpawnAttack(-Vector3.forward);
    }

    void SpawnAttack(Vector3 Direction = new Vector3()){
        if (base.PlayerSpecificMeter < base.PlayerSpecificMeterUseRate)
            return;

        base.PlayerSpecificMeter -= base.PlayerSpecificMeterUseRate;

        GameObject newAttack = Instantiate(ariana_attack);
        newAttack.transform.parent = transform;
        newAttack.transform.position = transform.position;
    
        newAttack.GetComponent<ariana_attackrest>().attackPierce = base.attackPierce;
        newAttack.GetComponent<ariana_attackrest>().damage = base.PlayerSpecificAttackDamage;

        newAttack.SetActive(true);

        Direction *= attackForce;

        newAttack.GetComponent<Rigidbody>().AddForce(Direction, ForceMode.Impulse);
    }
}