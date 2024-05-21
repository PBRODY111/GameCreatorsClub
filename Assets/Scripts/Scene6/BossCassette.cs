using System.Collections;
using UnityEngine;

namespace Scene6
{
    public class BossCassette : MonoBehaviour
    {
        [SerializeField] private float reach;
        [SerializeField] private bool activated = false;
        [SerializeField] private GameObject engineer;
        [SerializeField] private GameObject intText;
        [SerializeField] private AudioSource winter;
        // Start is called before the first frame update
        private void OnMouseExit()
        {
            intText.SetActive(false);
        }

        private void OnMouseOver()
        {
            intText.SetActive(IsWithinReach() && !activated);
            if (Input.GetKeyDown(KeyCode.E) && IsWithinReach() && !activated)
            {
                activated = true;
                winter.Play();
                StartCoroutine(Paradoxon());
            }
        }

        IEnumerator Paradoxon(){
            yield return new WaitForSeconds(Random.Range(60, 80));
            winter.Stop();
            engineer.SetActive(true);
        }

        private bool IsWithinReach()
        {
            return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
        }
    }
}
