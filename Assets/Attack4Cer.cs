using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Scene1;


public class Attack4Cer : MonoBehaviour
{
    public float t;
    [SerializeField] private GameObject [] targets;
    [SerializeField] private Vector3 startPosition;
    public Vector3 target;
    [SerializeField] private float timeToReachTarget;
    [SerializeField] private float rotationTime;
    public bool isActive = false;
    public bool aggression = false;
    private Quaternion _lookRotation;
    [SerializeField] private AudioSource suspenseAudio;
    [SerializeField] private AudioSource leaveAudio;
    [SerializeField] private CerKill cerKill;
    public string tagToIgnore = "ignoreCollision";

    private void Start()
    {
        Collider[] colliders = GameObject.FindGameObjectsWithTag(tagToIgnore)
                                    .Select(go => go.GetComponent<Collider>())
                                    .Where(collider => collider != null)
                                    .ToArray();

        // Ignore collisions with each collider found
        foreach (var collider in colliders)
        {
            if (collider != null)
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), collider);
            }
        }

        startPosition = transform.position;
        target = targets[0].transform.position;
    }

    private void Update()
    {
        if(cerKill.stopAttack){
            target = Player.Player.Instance.transform.position;
        }
        if(isActive){
            GetComponent<Animator>().SetBool("isLeaving", true);
            if(Time.timeScale != 0){
                t += Time.deltaTime / timeToReachTarget;
                var thisTransform = transform;
                thisTransform.position = Vector3.Lerp(startPosition, target, t);
                startPosition = thisTransform.position;
                _lookRotation = Quaternion.LookRotation(target);
                thisTransform.LookAt(target);
            }

            // Check if the distance is within the threshold
            float distance = Vector3.Distance(transform.position, target);
            if(!aggression){
                timeToReachTarget = 500;
                if (distance < 0.5f)
                {
                    if(target == targets[0].transform.position){
                        isActive = false;
                        target = targets[1].transform.position;
                        leaveAudio.Play();
                    } else if(target == targets[1].transform.position){
                        isActive = false;
                        target = targets[2].transform.position;
                        leaveAudio.Play();
                    } else if(target == targets[2].transform.position){
                        isActive = false;
                        target = targets[3].transform.position;
                        leaveAudio.Play();
                    } else if(target == targets[3].transform.position){
                        isActive = false;
                        target = targets[0].transform.position;
                        leaveAudio.Play();
                    } else{
                        target = targets[0].transform.position;
                    }
                    Debug.Log("Close");
                    StartCoroutine(ScareSequence());
                }
            } else{
                timeToReachTarget = 1;
                target = Player.Player.Instance.transform.position;
            }
        }
    }

    public IEnumerator ScareSequence(){
        t = 0;
        //GetComponent<Animator>().SetBool("isReturn", true);
        Debug.Log("Started");
        yield return new WaitForSeconds(Random.Range(5f, 10f));
        Debug.Log("moving");
        suspenseAudio.Play();
        isActive = true;
    }
}
