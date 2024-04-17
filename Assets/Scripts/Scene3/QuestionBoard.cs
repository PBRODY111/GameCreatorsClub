using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestionBoard : MonoBehaviour
{
    [SerializeField] private string [] set1;
    [SerializeField] private bool [] set2;
    [SerializeField] private GameObject intText;
    [SerializeField] private GameObject questionUI;
    [SerializeField] private float reach;
    [SerializeField] private TMP_Text textBlock;
    [SerializeField] private int question = 0;
    [SerializeField] private int currentQuestion = 0;
    [SerializeField] private PhoneAudio phoneAudio;
    [SerializeField] private AudioSource beepAudio;
    [SerializeField] private AudioSource buzzAudio;
    [SerializeField] private BreakerSwitch switch5;
    [SerializeField] private BreakerSwitch switch6;
    [SerializeField] private BreakerSwitch switch7;
    [SerializeField] private BreakerSwitch switch8;
    [SerializeField] private bool[] switchDirections = new bool[4];
    [SerializeField] private int[] randQuests = new int[3];
    [SerializeField] private int randQuestIndex = 0;
    private HashSet<int> generatedNumbers;
    // Start is called before the first frame update
    void Start()
    {
        // Assign switch directions to the array
        switchDirections[0] = switch5.switchDirection;
        switchDirections[1] = switch6.switchDirection;
        switchDirections[2] = switch7.switchDirection;
        switchDirections[3] = switch8.switchDirection;
    }

    private void OnMouseExit()
    {
        intText.SetActive(false);
    }

    private void OnMouseOver()
    {
        intText.SetActive(IsWithinReach() && phoneAudio.emergencyActive);
        if (Input.GetKeyDown(KeyCode.E) && IsWithinReach() && phoneAudio.emergencyActive)
        {
            questionUI.SetActive(true);
            intText.SetActive(false);
            Player.Player.Instance.UnlockCursor();
            Player.Player.Instance.DisableMovement();
            StartCoroutine(AskQuestions());
        }
    }

    public void SetRandomBool()
    {
        // Check if all booleans are false
        bool allFalse = true;
        foreach (bool b in switchDirections)
        {
            if (b)
            {
                allFalse = false;
                break;
            }
        }

        // If all booleans are false, set a random one to true
        if (allFalse)
        {
            int randomIndex = Random.Range(0, switchDirections.Length);
            switchDirections[randomIndex] = true;
        }
    }

    public void HideQuestion(){
        textBlock.text = "";
        questionUI.SetActive(false);
        Player.Player.Instance.LockCursor();
        Player.Player.Instance.EnableMovement();
    }

    public void TrueQuestion(){
        buzzAudio.Play();
        if(currentQuestion>4){
            if(set2[currentQuestion] == true){
                question++;
                Debug.Log(currentQuestion);
                StartCoroutine(AskQuestions());
            } else{
                textBlock.text = "WRONG";
                beepAudio.Play();
                if(question <= 2){
                    question = 0;
                } else if(question <= 5){
                    question = 3;
                } else if(question <= 8){
                    question = 6;
                } else if(question <= 11){
                    question = 9;
                }
                HideQuestion();
            }
        } else{
            question++;
            StartCoroutine(AskQuestions());
        }
    }
    public void FalseQuestion(){
        buzzAudio.Play();
        if(currentQuestion>4){
            if(set2[currentQuestion] == false){
                question++;
                Debug.Log(currentQuestion);
                StartCoroutine(AskQuestions());
            } else{
                textBlock.text = "WRONG";
                beepAudio.Play();
                if(question <= 2){
                    question = 0;
                } else if(question <= 5){
                    question = 3;
                } else if(question <= 8){
                    question = 6;
                } else if(question <= 11){
                    question = 9;
                }
                HideQuestion();
            }
        } else{
            question++;
            StartCoroutine(AskQuestions());
        }
    }

    IEnumerator AskQuestions(){
        SetRandomBool();
        yield return new WaitForSeconds(0.5f);
        if(question == 0){
            generatedNumbers = new HashSet<int>();
            for (int i = 0; i < randQuests.Length;)
            {
                int randomNumber = Random.Range(0, 5);

                // Check if the number is not already generated
                if (!generatedNumbers.Contains(randomNumber))
                {
                    randQuests[i] = randomNumber;
                    generatedNumbers.Add(randomNumber);
                    i++;
                }
            }
            textBlock.text = "EACH LEVEL IS THREE ROUNDS.";
            beepAudio.Play();
            yield return new WaitForSeconds(2f);
            textBlock.text = "LEVEL 1";
            beepAudio.Play();
            yield return new WaitForSeconds(0.7f);
            currentQuestion = randQuests[randQuestIndex];
        } else if(question == 3){
            randQuestIndex = 0;
            generatedNumbers = new HashSet<int>();
            for (int i = 0; i < randQuests.Length;)
            {
                int randomNumber = Random.Range(5, 10);

                // Check if the number is not already generated
                if (!generatedNumbers.Contains(randomNumber))
                {
                    randQuests[i] = randomNumber;
                    generatedNumbers.Add(randomNumber);
                    i++;
                }
            }
            textBlock.text = "S5: "+switch5.switchDirection;
            beepAudio.Play();
            yield return new WaitForSeconds(1.5f);
            textBlock.text = "LEVEL 2";
            beepAudio.Play();
            yield return new WaitForSeconds(0.7f);
            currentQuestion = randQuests[randQuestIndex];
        } else if(question == 6){
            randQuestIndex = 0;
            generatedNumbers = new HashSet<int>();
            for (int i = 0; i < randQuests.Length;)
            {
                int randomNumber = Random.Range(10, 15);

                // Check if the number is not already generated
                if (!generatedNumbers.Contains(randomNumber))
                {
                    randQuests[i] = randomNumber;
                    generatedNumbers.Add(randomNumber);
                    i++;
                }
            }
            textBlock.text = "S6: "+switch6.switchDirection;
            beepAudio.Play();
            yield return new WaitForSeconds(1.5f);
            textBlock.text = "LEVEL 3";
            beepAudio.Play();
            yield return new WaitForSeconds(0.7f);
            currentQuestion = randQuests[randQuestIndex];
        } else if(question == 9){
            randQuestIndex = 0;
            generatedNumbers = new HashSet<int>();
            for (int i = 0; i < randQuests.Length;)
            {
                int randomNumber = Random.Range(15, 22);

                // Check if the number is not already generated
                if (!generatedNumbers.Contains(randomNumber))
                {
                    randQuests[i] = randomNumber;
                    generatedNumbers.Add(randomNumber);
                    i++;
                }
            }
            textBlock.text = "S7: "+switch7.switchDirection;
            beepAudio.Play();
            yield return new WaitForSeconds(1.5f);
            textBlock.text = "LEVEL 4";
            beepAudio.Play();
            yield return new WaitForSeconds(0.7f);
            currentQuestion = randQuests[randQuestIndex];
        } else if(question == 12){
            textBlock.text = "S8: "+switch8.switchDirection;
            beepAudio.Play();
            yield return new WaitForSeconds(1.5f);
            HideQuestion();
        } else{
            randQuestIndex++;
            currentQuestion = randQuests[randQuestIndex];
        }
        textBlock.text = set1[currentQuestion];
        beepAudio.Play();
    }

    private bool IsWithinReach()
    {
        return Vector3.Distance(transform.position, Player.Player.Instance.transform.position) <= reach;
    }
}
