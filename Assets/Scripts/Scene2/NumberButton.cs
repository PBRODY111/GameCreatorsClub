using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NumberButton : MonoBehaviour
{
    public TextMeshProUGUI buttonText;
    public float increaseRate = 0.1f; // Rate at which the number increases when button is held
    public float decreaseRate = 0.5f; // Rate at which the number decreases when button is not held

    private bool isHoldingDown = false;
    private float timer = 0f;

    void Update()
    {
        if (isHoldingDown)
        {
            // Increase the number while the button is being held down
            IncreaseNumber();
        }
        else
        {
            // Decrease the number periodically when the button is not held down
            timer += Time.deltaTime;
            if (timer >= decreaseRate)
            {
                DecreaseNumber();
                timer = 0f;
            }
        }
    }

    // Method called when button is pressed down
    public void OnButtonDown()
    {
        isHoldingDown = true;
        Debug.Log(isHoldingDown);
    }

    // Method called when button is released
    public void OnButtonUp()
    {
        isHoldingDown = false;
    }

    // Increase the number
    private void IncreaseNumber()
    {
        int currentNumber = int.Parse(buttonText.text);
        if(currentNumber<118){
            currentNumber++;
        }
        buttonText.text = currentNumber.ToString();
    }

    // Decrease the number
    private void DecreaseNumber()
    {
        int currentNumber = int.Parse(buttonText.text);
        if(currentNumber>0){
            currentNumber--;
        }
        buttonText.text = currentNumber.ToString();
    }
}
