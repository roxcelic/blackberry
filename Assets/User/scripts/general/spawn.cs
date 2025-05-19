using UnityEngine;

public class spawn : MonoBehaviour {
    public GameObject[] ObjectToSpawn1;
    public GameObject[] ObjectToSpawn2;

    void Start() {
        switch (PlayerPrefs.GetInt("chatacter", 1)){
            case 1:
                foreach (GameObject objects in ObjectToSpawn1)
                    Instantiate(objects, transform.position, objects.transform.rotation);
            
                break;
            case 2:
                foreach (GameObject objects in ObjectToSpawn2)
                    Instantiate(objects, transform.position, objects.transform.rotation);

                break;
        }

        Destroy(gameObject);    
    }
}
