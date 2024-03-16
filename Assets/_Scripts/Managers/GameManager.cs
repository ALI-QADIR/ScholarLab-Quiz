using System;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public event Action<GameState> OnGameStateChanged;

        public GameState GameState { get; private set; }
        public SortAttribute SortAttribute { get; private set; }

        public int Correct => _correct;
        public int Incorrect => _incorrect;

        private readonly int _total = 18;
        private int _current = 0;
        private int _correct;
        private int _incorrect;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            UpdateGameState(GameState.Start);
        }

        private void GameOver()
        {
            OnGameStateChanged?.Invoke(GameState);
        }

        private void GamePlaying()
        {
            OnGameStateChanged?.Invoke(GameState);
        }

        private void GameStarted()
        {
            SortAttribute = (SortAttribute)UnityEngine.Random.Range(0, Enum.GetValues(typeof(SortAttribute)).Length);
            OnGameStateChanged?.Invoke(GameState);
            _current = 0;
            _correct = 0;
            _incorrect = 0;
            // Randomize the sort attribute
        }

        public void IncrementCorrect()
        {
            _current++;
            _correct++;
            if (_current == _total)
            {
                UpdateGameState(GameState.GameOver);
            }
        }

        public void IncrementIncorrect()
        {
            _current++;
            _incorrect++;
            if (_current == _total)
            {
                UpdateGameState(GameState.GameOver);
            }
        }

        public void UpdateGameState(GameState nextState)
        {
            GameState = nextState;
            switch (GameState)
            {
                case GameState.Start:
                    GameStarted();
                    break;

                case GameState.Playing:
                    GamePlaying();
                    break;

                case GameState.GameOver:
                    GameOver();
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public enum GameState
    {
        Start,
        Playing,
        GameOver,
    }

    public enum SortAttribute
    {
        Flier,
        Insect,
        Omnivore,
        SoloLiver,
        Mammal
    }
}