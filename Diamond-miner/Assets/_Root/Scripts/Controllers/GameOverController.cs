using System.Collections.Generic;
using MB;
using Profile;
using Tool;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    internal class GameOverController : BaseController
    {
        private Player _player;
        private int _diamondCount;
        private DiamondController _diamondController;
        private PauseManager _pauseManager;
        private List<GameObject> _enemys;
        private AudioEffectsManager _audioEffectsManager;

        private Collider2D _colliderPlayer;

        private bool _isFarther = false;
        private bool _isGameOver = false;

        private float _gameOverTime = 1;
        private float _fartherTime = 3;
        private float _time = 0;

        public GameOverController(Player player,int diamondCount, DiamondController diamondController, PauseManager pauseManager, List<GameObject> enemys, AudioEffectsManager audioEffectsManager)
        {
            _player = player;
            _diamondCount = diamondCount;
            _diamondController = diamondController;
            _pauseManager = pauseManager;
            _enemys = enemys;
            _audioEffectsManager = audioEffectsManager;

            _colliderPlayer = _player.GetComponent<Collider2D>();

            _diamondController.DiamondRaised += DiamondСheck;
            foreach (var emeny in _enemys)
            {
                emeny.gameObject.GetComponent<EnemyView>().EnemyContact += PlayerCheck;
            }
        }

        public void Update(float deltaTime)
        {
            if (_isFarther)
            {
                _time += deltaTime;
                if (_time >= _fartherTime)
                {
                    FartherMenuStart();
                    _isFarther = false;
                }
            }

            if (_isGameOver)
            {
                _time += deltaTime;
                if (_time >= _gameOverTime)
                {
                    GameOverStart();
                    _isGameOver = false;
                }
            }
        }

        private void GameOverStart()
        {
            SaveManagement.SetGameState(8);
            SceneManager.LoadScene(0);
            //_profilePlayer.CurrentState.Value = GameState.GameOverMenu;
        }

        private void FartherMenuStart()
        {
            SaveManagement.SetGameState(7);
            SceneManager.LoadScene(0);
            //_profilePlayer.CurrentState.Value = GameState.FartherMenu;
        }
        private void DiamondСheck()
        {
            _diamondCount--;
            if (_diamondCount <= 0)
            {
                _player.AudioSource.clip = _audioEffectsManager.EndClip;
                _player.AudioSource.Play();
                _isFarther = true;
            }
        }

        private void PlayerCheck(Collider2D collision)
        {
            if (collision == _colliderPlayer)
            {
                _player.AudioSource.clip = _audioEffectsManager.GameOverClip;
                _player.AudioSource.Play();
                _isGameOver = true;
            }
        }

        protected override void OnDispose()
        {
            _diamondController.DiamondRaised -= DiamondСheck;
            foreach (var emeny in _enemys)
            {
                emeny.gameObject.GetComponent<EnemyView>().EnemyContact -= PlayerCheck;
            }
        }
    }
}