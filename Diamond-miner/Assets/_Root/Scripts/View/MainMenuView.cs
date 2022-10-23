using UnityEngine;
using UnityEngine.UI;

namespace View
{
    internal class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _levelButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _exitButton;

        public Button StartGameButton => _startGameButton;
        public Button LevelButton => _levelButton;
        public Button SettingsButton => _settingsButton;
        public Button ExitButton => _exitButton;

    }
}
