using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scene1
{
    public class Toxin : MonoBehaviour
    {
        public float toxicLevel;
        public float timer;
        public float timer1;
        [SerializeField] private GameObject toxinUI;
        [SerializeField] private Slider toxinSlider;
        [SerializeField] private GameObject toxinOverlay;

        private bool _inTrigger;
        
        private void Update()
        {
            if (toxicLevel > 0)
            {
                toxinSlider.value = toxicLevel;
                toxinOverlay.GetComponent<Image>().color = new Color(toxinOverlay.GetComponent<Image>().color.r,
                    toxinOverlay.GetComponent<Image>().color.g, toxinOverlay.GetComponent<Image>().color.b,
                    toxicLevel / 10);
                toxinUI.SetActive(true);
                if (!_inTrigger)
                {
                    timer1 += Time.deltaTime;

                    if (timer1 >= 0.3f)
                    {
                        timer1 = 0f;
                        toxicLevel -= 0.1f;
                    }
                }
            }
            else
            {
                toxinUI.SetActive(false);
            }
        }

        private void OnTriggerExit(Collider collision)
        {
            if (collision.gameObject.name == "Zagreus") _inTrigger = false;
        }

        private void OnTriggerStay(Collider collision)
        {
            if (collision.gameObject.name == "Zagreus")
            {
                _inTrigger = true;
                if (toxicLevel <= 10)
                {
                    timer += Time.deltaTime;

                    if (timer >= 0.1f)
                    {
                        timer = 0f;
                        toxicLevel += 0.1f;
                    }
                }
                else
                {
                    SceneManager.LoadScene("GameOverScene");
                }
            }
        }
    }
}