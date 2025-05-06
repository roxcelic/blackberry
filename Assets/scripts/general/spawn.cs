using UnityEngine;

public class spawn : MonoBehaviour {
    public GameObject[] ObjectToSpawn;

    void Start() {
        foreach (GameObject objects in ObjectToSpawn)
            Instantiate(objects, transform.position, objects.transform.rotation);

        Destroy(gameObject);    
    }
}
