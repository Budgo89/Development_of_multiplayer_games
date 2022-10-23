using UnityEngine;
using UnityEngine.UI;

namespace View
{
    internal class VolumeMenuView : MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _saveButton;
        [SerializeField] private Slider _volumeSlider;

        public Button BackButton => _backButton;
        public Button SaveButton => _saveButton;
        public Slider VolumeSlider => _volumeSlider;
    }
}
