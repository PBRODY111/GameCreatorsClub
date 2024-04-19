using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseSequence : MonoBehaviour
{
    public NavMeshAgent ai;
    public Transform player;
    Vector3 dest;
    public float aiSpeed;
    [SerializeField] private AudioSource music;
    [SerializeField] private bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartChase());
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if(isActive){
            ai.speed = aiSpeed;
            dest = player.position;
            ai.destination = dest;
        }
    }

    IEnumerator StartChase(){
        yield return new WaitForSeconds(5f);
        music.Play();
        isActive = true;
    }
}
