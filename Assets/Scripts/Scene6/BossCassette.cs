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
        [SerializeField] private AudioSource winter;
        [SerializeField] private AudioSource overload;
        [SerializeField] private AudioClip system;
        [SerializeField] private int countDown = 65;
        [SerializeField] private int hits = 26;
        [SerializeField] private CannonUse cannon;
        [SerializeField] private Engineer paradox;
        private bool canShock = false;
        // Start is called before the first frame update
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
                    countDown = 65;
                    hits--;
                    if(hits == 0){
                        paradox.canKill = false;
                        paradox.bossFight.Stop();
                    }
                }
            }
        }

        IEnumerator Paradoxon(){
            yield return new WaitForSeconds(Random.Range(60, 80));
            winter.Stop();
            engineer.SetActive(true);
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
            while (true)
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
