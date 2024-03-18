using TMPro;
using UnityEngine;

namespace Assets._Scripts.UiElements
{
    /// <summary>
    /// Represents the info panel in the UI.
    /// </summary>
    public class InfoPanel : MonoBehaviour
    {
        /// <summary>
        /// The instance of the InfoPanel.
        /// </summary>
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

        /// <summary>
        /// Scales down the info panel.
        /// </summary>
        public void ScaleInfoPanelDown()
        {
            _isPanelUp = false;
        }

        /// <summary>
        /// Scales up the info panel and sets the animal name and description.
        /// </summary>
        /// <param name="animalName">The name of the animal.</param>
        /// <param name="animalDescription">The description of the animal.</param>
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