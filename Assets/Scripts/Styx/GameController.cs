using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public int pointValue = 0; // Number of points, publicly accessible
    public int currentLevel = 1; // Current level, publicly accessible

    public TextMeshProUGUI pointsText; // Reference to the TextMeshPro text for points
    public TextMeshProUGUI levelText; // Reference to the TextMeshPro text for level

    public Transform playerTransform; // Reference to the player object's transform
    [SerializeField] private GameObject level1Object;
    [SerializeField] private GameObject level2Object;
    [SerializeField] private GameObject level3Object;
    [SerializeField] private GameObject inhibitor;
    public AudioSource levelAudio;
    public AudioSource gameAudio;

    void Start()
    {
        UpdatePointsText();
        UpdateLevelText();
    }

    public void IncrementPointValue()
    {
        pointValue++;
        if (pointValue >= 6)
        {
            pointValue = 0;
            ChangeLevel();
            if (playerTransform != null)
            {
                playerTransform.localPosition = new Vector3(0f,0f,0f); // Reset player position to origin
            }
        }
        UpdatePointsText();
    }

    public void ChangeLevel()
    {
        if(currentLevel == 1){
            level1Object.SetActive(false);
            level2Object.SetActive(true);
        } else if(currentLevel == 2){
            level2Object.SetActive(false);
            level3Object.SetActive(true);
        } else if(currentLevel == 3){
            level3Object.SetActive(false);
            inhibitor.SetActive(true);
            gameAudio.Stop();
        }
        currentLevel++;
        levelAudio.Play();
        UpdateLevelText();
    }

    void UpdatePointsText()
    {
        if (pointsText != null)
        {
            pointsText.text = "" + pointValue + "/6";
        }
    }

    void UpdateLevelText()
    {
        if (levelText != null)
        {
            levelText.text = "Lvl " + currentLevel;
        }
    }
}
