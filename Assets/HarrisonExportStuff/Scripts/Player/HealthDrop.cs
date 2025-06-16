using UnityEngine;

public class HealthDrop : MonoBehaviour
{

    private GameObject playerref;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 3, 0 * Time.deltaTime);
    }
    void OnCollisionEnter(Collision colobj)
    {
        if (colobj.gameObject.tag == "Player")
        {
            playerref = colobj.gameObject;
            playerref.gameObject.GetComponent<PlayerControllerH>().Heal();
            Debug.Log("Healed");
            Destroy(gameObject);
        }
    }
    
}
