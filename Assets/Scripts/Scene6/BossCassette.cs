using System.Collections;
using UnityEngine;
using TMPro;

namespace Scene6
{
    public class BossCassette : MonoBehaviour
    {
        [SerializeField] private float reach;
        [SerializeField] private bool activated = false;
        [SerializeField] private GameObject engineer;
        [SerializeField] private GameObject intText;
        [SerializeField] private GameObject intText3;
        [SerializeField] private GameObject engineerTarget;
        [SerializeField] private GameObject sparks;
        [SerializeField] private GameObject ending2UI;
        [SerializeField] private GameObject inhibitor;
        [SerializeField] private GameObject playerCheckpoint;
        [SerializeField] private GameObject arms;
        [SerializeField] private AudioSource winter;
        [SerializeField] private int countDown = 45;
        [SerializeField] private int hits = 26;
        [SerializeField] private CannonUse cannon;
        [SerializeField] private Engineer paradox;
        [SerializeField] private EngineerTarget targetVar;
        private bool canShock = false;
        // Start is called before the first frame update
        private void Start(){
            SaveData data3 = SaveSystem.LoadHint();
            if(data3 != null){
                if(data3.hintString == "PARADOXON EXCITAT."){
                    Player.Player.Instance.transform.position = playerCheckpoint.transform.position;
                }
            }
        }
        private void OnMouseExit()
        {
            intText.SetActive(false);
            intText3.SetActive(false);
        }

        private void OnMouseOver()
        {
            intText.SetActive(IsWithinReach() && !activated);
            if (Input.GetKeyDown(KeyCode.E) && IsWithinReach() && !activated)
            {
                activated = true;
                winter.Play();

                // CHANGE THIS BACK!!!!!!

                //StartCoroutine(Paradoxon());
                StartCoroutine(Excitat());
            }

            if(IsWithinReach() && canShock){
                intText3.GetComponent<TMP_Text>().text = "DEBUG NEEDED TO INTERACT";
                intText3.SetActive(true);

                if (Input.GetMouseButtonDown(1) && IsWithinReach() && cannon.canShock){
                    countDown = 45;
                    paradox.systemAudio.Stop();
                    hits--;
                    StartCoroutine(Shock());
                    if(hits == 13){
                        arms.SetActive(true);
                    } else if(hits == 4){
                        targetVar.finalStage = true;
                    } else if(hits == 0){
                        arms.SetActive(false);
                        paradox.canKill = false;
                        paradox.bossFight.Stop();
                        canShock = false;
                        engineerTarget.SetActive(false);
                        StartCoroutine(Inhibitor());
                    }
                }
            }
        }

        IEnumerator Paradoxon(){
            yield return new WaitForSeconds(Random.Range(45, 55));
            winter.Stop();
            engineer.SetActive(true);
        }
        IEnumerator Shock(){
            sparks.SetActive(true);
            yield return new WaitForSeconds(1f);
            sparks.SetActive(false);
        }
        IEnumerator Inhibitor(){
            yield return new WaitForSeconds(1f);
            paradox.paradoxAudio.clip = paradox.paradoxLines[2];
            paradox.paradoxAudio.Play();
            yield return new WaitForSeconds(3f);
            paradox.charlieAudio.clip = paradox.charlieLines[2];
            paradox.charlieAudio.Play();
            yield return new WaitForSeconds(1f);
            paradox.paradoxAudio.clip = paradox.paradoxLines[3];
            paradox.paradoxAudio.Play();
            yield return new WaitForSeconds(1f);
            ending2UI.SetActive(true);
            yield return new WaitForSeconds(2f);
            engineer.SetActive(false);
            inhibitor.SetActive(true);
            gameObject.SetActive(false);
        }

        IEnumerator Excitat(){
            yield return new WaitForSeconds(Random.Range(60, 80));
            winter.Stop();
            engineer.SetActive(true);
            yield return new WaitForSeconds(8f);
            canShock = true;
            reach += 1.5f;
            StartCoroutine(DecreaseCount());
        }

        private IEnumerator DecreaseCount()
        {
            while (canShock)
            {
                if (countDown > 0)
                {
                    countDown--;
                }
                if(countDown <= 12 && !paradox.systemAudio.isPlaying){
                    paradox.systemAudio.clip = paradox.systemLines[1];
                    paradox.systemAudio.Play();
                }
                if(countDown == 0){
                    paradox.isActive = true;
                }
                yield return new WaitForSeconds(1f);
            }
        }

        private bool IsWithinReach()
        {
            return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
        }
    }
}
