using System;
using Controllers.UI;
using MB;
using Profile;
using Tool;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class MainController : BaseController
    {
        private ProfilePlayers _profilePlayer;
        private Transform _placeForUi;
        private GameLevel _gameLevel;
        private TileMapScanner _tileMapScanner;
        private LevelManager _levelManager;
        private PauseManager _pauseManager;
        private AudioMixer _audioMixer;
        private AudioEffectsManager _audioEffectsManager;
        private SwipeDetection _swipeDetection;

        private MainMenuController _mainMenuController;
        private SettingsMenuController _settingsMenuController;
        private VolumeMenuController _volumeMenuController;
        private LanguageMenuController _languageMenuController;
        private GameController _gameController;

        private ExitController _exitController;
        private LevelMenuController _levelMenuController;
        private FartherMenuController _fartherMenuController;
        private GameOverMenuController _gameOverMenuController;
        

        public MainController(ProfilePlayers profilePlayer, Transform placeForUi, GameLevel gameLevel, TileMapScanner tileMapScanner, LevelManager levelManager, PauseManager pauseManager, AudioMixer audioMixer, AudioEffectsManager audioEffectsManager, SwipeDetection swipeDetection)
        {
            _profilePlayer = profilePlayer;
            _placeForUi = placeForUi;
            _gameLevel = gameLevel;
            _tileMapScanner = tileMapScanner;
            _levelManager = levelManager;
            _pauseManager = pauseManager;
            _audioMixer = audioMixer;
            _audioEffectsManager = audioEffectsManager;
            _swipeDetection = swipeDetection;

            profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
            OnChangeGameState(_profilePlayer.CurrentState.Value);
        }

        public void Update(float deltaTime)
        {
            _gameController?.Update(deltaTime);
        }
        
        protected override void OnDispose()
        {
            DisposeControllers();
        }

        private void OnChangeGameState(GameState state)
        {
            DisposeControllers();
            switch (state)
            {
                case GameState.Game:
                    SceneManager.LoadScene(1);
                    break;
                case GameState.MainMenu:
                    _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                    break;
                case GameState.LevelMenu:
                    _levelMenuController = new LevelMenuController(_placeForUi, _profilePlayer, _gameLevel);
                    break;
                case GameState.SettingsMenu:
                    _settingsMenuController = new SettingsMenuController(_placeForUi, _profilePlayer);
                    break;
                case GameState.LanguageMenu:
                    _languageMenuController = new LanguageMenuController(_placeForUi, _profilePlayer);
                    break;
                case GameState.VolumeMenu:
                    _volumeMenuController = new VolumeMenuController(_placeForUi, _profilePlayer, _audioMixer);
                    break;
                case GameState.FartherMenu:
                    _fartherMenuController = new FartherMenuController(_placeForUi, _profilePlayer, _levelManager, _gameLevel);
                    break;
                case GameState.GameOverMenu:
                    _gameOverMenuController = new GameOverMenuController(_placeForUi, _profilePlayer);
                    break;
                case GameState.ExitMenu:
                    _exitController = new ExitController(_placeForUi, _profilePlayer);
                    break;
            }
        }

        private void DisposeControllers()
        {
            _levelMenuController?.Dispose();
            _gameOverMenuController?.Dispose();
            _exitController?.Dispose();
            _settingsMenuController?.Dispose();
            _languageMenuController?.Dispose();
            _volumeMenuController?.Dispose();
            _fartherMenuController?.Dispose();
            _gameController?.Dispose();
            _mainMenuController?.Dispose();
            
        }
    }
}
