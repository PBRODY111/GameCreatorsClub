using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scene1
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
        private Quaternion _lookRotation;

        private void Start()
        {
            startPosition = transform.position;
            target = targets[0].transform.position;
            StartCoroutine(ScareSequence());
        }

        private void Update()
        {
            if(isActive){
                GetComponent<Animator>().SetBool("isLeaving", true);
                t += Time.deltaTime / timeToReachTarget;
                var thisTransform = transform;
                thisTransform.position = Vector3.Lerp(startPosition, target, t);
                startPosition = thisTransform.position;
                _lookRotation = Quaternion.LookRotation(target);
                thisTransform.LookAt(target);

                // Check if the distance is within the threshold
                float distance = Vector3.Distance(transform.position, target);
                if (distance < 0.5f)
                {
                    isActive = false;
                    if(target == targets[0].transform.position){
                        target = targets[1].transform.position;
                    } else{
                        target = targets[0].transform.position;
                    }
                    Debug.Log("Close");
                    StartCoroutine(ScareSequence());
                }
            }
        }

        private IEnumerator ScareSequence(){
            t = 0;
            GetComponent<Animator>().SetBool("isReturn", true);
            Debug.Log("Started");
            yield return new WaitForSeconds(Random.Range(5f, 10f));
            Debug.Log("moving");
            isActive = true;
        }
    }
}