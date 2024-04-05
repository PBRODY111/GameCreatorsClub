using UnityEngine;

public class LookAtHead : MonoBehaviour
{
    public Transform target; // Assign the target GameObject in the Inspector

    void Update()
    {
        // Check if the target is assigned
        if (target != null)
        {
            // Make this GameObject's transform look at the target's position
            transform.LookAt(target.position);
        }
    }
}
