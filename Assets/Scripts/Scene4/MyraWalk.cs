using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MyraWalk : MonoBehaviour
{
    public NavMeshAgent ai;
    [SerializeField] private Vector3 dest;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource myra;
    public float aiSpeed;
    private static readonly int IsRun = Animator.StringToHash("isRun");
    private bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartRun());
    }

    IEnumerator StartRun(){
        yield return new WaitForSeconds(0.5f);
        myra.Play();
        yield return new WaitForSeconds(4.5f);
        isActive = true;
        animator.SetBool(IsRun, true);
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive){
            ai.speed = aiSpeed;
            ai.destination = dest;
        } else{
            ai.speed = 0;
            ai.destination = Player.Player.Instance.transform.position;
        }
    }
}
