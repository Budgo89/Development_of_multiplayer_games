using Profile;
using Tool;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using View;

namespace Controllers
{
    internal class GameOverMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("UI/GameOverMenu");
        private Transform _placeForUi;
        private ProfilePlayers _profilePlayer;

        private GameOverMenuView _gameOverMenuView;

        private Button _restartButton;
        private Button _mainMenuButton;

        public GameOverMenuController(Transform placeForUi, ProfilePlayers profilePlayer)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;

            _gameOverMenuView = LoadView(placeForUi);
            AddButton();
            SubscribeButton();
        }

        private void AddButton()
        {
            _restartButton = _gameOverMenuView.RestartButton;
            _mainMenuButton = _gameOverMenuView.MainMenuButton;
        }

        private void SubscribeButton()
        {
            _restartButton.onClick.AddListener(OnRestartButtonClick);
            _mainMenuButton.onClick.AddListener(OnMainMenuButtonClick);
        }

        private void OnMainMenuButtonClick()
        {
            //SaveManagement.SetRestart(0);
            //SceneManager.LoadScene(0);
            _profilePlayer.CurrentState.Value = GameState.MainMenu;
        }

        private void OnRestartButtonClick()
        {
            //SaveManagement.SetRestart(1);
            SceneManager.LoadScene(1);
        }

        private void UnsubscribeButton()
        {
            _restartButton.onClick.RemoveAllListeners();
            _mainMenuButton.onClick.RemoveAllListeners();
        }

        protected override void OnDispose()
        {
            UnsubscribeButton();
        }

        private GameOverMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<GameOverMenuView>();
        }
    }
}