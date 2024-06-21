using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundControl : MonoBehaviour
{
    [SerializeField] private GameObject tree;
    [SerializeField] private Transform treeParent; 
    [SerializeField] private GameObject cloud;
    [SerializeField] private Transform cloudParent; 
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("DASHR HIDE ITEMS");
        StartCoroutine(AddTree());
        StartCoroutine(AddCloud());
    }

    IEnumerator AddTree(){
        while (true){
            Debug.Log("tree adding");
            Debug.Log("Time scale: " + Time.timeScale);
            Instantiate(tree, treeParent);
            yield return new WaitForSeconds(Random.Range(3f, 5f));
            
            Debug.Log("tree added");
        }
    }
    IEnumerator AddCloud(){
        while (true){
            Instantiate(cloud, cloudParent);
            yield return new WaitForSeconds(Random.Range(4f, 6f));
        }
    }
}
