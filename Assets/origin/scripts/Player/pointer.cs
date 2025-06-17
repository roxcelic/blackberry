using UnityEngine;

public class pointer : MonoBehaviour {
    public GameObject player;
    public GameObject Target;

    public Vector3 defaultScale = new(2, 2, 1);
    public types type;

    public enum types {
        Roxie,
        Dylan
    };

    void Start() {
        if (PlayerPrefs.GetString("altPointer", "false") == "true") type = types.Dylan;
    }

    void Update() {
        if (player == null || Target == null){
            Destroy(transform.gameObject);
            return;
        }

        // assign variables
        float A = 0;
        float B = 0;
        float C = 0;

        float zangel = 0;

        Vector2 tL = new Vector2();
        Vector2 pL = new Vector2();

        switch (type.ToString()) {
            case "Roxie":
                // positions
                tL = new Vector2(Target.transform.position.x, Target.transform.position.y);
                pL = new(0, 1);

                tL -= new Vector2(player.transform.position.x, player.transform.position.y);
                tL = tL.normalized;

                A = tL.x * pL.x + tL.y * pL.y;
                B = Mathf.Sqrt(Mathf.Pow(tL.x, 2) + Mathf.Pow(tL.y, 2));
                C = Mathf.Sqrt(Mathf.Pow(pL.x, 2) + Mathf.Pow(pL.y, 2));

                zangel = Mathf.Acos(
                    (A / (B * C))
                ) *
                Mathf.Rad2Deg;

                if (tL.x > pL.x ) zangel = 360 - zangel;

                break;
            case "Dylan":
                // positions
                tL = new Vector2(Target.transform.position.x, Target.transform.position.z);
                pL = new(0, 1);

                tL -= new Vector2(player.transform.position.x, player.transform.position.z);
                tL = tL.normalized;

                A = tL.x * pL.x + tL.y * pL.y;
                B = Mathf.Sqrt(Mathf.Pow(tL.x, 2) + Mathf.Pow(tL.y, 2));
                C = Mathf.Sqrt(Mathf.Pow(pL.x, 2) + Mathf.Pow(pL.y, 2));

                zangel = Mathf.Acos(
                    (A / (B * C))
                ) *
                Mathf.Rad2Deg;

                if (tL.x > pL.x ) zangel = 360 - zangel;

                break;

        }

        if (zangel >= 0 && zangel <= 360) {
            Quaternion target = Quaternion.Euler(0, 0, zangel);
            transform.rotation = target;   
        }

    }
}