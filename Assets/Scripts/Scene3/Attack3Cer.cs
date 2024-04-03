using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack3Cer : MonoBehaviour
{
    private float timer = 0f;
    public float t;
    private bool isRotating = false;
    private bool isActive = false;
    private Quaternion originalRotation;
    [SerializeField] private GameObject intText;
    [SerializeField] private float reach;
    public Vector3 target;
    [SerializeField] private float timeToReachTarget;
    [SerializeField] private GameObject cer;
    private Quaternion _lookRotation;
    [SerializeField] private Vector3 startPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.rotation;
        ResetTimer();
    }

    private void OnMouseExit()
    {
        intText.SetActive(false);
    }

    private void OnMouseOver()
    {
        intText.SetActive(IsWithinReach());
        if (Input.GetKeyDown(KeyCode.E) && IsWithinReach())
        {
            ResetRotation();
            ResetTimer();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive){
            t += Time.deltaTime / timeToReachTarget;
            var thisTransform = cer.transform;
            thisTransform.position = Vector3.Lerp(startPosition, target, t);
            startPosition = thisTransform.position;
            _lookRotation = Quaternion.LookRotation(target);
            thisTransform.LookAt(target);

            // Check if the distance is within the threshold
            float distance = Vector3.Distance(transform.position, target);
        }
        
        timer += Time.deltaTime;

        if (isRotating)
        {
            // Rotate by 15 degrees after 30-45 seconds
            if (timer >= 35f && timer <= 45f)
            {
                transform.Rotate(Vector3.forward, 6f * Time.deltaTime);
            }
            // Rotate by 10 degrees after 10-20 more seconds
            else if (timer >= 45f)
            {
                isActive = true;
                timeToReachTarget = 1;
                target = Player.Player.Instance.transform.position;
            }
        }
    }

    // Reset rotation to original state
    void ResetRotation()
    {
        transform.rotation = originalRotation;
    }

    // Reset timer
    void ResetTimer()
    {
        timer = 0f;
        isRotating = true;
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
