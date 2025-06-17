using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

public class eamonController : PlayerController {
    public bool canUseAbility;

    protected override void Update() {
        base.Update();

        if (eevee.input.Collect("special") && canUseAbility) {
            canUseAbility = false;

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

            StartCoroutine(healWait(0.25f));
        }
    }
    
    public override void Heal(float Heal, bool modified = false) {
        base.Heal(Heal, modified);

        base.PlayerSpecificMeter += Heal;
    }

    public IEnumerator healWait(float delay) {
        yield return new WaitForSeconds(delay);
        canUseAbility = true;
    }
}
