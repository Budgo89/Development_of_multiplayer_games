using Profile;
using Tool;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using View;

namespace Controllers.UI
{
    internal class ExitController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("UI/ExitMenu");

        private ExitMenuView _exitMenuView;
        private ProfilePlayers _profilePlayer;

        private Button _exitButton;
        private Button _backButton;

        public ExitController(Transform placeForUi, ProfilePlayers profilePlayer)
        {
            _exitMenuView = LoadView(placeForUi);
            _profilePlayer = profilePlayer;
            AddButton();
            SubscribeButton();
        }

        private ExitMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<ExitMenuView>();
        }
        private void AddButton()
        {
            _exitButton = _exitMenuView.ExitButton;
            _backButton = _exitMenuView.BackButton;
        }

        private void SubscribeButton()
        {
            _exitButton.onClick.AddListener(OnExitButtonClick);
            _backButton.onClick.AddListener(OnBackButtonClick);
        }

        private void OnBackButtonClick() => _profilePlayer.CurrentState.Value = GameState.MainMenu;

        private void UnsubscribeButton()
        {
            _exitButton.onClick.RemoveAllListeners();
            _backButton.onClick.RemoveAllListeners();
        }

        private void OnExitButtonClick()
        {
            SaveManagement.SetRestart(0);
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        protected override void OnDispose()
        {
            UnsubscribeButton();
        }
    }
}
