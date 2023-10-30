using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCube : MonoBehaviour
{
    [SerializeField] private KeyCode key = KeyCode.Space;
    [SerializeField] private GameObject cubePrefab;
    private int cubeCount = 0;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(key))
        {
            Instantiate(cubePrefab, new Vector3(0,0.5f,0), new Quaternion());
            Debug.Log(cubeCount++);
        }
    }
}
