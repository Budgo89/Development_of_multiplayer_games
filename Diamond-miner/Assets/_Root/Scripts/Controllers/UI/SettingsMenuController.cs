using Profile;
using Tool;
using UnityEngine;
using UnityEngine.UI;
using View;

namespace Controllers
{
    internal class SettingsMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("UI/SettingsMenu");

        private Transform _placeForUi;
        private ProfilePlayers _profilePlayer;

        private Button _volumeButton;
        private Button _languageButton;
        private Button _backButton;

        private SettingsMenuView _settingsMenuView;

        public SettingsMenuController(Transform placeForUi, ProfilePlayers profilePlayer)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
            _settingsMenuView = LoadView(placeForUi);

            AddButtons();
            SubscribeButton();
        }

        private void AddButtons()
        {
            _volumeButton = _settingsMenuView.VolumeButton;
            _languageButton = _settingsMenuView.LanguageButton;
            _backButton = _settingsMenuView.BackButton;
        }

        private void SubscribeButton()
        {
            _volumeButton.onClick.AddListener(OnVolumeButtonClick);
            _languageButton.onClick.AddListener(OnLanguageButtonClick);
            _backButton.onClick.AddListener(OnBackButtonClick);
        }

        protected override void OnDispose()
        {
            UnsubscribeButton();
        }

        private void OnBackButtonClick() => _profilePlayer.CurrentState.Value = GameState.MainMenu;

        private void OnLanguageButtonClick() => _profilePlayer.CurrentState.Value = GameState.LanguageMenu;

        private void OnVolumeButtonClick() => _profilePlayer.CurrentState.Value = GameState.VolumeMenu;

        private void UnsubscribeButton()
        {
            _volumeButton.onClick.RemoveAllListeners();
            _languageButton.onClick.RemoveAllListeners();
            _backButton.onClick.RemoveAllListeners();
        }

        private SettingsMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<SettingsMenuView>();
        }
    }
}