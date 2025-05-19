using UnityEngine;
using UnityEngine.UI;

using TMPro;

using System;
using System.Collections;
using System.Collections.Generic;

public class ChangeFloorDisplay : MonoBehaviour {
    
    public int currentFloor;
    public TMP_Text floor;
    public TMP_Text tip;
    public float opacityHold = 1;
    public bool active = true;

    public List<string> tips = new List<string>();
    
    void Start() {
        if (!active) {
            tip.text = "";
            return;
        }

        int tipid;

        if (PlayerPrefs.GetInt("loading-screen-tipnumber-active", 0) == 0){
            currentFloor = PlayerPrefs.GetInt("floor", 0);
            floor.text = currentFloor.ToString();

            tipid = UnityEngine.Random.Range(0, tips.Count);
            PlayerPrefs.SetInt("loading-screen-tipnumber", tipid);
            tip.text = tips[tipid];

            PlayerPrefs.SetInt("loading-screen-tipnumber-active", 1);
        } else {
            currentFloor = PlayerPrefs.GetInt("floor", 0);
            floor.text = currentFloor.ToString();

            tipid = PlayerPrefs.GetInt("loading-screen-tipnumber", 0);

            tip.text = tips[tipid];
            PlayerPrefs.SetInt("loading-screen-tipnumber-active", 0);

            StartCoroutine(changeDisplay());
        }
    }

    public IEnumerator changeDisplay() {
        yield return new WaitForSeconds(0.1f);
        
        while (opacityHold > 0.1f) {
            opacityHold = Mathf.Lerp(opacityHold, 0, Time.deltaTime *  2.5f);
            floor.color = new Color(floor.color.r, floor.color.g, floor.color.b, opacityHold);

            yield return 0;
        }

        floor.text = (PlayerPrefs.GetInt("floor", 0)).ToString();

        while (opacityHold < 0.9f) {
            opacityHold = Mathf.Lerp(opacityHold, 1, Time.deltaTime *  2.5f);
            floor.color = new Color(floor.color.r, floor.color.g, floor.color.b, opacityHold);

            yield return 0;
        }
    }
}
