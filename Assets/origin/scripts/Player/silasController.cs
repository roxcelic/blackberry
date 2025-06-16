using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

public class silasController : PlayerController {
    [Header("silas config")]
    public bool spinning = false;

    protected override void Update() {
        base.Update();

        if (eevee.input.Collect("silas-spin") && !spinning && base.PlayerSpecificMeter > base.PlayerSpecificMeterUseRate) {
            base.PlayerSpecificMeter -= base.PlayerSpecificMeterUseRate;
            StartCoroutine(spin());
        }
    }

    public IEnumerator spin() {
        spinning = true;
        base.speed *= 2;
        yield return new WaitForSeconds(0.5f);
        base.speed /= 2;
        spinning = false;
    }
}
