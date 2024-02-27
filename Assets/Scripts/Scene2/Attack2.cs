using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scene2
{
    public class Attack2 : MonoBehaviour
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
        [SerializeField] private GameObject computerUI;
        [SerializeField] private AudioSource suspenseAudio;

        private void Start()
        {
            startPosition = transform.position;
            target = targets[0].transform.position;
            StartCoroutine(ScareSequence());
        }

        private void Update()
        {
            if(isActive){
                if(computerUI.activeSelf){
                    target = targets[2].transform.position;
                }
                GetComponent<Animator>().SetBool("isLeaving", true);
                t += Time.deltaTime / timeToReachTarget;
                var thisTransform = transform;
                thisTransform.position = Vector3.Lerp(startPosition, target, t);
                startPosition = thisTransform.position;
                _lookRotation = Quaternion.LookRotation(target);
                thisTransform.LookAt(target);

                // Check if the distance is within the threshold
                float distance = Vector3.Distance(transform.position, target);
                if(!aggression){
                    timeToReachTarget = 500;
                    if (distance < 0.5f)
                    {
                        if(target == targets[0].transform.position){
                            isActive = false;
                            target = targets[1].transform.position;
                        } else if(target == targets[1].transform.position){
                            isActive = false;
                            target = targets[0].transform.position;
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

        private IEnumerator ScareSequence(){
            t = 0;
            //GetComponent<Animator>().SetBool("isReturn", true);
            Debug.Log("Started");
            yield return new WaitForSeconds(Random.Range(12f, 20f));
            Debug.Log("moving");
            suspenseAudio.Play();
            isActive = true;
        }
    }
}