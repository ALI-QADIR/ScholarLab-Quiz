using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Assets._Scripts.UiElements
{
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

        public void SetAnimalButton(AnimalButtonSO animalButtonScriptableObject)
        {
            _animalButtonScriptableObject = animalButtonScriptableObject;
            _animalImage.sprite = _animalButtonScriptableObject.animalSprite;
        }

        public AnimalButtonSO GetAnimalAttribute() => _animalButtonScriptableObject;

        public string GetAnimalName() => _animalButtonScriptableObject.animalName;

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