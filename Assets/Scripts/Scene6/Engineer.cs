using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
//using Steamworks;

namespace Scene6
{
    public class Engineer : MonoBehaviour
    {
        public AudioSource charlieAudio;
        public AudioSource paradoxAudio;
        public AudioSource systemAudio;
        [SerializeField] private AudioSource jumpscareAudio;
        public AudioSource bossFight;
        public AudioClip [] charlieLines;
        public AudioClip [] paradoxLines;
        public AudioClip [] systemLines;
        [SerializeField] private Animator escapeAnim;
        [SerializeField] private Animator engineerAnim;
        [SerializeField] private GameObject escapeText;
        [SerializeField] private GameObject escapeUI;
        [SerializeField] private GameObject engineerTarget;
        public bool isActive = false;
        [SerializeField] private Vector3 startPosition;
        [SerializeField] private float timeToReachTarget;
        [SerializeField] private EngineerTarget targetVar;
        private Quaternion _lookRotation;
        public float t;
        public Vector3 target;

        private AudioSource[] _allAudioSources;
        private static readonly int IsEscape = Animator.StringToHash("isEscape");
        private static readonly int IsScared = Animator.StringToHash("isScared");

        private bool hasEntered = false;
        public bool canKill = false;

        private void Start()
        {
            try{
                Steamworks.SteamClient.Init(3004570);
            } catch (System.Exception e){
                Debug.Log("Cannot connect to Steam");
            }

            SaveData data2 = SaveSystem.LoadEndings();
            if(data2 == null){
                StartCoroutine(Ending1());
            } else{
                Debug.Log(data2.ending);
                Debug.Log(HasMinigames());
                if(data2.ending >= 1 && HasMinigames()){
                    StartCoroutine(Ending2());
                } else{
                    StartCoroutine(Ending1());
                }
            }
        }

        // KILL MECHANIC!!!
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.name == "Zagreus" && !hasEntered && canKill)
            {
                hasEntered = true;
                StartCoroutine(Jumpscare());
            }
        }

        void Update(){
            if(isActive){
                target = new Vector3(Player.Player.Instance.transform.position.x, gameObject.transform.position.y, Player.Player.Instance.transform.position.z);
                t += Time.deltaTime / timeToReachTarget;
                var thisTransform = gameObject.transform;
                startPosition = thisTransform.position;
                thisTransform.position = Vector3.Lerp(startPosition, target, t);
                _lookRotation = Quaternion.LookRotation(target);
                thisTransform.LookAt(target);

                // Check if the distance is within the threshold
                float distance = Vector3.Distance(transform.position, target);
            }
            if(canKill && !isActive && targetVar.canAttack){
                var thisTransform = gameObject.transform;
                _lookRotation = Quaternion.LookRotation(new Vector3(Player.Player.Instance.transform.position.x, gameObject.transform.position.y, Player.Player.Instance.transform.position.z));
                thisTransform.LookAt(new Vector3(Player.Player.Instance.transform.position.x, gameObject.transform.position.y, Player.Player.Instance.transform.position.z));
                if(!isActive && targetVar.canAttack){
                    target = new Vector3(engineerTarget.transform.position.x, gameObject.transform.position.y, engineerTarget.transform.position.z);
                    t += Time.deltaTime / timeToReachTarget;
                    startPosition = thisTransform.position;
                    thisTransform.position = Vector3.Lerp(startPosition, target, t);

                    // Check if the distance is within the threshold
                    float distance = Vector3.Distance(transform.position, target);
                }
            }
        }

        IEnumerator Ending1(){
            yield return new WaitForSeconds(4f);
            paradoxAudio.clip = paradoxLines[0];
            paradoxAudio.Play();
            yield return new WaitForSeconds(7f);
            StartCoroutine(Escape());
        }
        IEnumerator Escape(){
            escapeUI.SetActive(true);
            escapeAnim.SetBool(IsEscape, true);
            isActive = true;
            jumpscareAudio.Play();
            engineerAnim.SetBool(IsScared, true);
            yield return new WaitForSeconds(1.5f);
            _allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            if (_allAudioSources != null)
                foreach (var audioS in _allAudioSources)
                    audioS.Stop();
            systemAudio.clip = systemLines[0];
            systemAudio.Play();
            escapeText.GetComponent<TMP_Text>().text = "";
            escapeText.SetActive(true);
            yield return new WaitForSeconds(6f);
            SaveSystem.SaveEndings(1);
            /*
            var ach = new Steamworks.Data.Achievement("ENDING_1");
            ach.Trigger();
            */
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        IEnumerator Ending2(){
            bossFight.Play();
            charlieAudio.clip = charlieLines[0];
            charlieAudio.Play();
            yield return new WaitForSeconds(4f);
            paradoxAudio.clip = paradoxLines[1];
            paradoxAudio.Play();
            canKill = true;
            yield return new WaitForSeconds(4f);
            engineerTarget.SetActive(true);
        }

        IEnumerator Jumpscare(){
            isActive = true;
            jumpscareAudio.Play();
            engineerAnim.SetBool(IsScared, true);
            yield return new WaitForSeconds(1.5f);
            SaveSystem.SaveHint("paradox","room6");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("GameOverScene");
        }

        private bool HasMinigames(){
            SaveData data = SaveSystem.LoadMinigame();
            if(data != null){
                Debug.Log(data.styx);
                Debug.Log(data.doublePong);
                Debug.Log(data.dashr);
                if(data.styx && data.doublePong && data.dashr){
                    return true;
                }
            }
            return false;
        }
    }
}