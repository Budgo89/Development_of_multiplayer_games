using Profile;
using Tool;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using View;

namespace Controllers
{
    internal class LanguageMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("UI/LanguageMenu");

        private Transform _placeForUi;
        private ProfilePlayers _profilePlayer;

        private LanguageMenuView _languageMenuView;

        private Button _backButton;
        private Button _russianButton;
        private Button _englishButton;

        public LanguageMenuController(Transform placeForUi, ProfilePlayers profilePlayer)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
            _languageMenuView = LoadView(placeForUi);
            AddButtons();
            SubscribeButton();
        }

        private void AddButtons()
        {
            _backButton = _languageMenuView.BackButton;
            _russianButton = _languageMenuView.RussianButton;
            _englishButton = _languageMenuView.EnglishButton;
        }

        private void SubscribeButton()
        {
            _backButton.onClick.AddListener(OnBackButtonClick);
            _englishButton.onClick.AddListener(() => ChangeLanguage(0));
            _backButton.onClick.AddListener(() => ChangeLanguage(1));
        }

        private void ChangeLanguage(int index) { LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index]; }
        private void OnBackButtonClick() => _profilePlayer.CurrentState.Value = GameState.SettingsMenu;

        private void UnsubscribeButton()
        {
            _backButton.onClick.RemoveAllListeners();
            _englishButton.onClick.RemoveAllListeners();
            _backButton.onClick.RemoveAllListeners();
        }

        protected override void OnDispose()
        {
            UnsubscribeButton();
        }
        
        private LanguageMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<LanguageMenuView>();
        }
    }
}