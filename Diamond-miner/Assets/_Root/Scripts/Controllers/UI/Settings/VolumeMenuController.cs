using System;
using System.Text.RegularExpressions;
using Profile;
using Tool;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using View;
using Object = UnityEngine.Object;

namespace Controllers
{
    internal class VolumeMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("UI/VolumeMenu");

        private Transform _placeForUi;
        private ProfilePlayers _profilePlayer;
        private AudioMixer _audioMixer;

        private VolumeMenuView _volumeMenuView;

        private Button _backButton;
        private Button _saveButton;
        private Slider _volumeSlider;

        private float _volume;

        public VolumeMenuController(Transform placeForUi, ProfilePlayers profilePlayer, AudioMixer audioMixer)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
            _audioMixer = audioMixer;
            _volumeMenuView = LoadView(placeForUi);
            AddButtons();
            _volume = SaveManagement.GetVolume();
            _volumeSlider.value = _volume;
            SubscribeButton();
            float b = 0f;
            _audioMixer.GetFloat("volume", out b);
            Debug.Log(b);
        }

        private void AddButtons()
        {
            _backButton = _volumeMenuView.BackButton;
            _saveButton = _volumeMenuView.SaveButton;
            _volumeSlider = _volumeMenuView.VolumeSlider;
        }

        private void SubscribeButton()
        {
            _backButton.onClick.AddListener(OnBackButtonClick);
            _saveButton.onClick.AddListener(OnSaveButtonClick);
            _volumeSlider.onValueChanged.AddListener(OcVolumeSliderChanged);
        }

        private void OcVolumeSliderChanged(float arg0)
        {
            _volume = arg0 ;
            _audioMixer.SetFloat("volume", (float)(Math.Log10(_volume) * 20));
        }

        private void OnSaveButtonClick()
        {
            SaveManagement.SetVolume(_volume);
            _profilePlayer.CurrentState.Value = GameState.SettingsMenu;
        }

        private void OnBackButtonClick() => _profilePlayer.CurrentState.Value = GameState.SettingsMenu;

        private void UnsubscribeButton()
        {
            _backButton.onClick.RemoveAllListeners();
        }

        protected override void OnDispose()
        {
            UnsubscribeButton();
        }

        private VolumeMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<VolumeMenuView>();
        }
    }
}