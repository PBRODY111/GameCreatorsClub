using TMPro;
using UnityEngine;

namespace Scene1.Computer.Styx
{
    public class GameController : MonoBehaviour
    {
        public int pointValue;
        public int currentLevel = 1;

        public TextMeshProUGUI pointsText;
        public TextMeshProUGUI levelText;

        public Transform playerTransform;
        [SerializeField] private GameObject level1Object;
        [SerializeField] private GameObject level2Object;
        [SerializeField] private GameObject level3Object;
        [SerializeField] private GameObject inhibitor;
        public AudioSource levelAudio;
        public AudioSource gameAudio;

        private void Start()
        {
            QualitySettings.vSyncCount = 1;
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
                    playerTransform.localPosition = Vector3.zero;
            }

            UpdatePointsText();
        }

        private void ChangeLevel()
        {
            switch (currentLevel)
            {
                case 1:
                    level1Object.SetActive(false);
                    level2Object.SetActive(true);
                    break;
                case 2:
                    level2Object.SetActive(false);
                    level3Object.SetActive(true);
                    break;
                case 3:
                    level3Object.SetActive(false);
                    inhibitor.SetActive(true);
                    gameAudio.Stop();
                    break;
            }

            currentLevel++;
            levelAudio.Play();
            UpdateLevelText();
        }

        private void UpdatePointsText()
        {
            if (pointsText != null) pointsText.text = "" + pointValue + "/6";
        }

        private void UpdateLevelText()
        {
            if (levelText != null) levelText.text = "Lvl " + currentLevel;
        }
    }
}