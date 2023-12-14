using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Toxin : MonoBehaviour
{
    public float toxicLevel = 0;
    public float timer = 0;
    public float timer1 = 0;
    [SerializeField] private GameObject toxinUI;
    [SerializeField] private Slider toxinSlider;
    [SerializeField] private GameObject toxinOverlay;
    private bool inTrigger = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerStay(Collider collision){
        if (collision.gameObject.name == "Zagreus")
        {
            inTrigger = true;
            if(toxicLevel <= 10){
                timer += Time.deltaTime;

                if (timer >= 0.1f){
                    timer = 0f;
                    toxicLevel += 0.1f;
                }
            } else{
                SceneManager.LoadScene(4);
            }
        }
    }

    void OnTriggerExit(Collider collision){
        if (collision.gameObject.name == "Zagreus")
        {
            inTrigger = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(toxicLevel > 0){
            toxinSlider.value = toxicLevel;
            toxinOverlay.GetComponent<Image>().color = new Color(toxinOverlay.GetComponent<Image>().color.r, toxinOverlay.GetComponent<Image>().color.g, toxinOverlay.GetComponent<Image>().color.b, (toxicLevel/10));
            toxinUI.SetActive(true);
            if(!inTrigger){
                timer1 += Time.deltaTime;

                if (timer1 >= 0.3f){
                    timer1 = 0f;
                    toxicLevel -= 0.1f;
                }
            }
        } else {
            toxinUI.SetActive(false);
        }
    }
}
