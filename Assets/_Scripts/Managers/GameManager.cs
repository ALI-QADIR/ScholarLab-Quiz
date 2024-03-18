using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    /// <summary>
    /// The GameManager class manages the game state and provides various game-related functionalities.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// The instance of the GameManager class.
        /// </summary>
        public static GameManager instance;

        /// <summary>
        /// Event that is triggered when the game state changes.
        /// </summary>
        public event Action<GameState> OnGameStateChanged;

        /// <summary>
        /// The current game state.
        /// </summary>
        public GameState GameState { get; private set; }

        /// <summary>
        /// The sort attribute used in the game.
        /// </summary>
        public SortAttribute SortAttribute { get; private set; }

        /// <summary>
        /// The number of correct answers.
        /// </summary>
        public int Correct => _correct;

        /// <summary>
        /// The number of incorrect answers.
        /// </summary>
        public int Incorrect => _incorrect;

        private const int _TOTAL = 18;
        private int _current = 0;
        private int _correct;
        private int _incorrect;
        private List<Tuple<string, Sprite>> _incorrectCardNamesAndSpritesTuple = new();

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            GameStartDelay();
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
            _incorrectCardNamesAndSpritesTuple.Clear();
            // Randomize the sort attribute
        }

        /// <summary>
        /// Increments the number of correct answers and checks if the game is over.
        /// </summary>
        public void IncrementCorrect()
        {
            _current++;
            _correct++;
            if (_current == _TOTAL)
            {
                UpdateGameState(GameState.GameOver);
            }
        }

        /// <summary>
        /// Increments the number of incorrect answers, adds the incorrect card name and sprite to the list, and checks if the game is over.
        /// </summary>
        /// <param name="cardName">The name of the incorrect card.</param>
        /// <param name="cardSprite">The sprite of the incorrect card.</param>
        public void IncrementIncorrect(string cardName, Sprite cardSprite)
        {
            _current++;
            _incorrect++;
            _incorrectCardNamesAndSpritesTuple.Add(new Tuple<string, Sprite>(cardName, cardSprite));
            if (_current == _TOTAL)
            {
                UpdateGameState(GameState.GameOver);
            }
        }

        /// <summary>
        /// Updates the game state to the specified next state.
        /// </summary>
        /// <param name="nextState">The next game state.</param>
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

        private async void GameStartDelay()
        {
            await Task.Delay(20);
            UpdateGameState(GameState.Start);
        }

        /// <summary>
        /// Gets the incorrect card name and sprite at the specified index.
        /// </summary>
        /// <param name="i">The index of the incorrect card.</param>
        /// <returns>The tuple containing the incorrect card name and sprite.</returns>
        public Tuple<string, Sprite> GetIncorrectCardNameAndImage(int i)
        {
            return _incorrectCardNamesAndSpritesTuple[i];
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