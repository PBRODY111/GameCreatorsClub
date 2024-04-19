using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Scene6
{
    public class ChaseSequence : MonoBehaviour
    {
        public NavMeshAgent ai;
        public Transform player;
        Vector3 dest;
        public float aiSpeed;
        [SerializeField] private AudioSource music;
        [SerializeField] private bool isActive = false;
        [SerializeField] private Animator cerberusAnimator;
        private static readonly int IsRun = Animator.StringToHash("isRun");
        void Start()
        {
            StartCoroutine(StartChase());
        }

        void Update()
        {
            if(isActive){
                ai.speed = aiSpeed;
                dest = player.position;
                ai.destination = dest;
            }
        }

        IEnumerator StartChase(){
            yield return new WaitForSeconds(5f);
            cerberusAnimator.SetBool(IsRun, true);
            music.Play();
            isActive = true;
        }
    }
}
