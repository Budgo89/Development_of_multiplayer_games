using System.Collections.Generic;
using Profile;
using TMPro;
using Tool;
using UnityEngine;
using UnityEngine.UI;
using View;

namespace Controllers.UI
{
    internal class LevelMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("UI/LevelMenu");

        private Transform _placeForUi;
        private ProfilePlayers _profilePlayer;
        private GameLevel _gameLevel;

        private LevelMenuView _levelMenuView;

        private int _currentLevel;

        private Button _nextButton;
        private Button _previousButton;
        private TMP_Text _levelText;
        private Button _backButton;
        private Button _setButton;


        public LevelMenuController(Transform placeForUi, ProfilePlayers profilePlayer, GameLevel gameLevel)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
            _gameLevel = gameLevel;

            _currentLevel = gameLevel.CurrentLevel;

            _levelMenuView = LoadView(placeForUi);
            AddButton();
            SubscribeButton();
            ActiveButton();
        }

        private void ActiveButton()
        {
            _previousButton.gameObject.SetActive(_currentLevel != 0);
            _nextButton.gameObject.SetActive(_currentLevel != _gameLevel.AvailableLevel);
            _levelText.text = (_currentLevel + 1).ToString();
        }

        private void AddButton()
        {
            _nextButton = _levelMenuView.NextButton;
            _previousButton = _levelMenuView.PrevButton;
            _backButton = _levelMenuView.BackButton;
            _setButton = _levelMenuView.SetButton;
            _levelText = _levelMenuView.LevelText;
        }

        private void SubscribeButton()
        {
            _nextButton.onClick.AddListener(OnNextButtonClick);
            _previousButton.onClick.AddListener(OnPreviousButtonClick);
            _backButton.onClick.AddListener(OnBackButtonClick);
            _setButton.onClick.AddListener(OnSetButtonClick);
        }

        private void OnSetButtonClick()
        {
            _gameLevel.CurrentLevel = _currentLevel;
            _profilePlayer.CurrentState.Value = GameState.Game;
        }

        private void OnBackButtonClick() => _profilePlayer.CurrentState.Value = GameState.MainMenu;

        private void OnPreviousButtonClick()
        {
            if (_currentLevel != 0)
            {
                _currentLevel--;
                ActiveButton();
            }
        }

        private void OnNextButtonClick()
        {
            if (_currentLevel != _gameLevel.AvailableLevel)
            {
                _currentLevel++;
                ActiveButton();
            }
        }

        protected override void OnDispose()
        {
            UnsubscribeButton();
        }

        private void UnsubscribeButton()
        {
            _nextButton.onClick.RemoveAllListeners();
            _previousButton.onClick.RemoveAllListeners();
            _backButton.onClick.RemoveAllListeners();
            _setButton.onClick.RemoveAllListeners();
        }

        private LevelMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<LevelMenuView>();
        }
    }
}