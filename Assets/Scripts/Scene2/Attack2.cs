using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Video;

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
        [SerializeField] private GameObject hotplateUI;
        [SerializeField] private AudioSource suspenseAudio;
        [SerializeField] private AudioSource computerAudio;
        [SerializeField] private AudioSource leaveAudio;
        [SerializeField] private VideoPlayer videoPlayer;
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
            StartCoroutine(ScareSequence());
        }

        private void Update()
        {
            if(isActive){
                if(computerUI.activeSelf){
                    target = targets[2].transform.position;
                } else if(hotplateUI.activeSelf){
                    target = targets[3].transform.position;
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
                            leaveAudio.Play();
                        } else if(target == targets[1].transform.position){
                            isActive = false;
                            target = targets[0].transform.position;
                            leaveAudio.Play();
                        } else{
                            if(computerUI.activeSelf){
                                computerAudio.Pause();
                                videoPlayer.Pause();
                                computerUI.SetActive(false);
                                aggression = true;
                            } else if(hotplateUI.activeSelf){
                                hotplateUI.SetActive(false);
                                aggression = true;
                            }
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
            yield return new WaitForSeconds(Random.Range(20f, 30f));
            Debug.Log("moving");
            suspenseAudio.Play();
            isActive = true;
        }
    }
}