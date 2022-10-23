using Profile;
using Tool;
using UnityEngine;
using UnityEngine.UI;
using View;

namespace Controllers.UI
{
    internal class MainMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("UI/MainMenu");

        private Transform _placeForUi;
        private ProfilePlayers _profilePlayer;

        private MainMenuView _mainMenuView;

        private Button _startGameButton;
        private Button _levelButton;
        private Button _settingsButton;
        private Button _exitButton;

        public MainMenuController(Transform placeForUi, ProfilePlayers profilePlayer)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;

            _mainMenuView = LoadView(placeForUi);
            AddButton();
            SubscribeButton();
        }
        private MainMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<MainMenuView>();
        }

        private void AddButton()
        {
            _startGameButton = _mainMenuView.StartGameButton;
            _levelButton = _mainMenuView.LevelButton;
            _settingsButton = _mainMenuView.SettingsButton;
            _exitButton = _mainMenuView.ExitButton;
        }

        private void SubscribeButton()
        {
            _startGameButton.onClick.AddListener(OnStartGameButtonClick);
            _levelButton.onClick.AddListener(OnLevelButtonClick);
            _settingsButton.onClick.AddListener(OnSettingsButtonClick);
            _exitButton.onClick.AddListener(OnExitButtonClick);
        }

        private void OnStartGameButtonClick() => _profilePlayer.CurrentState.Value = GameState.Game;

        private void OnLevelButtonClick() => _profilePlayer.CurrentState.Value = GameState.LevelMenu;

        private void OnSettingsButtonClick() => _profilePlayer.CurrentState.Value = GameState.SettingsMenu;

        private void OnExitButtonClick() => _profilePlayer.CurrentState.Value = GameState.ExitMenu;

        private void UnsubscribeButton()
        {
            _startGameButton.onClick.RemoveAllListeners();
            _levelButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
        }
        protected override void OnDispose()
        {
            UnsubscribeButton();
        }
    }
}