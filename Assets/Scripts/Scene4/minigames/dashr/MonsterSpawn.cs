using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    [SerializeField] private GameObject monster;
    [SerializeField] private Transform parent;  
    [SerializeField] private float max;
    [SerializeField] private float min;
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn(){
        while (true){
            Instantiate(monster, parent);
            yield return new WaitForSeconds(Random.Range(min, max));
        }
    }
}
