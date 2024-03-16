using TMPro;
using UnityEngine;

namespace Assets._Scripts.UiElements
{
    public class InfoPanel : MonoBehaviour
    {
        public static InfoPanel Instance;

        [SerializeField] private TextMeshProUGUI _animalNameText;
        [SerializeField] private TextMeshProUGUI _animalDescriptionText;

        private bool _isPanelUp;

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
            ScaleInfoPanelDown();
        }

        public void ScaleInfoPanelDown()
        {
            _isPanelUp = false;
        }

        public void ScaleInfoPanelUp(string animalName, string animalDescription)
        {
            _animalNameText.text = animalName;
            _animalDescriptionText.text = animalDescription;
            _isPanelUp = true;
        }

        private void FixedUpdate()
        {
            if (_isPanelUp && transform.localScale.x < 1)
            {
                transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            }
            else if (!_isPanelUp && transform.localScale.x > 0)
            {
                transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
            }
        }
    }
}