using UnityEngine;

public class shotgun : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Enter Gun1"+collision.gameObject.tag);
        if (collision.gameObject.tag == "DoorActivate")
        {
            Destroy(gameObject);
        }
    }
}
