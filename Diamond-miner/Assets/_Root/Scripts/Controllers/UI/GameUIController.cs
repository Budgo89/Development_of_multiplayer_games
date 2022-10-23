using System;
using TMPro;
using Tool;
using UnityEngine;
using UnityEngine.UI;
using View;
using Object = UnityEngine.Object;

namespace Controllers.UI
{
    public class GameUIController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("UI/GameUI");

        private Transform _placeForUi;
        private int _diamondCount;
        private DiamondController _diamondController;
        private PauseManager _pauseManager;

        private GameUIView _gameUIView;

        private Button _pauseMenuButton;
        private TMP_Text _diamondСounts;
        private TMP_Text _gameTime;

        private TimeSpan _time;
        private float _seconds;

        private PauseMenuController _pauseMenuController;
        

        public GameUIController(Transform placeForUi, int diamondCount, DiamondController diamondController, PauseManager pauseManager)
        {
            _placeForUi = placeForUi;
            _diamondCount = diamondCount;
            _diamondController = diamondController;
            _pauseManager = pauseManager;

            _gameUIView = LoadView(placeForUi);

            AddElementsUi();
            SubscribeButton();

            _diamondСounts.text = _diamondCount.ToString();

            _diamondController.DiamondRaised += DiamondСounts;
           
        }

        public void Update(float deltaTime)
        {
            DisplayTimer(deltaTime);
            
        }

        private void DiamondСounts()
        {
            _diamondCount--;
            _diamondСounts.text = _diamondCount.ToString();
        }

        private void SubscribeButton()
        {
            _pauseMenuButton.onClick.AddListener(OnPauseMenuButtonClick);
        }

        private void DisplayTimer(float deltaTime)
        {
            _seconds += deltaTime;
            _time = TimeSpan.FromSeconds(_seconds);

            _gameTime.text = $"{_time.Hours:00}:{_time.Minutes:00}:{_time.Seconds:00}";
        }

        private void OnPauseMenuButtonClick()
        {
            _pauseManager.EnablePause();
            _pauseMenuController = new PauseMenuController(_placeForUi, _pauseManager);
        }

        private void UnsubscribeButton()
        {
            _pauseMenuButton.onClick.RemoveAllListeners();
        }
        private void AddElementsUi()
        {
            _pauseMenuButton = _gameUIView.PauseMenuButton;
            _gameTime = _gameUIView.GameTime;
            _diamondСounts = _gameUIView.DiamondСounts;
        }

        private GameUIView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<GameUIView>();
        }

        protected override void OnDispose()
        {
            _pauseMenuController?.Dispose();
            _diamondСounts.text = string.Empty;
            _diamondController.DiamondRaised -= DiamondСounts;
            UnsubscribeButton();
            _diamondController?.Dispose();
        }
    }
}
