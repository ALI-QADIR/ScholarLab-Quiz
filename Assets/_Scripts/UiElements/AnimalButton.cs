using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Assets._Scripts.UiElements
{
    /// <summary>
    /// Represents a button that displays an animal.
    /// </summary>
    public class AnimalButton : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private Button _animalButton;
        [SerializeField] private Image _animalImage;
        private AnimalButtonSO _animalButtonScriptableObject;
        private Transform _parentTransform;

        private void Start()
        {
            _animalButton.onClick.AddListener(() =>
            {
                InfoPanel.Instance.ScaleInfoPanelUp(_animalButtonScriptableObject.animalName, _animalButtonScriptableObject.animalDescription);
            });
        }

        /// <summary>
        /// Sets the animal button with the specified scriptable object.
        /// </summary>
        /// <param name="animalButtonScriptableObject">The scriptable object representing the animal button.</param>
        public void SetAnimalButton(AnimalButtonSO animalButtonScriptableObject)
        {
            _animalButtonScriptableObject = animalButtonScriptableObject;
            _animalImage.sprite = _animalButtonScriptableObject.animalSprite;
        }

        /// <summary>
        /// Gets the animal button's scriptable object.
        /// </summary>
        /// <returns>The scriptable object representing the animal button.</returns>
        public AnimalButtonSO GetAnimalAttribute() => _animalButtonScriptableObject;

        /// <summary>
        /// Gets the name of the animal.
        /// </summary>
        /// <returns>The name of the animal.</returns>
        public string GetAnimalName() => _animalButtonScriptableObject.animalName;

        /// <summary>
        /// Gets the sprite of the animal.
        /// </summary>
        /// <returns>The sprite of the animal.</returns>
        public Sprite GetAnimalSprite() => _animalButtonScriptableObject.animalSprite;

        public void OnBeginDrag(PointerEventData eventData)
        {
            _parentTransform = transform.parent;
            transform.SetParent(transform.root);
            _animalImage.raycastTarget = false;
            _animalImage.color = Color.green;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.SetParent(_parentTransform);
            _animalImage.raycastTarget = true;
            _animalImage.color = Color.white;
        }

        public void DroppedInBucket()
        {
            _animalImage.raycastTarget = true;
            _animalImage.color = Color.white;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void SetParent(Transform t)
        {
            _parentTransform = t;
            transform.SetParent(t);
            gameObject.SetActive(false);
        }
    }
}