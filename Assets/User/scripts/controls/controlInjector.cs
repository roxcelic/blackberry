using UnityEngine;

using System.Linq;
using System.Collections.Generic;

public class controlInjector : MonoBehaviour {

    public bool overwrite = false;
    public List<string> keys = new List<string>();
    
    void Start() {
        Dictionary<string, eevee.config> controls = eevee.Qlock.extractr();

        foreach (string key in sys.data.config.Keys){
            if (controls.Keys.Contains(key) && overwrite){
                Debug.Log($"overwritng {key}");
                eevee.inject.OverWrite(sys.data.config[key]);
            } else if (!controls.Keys.Contains(key)){
                eevee.inject.add(sys.data.config[key]);
            }
        }

        foreach (string key in eevee.Qlock.extractr().Keys)
            keys.Add(key.ToString());
        
        Debug.Log($"injecting.. \n on object {gameObject.name}");
    }
}
