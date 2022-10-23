using Profile;
using Tool;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using View;

namespace Controllers
{
    internal class FartherMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("UI/FartherMenu");

        private Transform _placeForUi;
        private ProfilePlayers _profilePlayer;
        private LevelManager _levelManager;
        private GameLevel _gameLevel;

        private FartherMenuView _fartherMenuView;

        private Button _continueButton;
        private Button _restartButton;
        private Button _mainMenuButton;

        public FartherMenuController(Transform placeForUi, ProfilePlayers profilePlayer, LevelManager levelManager, GameLevel gameLevel)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
            _levelManager = levelManager;
            _gameLevel = gameLevel;

            _fartherMenuView = LoadView(placeForUi);
            AddButton();

            SubscribeButton();
        }

        private void AddButton()
        {
            _continueButton = _fartherMenuView.ContinueButton;
            _restartButton = _fartherMenuView.RestartButton;
            _mainMenuButton = _fartherMenuView.MainMenuButton;

            if (_levelManager.Levels.Count - 1 == _gameLevel.CurrentLevel)
            {
                _continueButton.gameObject.SetActive(false);
            }
        }

        private void SubscribeButton()
        {
            _continueButton.onClick.AddListener(OnContinueButtonClick);
            _restartButton.onClick.AddListener(OnRestartButtonClick);
            _mainMenuButton.onClick.AddListener(OnMainMenuButtonClick);
        }

        private void OnContinueButtonClick()
        {
            if (_gameLevel.CurrentLevel == _gameLevel.AvailableLevel)
            {
                _gameLevel.CurrentLevel++;
                _gameLevel.AvailableLevel++;
            }
            else if (_gameLevel.AvailableLevel > _gameLevel.CurrentLevel)
                _gameLevel.CurrentLevel++;
            SaveManagement.SetLevels(_gameLevel);
            //SaveManagement.SetRestart(1);
            SceneManager.LoadScene(1);
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
            _continueButton.onClick.RemoveAllListeners();
            _restartButton.onClick.RemoveAllListeners();
            _mainMenuButton.onClick.RemoveAllListeners();
        }

        protected override void OnDispose()
        {
            UnsubscribeButton();
        }

        private FartherMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<FartherMenuView>();
        }
    }
}