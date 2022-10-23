
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    internal class SettingsMenuView : MonoBehaviour
    {
        [SerializeField] private Button _volumeButton;
        [SerializeField] private Button _languageButton;
        [SerializeField] private Button _backButton;

        public Button VolumeButton => _volumeButton;
        public Button LanguageButton => _languageButton;
        public Button BackButton => _backButton;
    }
}
