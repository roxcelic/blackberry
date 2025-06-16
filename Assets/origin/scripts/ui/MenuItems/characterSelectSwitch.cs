using UnityEngine;

public class characterSelectSwitch : ButtonIsh {
    [Header("config")]
    public GameObject target;
    
    public GameObject[] newMenu;

    protected override void Action() {
        base.Action();

        foreach (Transform Child in target.transform) Destroy(Child.gameObject);

        foreach (GameObject newnewMenu in newMenu) {
            GameObject tmpObj = Instantiate(newnewMenu);
            tmpObj.transform.parent = target.transform;
            tmpObj.transform.localPosition = newnewMenu.transform.position; 
        }
    }
}