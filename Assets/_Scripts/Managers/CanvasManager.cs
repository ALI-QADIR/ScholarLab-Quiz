using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Managers
{
    public class CanvasManager : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] private Transform _startPanel;

        [SerializeField] private Transform _sortPanel;
        [SerializeField] private Transform _finishPanel;
        [SerializeField] private Transform _incorrectCardsPanel;

        [Header("Text objects")]
        [SerializeField] private TextMeshProUGUI _start;

        [SerializeField] private TextMeshProUGUI _correct;
        [SerializeField] private TextMeshProUGUI _incorrect;

        [Header("Prefab"), SerializeField] private GameObject _incorrectCardPrefab;

        [Header("Bucket Text")]
        [SerializeField] private TextMeshProUGUI _redBucketText;

        [SerializeField] private TextMeshProUGUI _blueBucketText;

        private List<GameObject> _incorrectCardsPool = new();

        private void Awake()
        {
            _startPanel.gameObject.SetActive(true);
            _sortPanel.gameObject.SetActive(false);
            _finishPanel.gameObject.SetActive(false);
            for (int i = 0; i < 18; i++)
            {
                var card = Instantiate(_incorrectCardPrefab, _incorrectCardsPanel);
                card.gameObject.SetActive(false);
                _incorrectCardsPool.Add(card);
            }
        }

        private void Start()
        {
            GameManager.instance.OnGameStateChanged += OnGameStateChanged;
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
                    GameOver();
                    break;
            }
        }

        private void GameOver()
        {
            _startPanel.gameObject.SetActive(false);
            _sortPanel.gameObject.SetActive(false);
            _finishPanel.gameObject.SetActive(true);
            int correct = GameManager.instance.Correct;
            int incorrect = GameManager.instance.Incorrect;
            _correct.text = $"Correct: {correct}";
            _incorrect.text = $"Incorrect: {incorrect}";
            for (int i = 0; i < 18; i++)
            {
                if (i < incorrect)
                {
                    _incorrectCardsPool[i].gameObject.SetActive(true);
                    Tuple<string, Sprite> tuple = GameManager.instance.GetIncorrectCardNameAndImage(i);
                    _incorrectCardsPool[i].GetComponentInChildren<TextMeshProUGUI>().text = tuple.Item1;
                    _incorrectCardsPool[i].GetComponentInChildren<Image>().sprite = tuple.Item2;
                }
                else
                {
                    _incorrectCardsPool[i].gameObject.SetActive(false);
                }
            }
        }

        private string GetBucketString(bool b)
        {
            return GameManager.instance.SortAttribute switch
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
            return GameManager.instance.SortAttribute switch
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
            GameManager.instance.UpdateGameState(GameState.Playing);
        }

        public void RestartGame()
        {
            GameManager.instance.UpdateGameState(GameState.Start);
        }
    }
}