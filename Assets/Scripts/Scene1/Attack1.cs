using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1 : MonoBehaviour
{
    public float t;
    [SerializeField] private Vector3 startPosition;
    public Vector3 target;
    [SerializeField] private float timeToReachTarget;
    public bool jumpscare = false;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime/timeToReachTarget;
        if(jumpscare == true){
            transform.position = Vector3.Lerp(startPosition, target, t);
            startPosition = transform.position;
        }
    }
}
