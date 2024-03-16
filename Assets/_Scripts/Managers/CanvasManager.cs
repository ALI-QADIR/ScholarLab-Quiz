using TMPro;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    public class CanvasManager : MonoBehaviour
    {
        [SerializeField] private Transform _startPanel;
        [SerializeField] private Transform _sortPanel;
        [SerializeField] private Transform _finishPanel;

        [SerializeField] private TextMeshProUGUI _start;
        [SerializeField] private TextMeshProUGUI _correct;
        [SerializeField] private TextMeshProUGUI _incorrect;

        [SerializeField] private TextMeshProUGUI _redBucketText;
        [SerializeField] private TextMeshProUGUI _blueBucketText;

        private void Awake()
        {
            _startPanel.gameObject.SetActive(true);
            _sortPanel.gameObject.SetActive(false);
            _finishPanel.gameObject.SetActive(false);
        }

        private void Start()
        {
            GameManager.Instance.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState state)
        {
            switch (state)
            {
                case GameState.Start:
                    _startPanel.gameObject.SetActive(true);
                    _sortPanel.gameObject.SetActive(false);
                    _finishPanel.gameObject.SetActive(false);
                    string sortAttribute = SortAttributeString();
                    _start.text = sortAttribute;
                    break;

                case GameState.Playing:
                    _startPanel.gameObject.SetActive(false);
                    _sortPanel.gameObject.SetActive(true);
                    _finishPanel.gameObject.SetActive(false);
                    _redBucketText.text = GetBucketString(true);
                    _blueBucketText.text = GetBucketString(false);
                    break;

                case GameState.GameOver:
                    _startPanel.gameObject.SetActive(false);
                    _sortPanel.gameObject.SetActive(false);
                    _finishPanel.gameObject.SetActive(true);
                    _correct.text = $"Correct: {GameManager.Instance.Correct}";
                    _incorrect.text = $"Incorrect: {GameManager.Instance.Incorrect}";
                    break;
            }
        }

        private string GetBucketString(bool b)
        {
            return GameManager.Instance.SortAttribute switch
            {
                SortAttribute.Flier => b ? "Flies" : "Doesn't Fly",
                SortAttribute.Insect => b ? "Insect" : "Not an Insect",
                SortAttribute.Omnivore => b ? "Omnivore" : "Herbivore",
                SortAttribute.SoloLiver => b ? "Lives Alone" : "Lives in a Group",
                SortAttribute.Mammal => b ? "Mammal" : "Lays Eggs",
                _ => "Unknown"
            };
        }

        private string SortAttributeString()
        {
            return GameManager.Instance.SortAttribute switch
            {
                SortAttribute.Flier => "Sort if the animal Flies or not",
                SortAttribute.Insect => "Sort if the animal is an Insect or not",
                SortAttribute.Omnivore => "Sort if the animal is an Omnivore or Herbivore",
                SortAttribute.SoloLiver => "Sort if the animal lives alone or in a group",
                SortAttribute.Mammal => "Sort if the animal is a Mammal or Lays Eggs",
                _ => "Unknown"
            };
        }

        public void PlayGame()
        {
            GameManager.Instance.UpdateGameState(GameState.Playing);
        }

        public void RestartGame()
        {
            GameManager.Instance.UpdateGameState(GameState.Start);
        }
    }
}