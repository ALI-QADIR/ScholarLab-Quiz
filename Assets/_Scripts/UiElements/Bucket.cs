using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets._Scripts.Managers;

namespace Assets._Scripts.UiElements
{
    public class Bucket : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TextMeshProUGUI _bucketTitleText;
        [SerializeField] private Image _bucketImage;
        [SerializeField] private bool _bucketValue;
        private Color _bucketSpriteColor;

        private AnimalButton _animalButton;

        private void Start()
        {
            _bucketSpriteColor = _bucketImage.color;
        }

        public void OnDrop(PointerEventData eventData)
        {
            _animalButton = eventData.pointerDrag.GetComponent<AnimalButton>();
            _animalButton.SetParent(transform.parent);
            _animalButton.DroppedInBucket();
            string animalName = _animalButton.GetAnimalName();
            Sprite animalSprite = _animalButton.GetAnimalSprite();

            switch (GameManager.Instance.SortAttribute)
            {
                case SortAttribute.Flier:
                    if (_bucketValue == _animalButton.GetAnimalAttribute().isFlier)
                    {
                        GameManager.Instance.IncrementCorrect();
                    }
                    else
                    {
                        animalName += (_animalButton.GetAnimalAttribute().isFlier ? " Flies" : " No Fly");
                        GameManager.Instance.IncrementIncorrect(animalName, animalSprite);
                    }
                    break;

                case SortAttribute.Insect:
                    if (_bucketValue == _animalButton.GetAnimalAttribute().isInsect)
                    {
                        GameManager.Instance.IncrementCorrect();
                    }
                    else
                    {
                        animalName += (_animalButton.GetAnimalAttribute().isInsect ? " Insect" : " No Insect");
                        GameManager.Instance.IncrementIncorrect(animalName, animalSprite);
                    }
                    break;

                case SortAttribute.Omnivore:
                    if (_bucketValue == _animalButton.GetAnimalAttribute().isOmnivore)
                    {
                        GameManager.Instance.IncrementCorrect();
                    }
                    else
                    {
                        animalName += (_animalButton.GetAnimalAttribute().isOmnivore ? " Omnivore" : " Herbivore");
                        GameManager.Instance.IncrementIncorrect(animalName, animalSprite);
                    }
                    break;

                case SortAttribute.SoloLiver:
                    if (_bucketValue == _animalButton.GetAnimalAttribute().isSoloLiver)
                    {
                        GameManager.Instance.IncrementCorrect();
                    }
                    else
                    {
                        animalName += (_animalButton.GetAnimalAttribute().isSoloLiver ? " Lives Alone" : " Lives in a Group");
                        GameManager.Instance.IncrementIncorrect(animalName, animalSprite);
                    }
                    break;

                case SortAttribute.Mammal:
                    if (_bucketValue == _animalButton.GetAnimalAttribute().isMammal)
                    {
                        GameManager.Instance.IncrementCorrect();
                    }
                    else
                    {
                        animalName += (_animalButton.GetAnimalAttribute().isMammal ? " Mammal" : " Lays Eggs");
                        GameManager.Instance.IncrementIncorrect(animalName, animalSprite);
                    }
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null) return;
            _bucketImage.color = Color.white;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _bucketImage.color = _bucketSpriteColor;
        }
    }
}