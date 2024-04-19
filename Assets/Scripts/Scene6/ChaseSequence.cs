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
        [SerializeField] private GameObject glitched;
        public Vector3 targetPosition = new Vector3(0.38f, 0f, -72f);
        public float duration = 6f;
        private static readonly int IsRun = Animator.StringToHash("isRun");
        private static readonly int IsStart = Animator.StringToHash("isStart");
        void Start()
        {
            glitched.SetActive(false);
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
            yield return new WaitForSeconds(3.5f);
            cerberusAnimator.SetBool(IsStart, true);
            music.Play();
            glitched.SetActive(true);
            StartCoroutine(MoveToPosition(targetPosition, duration));
            
        }
        IEnumerator MoveToPosition(Vector3 targetPosition, float duration)
        {
            Vector3 startPosition = transform.position;
            float timeElapsed = 0f;

            while (timeElapsed < duration)
            {
                // Calculate the interpolation parameter based on the current time and duration
                float t = Mathf.Clamp01(timeElapsed / duration);

                // Interpolate between the start and target positions
                transform.position = Vector3.Lerp(startPosition, targetPosition, t);

                // Increment timeElapsed by the time passed since the last frame
                timeElapsed += Time.deltaTime;

                // Wait for the end of the frame before continuing to the next iteration
                yield return null;
            }

            // Ensure the object reaches the exact target position
            gameObject.transform.position = targetPosition;
            cerberusAnimator.SetBool(IsRun, true);
            isActive = true;
        }
    }
}
