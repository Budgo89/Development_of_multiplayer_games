using Profile;
using Tool;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using View;

namespace Controllers.UI
{
    internal class PauseMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("UI/PauseMenu");

        private Transform _placeForUi;
        private PauseManager _pauseManager;

        private PauseMenuView _pauseMenuView;

        private Button _toTheGameButton;
        private Button _repeatButton;
        private Button _mainMenuButton;

        public PauseMenuController(Transform placeForUi, PauseManager pauseManager)
        {
            _placeForUi = placeForUi;
            _pauseManager = pauseManager;

            _pauseMenuView = LoadView(placeForUi);

            AddButtons();
            SubscribeButton();
        }

        private void AddButtons()
        {
            _toTheGameButton = _pauseMenuView.ToTheGameButton;
            _repeatButton = _pauseMenuView.RepeatButton;
            _mainMenuButton = _pauseMenuView.MainMenuButton;
        }
        private void SubscribeButton()
        {
            _toTheGameButton.onClick.AddListener(OnToTheGameButtonClick);
            _repeatButton.onClick.AddListener(OnRepeatButtonClick);
            _mainMenuButton.onClick.AddListener(OnMainMenuButtonClick);
        }

        private void OnMainMenuButtonClick()
        {
            //SaveManagement.SetRestart(0);
            SaveManagement.SetGameState(0);
            SceneManager.LoadScene(0);
        }

        private void OnRepeatButtonClick()
        {
            //OnToTheGameButtonClick();
            //SaveManagement.SetRestart(1);
            SceneManager.LoadScene(1);
            //SceneManager.LoadScene("SampleScene");
        }

        private void OnToTheGameButtonClick()
        {
            _pauseManager.DisablePause();
            Object.Destroy(_pauseMenuView.gameObject);
        }

        private void UnsubscribeButton()
        {
            _toTheGameButton.onClick.RemoveAllListeners();
            _repeatButton.onClick.RemoveAllListeners();
            _mainMenuButton.onClick.RemoveAllListeners();
        }

        private PauseMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<PauseMenuView>();
        }

        protected override void OnDispose()
        {
            UnsubscribeButton();
        }
    }
}
