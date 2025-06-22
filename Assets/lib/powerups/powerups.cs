using UnityEngine;
using UnityEngine.UI;

using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace powerups {
    [System.Serializable]
    public class PowerUp {
        public string Description;
        public string Name;
        public Action<MonoBehaviour> action;
    }

    public static class full {
        public static Action<MonoBehaviour> Base = (This) => {
            if (This.gameObject.tag != "Player") return;
        };

        public static Dictionary<string, powerups.PowerUp> act = new Dictionary<string, powerups.PowerUp> {
        };

        public static Dictionary<string, powerups.PowerUp> basis = new Dictionary<string, powerups.PowerUp> {
        };

        public static Dictionary<string, powerups.PowerUp> fun = new Dictionary<string, powerups.PowerUp> {
        };

        // ienumerators
        private static IEnumerator TestCoroutine() {
            yield return 0;
        } 


        public static Dictionary<string, powerups.PowerUp> truePowerups(){
            Dictionary<string, powerups.PowerUp> working_ref = full.act;

            foreach(string key in basis.Keys)
                working_ref[key] = basis[key];

            if (PlayerPrefs.GetString("expandedPowerUp", "false") == "true")
                foreach(string key in fun.Keys)
                    working_ref[key] = fun[key];
            
            return working_ref;
        }
    }
}