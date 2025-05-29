using UnityEngine;
using System.Collections.Generic;

public class DoorManager : MonoBehaviour
{
    [Header("Door Settings")]
    public List<GameObject> doors = new List<GameObject>();
    public GameObject player;
    public float openDistance = 5f;
    public float closeDistance = 2f;
    
    private List<Animator> animators = new List<Animator>();

    void Start()
    {
        // Store each door's Animator in a list
        foreach (GameObject door in doors)
        {
            Animator doorAnimator = door.GetComponent<Animator>();
            if (doorAnimator != null)
            {
                animators.Add(doorAnimator);
                doorAnimator.SetBool("character_nearby", false); // Ensure doors start closed
            }
        }
    }

    void Update()
    {
        for (int i = 0; i < doors.Count; i++)
        {
            DoorActivate doorScript = doors[i].GetComponent<DoorActivate>();
            if (doorScript == null) continue;
            if (doorScript.IsBlocked()) continue;
            float distance = Vector3.Distance(player.transform.position, doors[i].transform.position);
            bool isNearby = distance < openDistance;
            bool shouldClose = distance > closeDistance;
            animators[i].SetBool("character_nearby", isNearby);

            // Optionally, you can add more logic for complex door behavior
        }
    }
}