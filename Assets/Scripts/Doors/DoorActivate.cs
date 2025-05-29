using UnityEngine;

public class DoorActivate : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private bool isBlocked = true;
    public GameObject activatedDoor;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (activatedDoor == null)
        {
            isBlocked = false;
        }
    }
    public bool IsBlocked()
    {
        return isBlocked; // Exit the method if the door is blocked
        
    }
    public void SetBlocked(bool value) // Public method to modify the private variable
        {
            isBlocked = value;
        }
}
