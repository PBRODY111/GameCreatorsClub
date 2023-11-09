using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCube : MonoBehaviour
{
    [SerializeField] private KeyCode key = KeyCode.Space;
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Transform deskSpam;
    void Update()
    {
        if (Input.GetKey(key))
            Instantiate(cubePrefab,new Vector3(0f,0.5f,0f), new Quaternion(),deskSpam);
    }
}
