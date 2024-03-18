using System.Collections.Generic;
using Assets._Scripts.Managers;
using UnityEngine;

namespace Assets._Scripts.UiElements
{
    public class AnimalsGridCreator : MonoBehaviour
    {
        [SerializeField] private AnimalButton _animalButtonPrefab;
        [SerializeField] private Transform _animalsGrid;
        private List<AnimalButton> _animalButtons = new();

        private void Start()
        {
            CreateAnimalButtons();
            GameManager.instance.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState gameState)
        {
            if (gameState == GameState.Start)
            {
                SetAnimalButtonParent();
            }
        }

        private void SetAnimalButtonParent()
        {
            foreach (var animalButton in _animalButtons)
            {
                animalButton.SetParent(_animalsGrid);
                animalButton.gameObject.SetActive(true);
            }
        }

        private void CreateAnimalButtons()
        {
            // load the scriptable objects from the AnimalButtonSO folder in resources
            var animalButtonScriptableObjects = Resources.LoadAll<AnimalButtonSO>("AnimalButtonSO");

            foreach (var animalButtonScriptableObject in animalButtonScriptableObjects)
            {
                var animalButton = Instantiate(_animalButtonPrefab, _animalsGrid);
                animalButton.SetAnimalButton(animalButtonScriptableObject);
                _animalButtons.Add(animalButton);
            }
        }

        private void OnDestroy()
        {
            GameManager.instance.OnGameStateChanged -= OnGameStateChanged;
        }
    }
}