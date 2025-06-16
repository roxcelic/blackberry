using UnityEngine;

public class eamonController : PlayerController {

    protected override void Update() {
        base.Update();

        if (eevee.input.Collect("eamon-heal")) {
            float possible_heal = base.PlayerSpecificMeter / 2;
            float required_heal = base.maxHealth - base.health;
            float applied_heal = 0;

            if (possible_heal > required_heal) {
                applied_heal = required_heal;
                possible_heal -= required_heal;
            } else {
                applied_heal = possible_heal;
                possible_heal = 0;
            }

            Mathf.Clamp(possible_heal, 1, base.PlayerSpecificMeterMax);

            Heal(applied_heal, true);
            base.PlayerSpecificMeter = possible_heal * 2;
        }
    }
    
    public override void Heal(float Heal, bool modified = false) {
        base.Heal(Heal, modified);

        base.PlayerSpecificMeter += Heal;
    }
}
