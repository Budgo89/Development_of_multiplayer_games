using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class GameOverMenuView : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _mainMenuButton;

        public Button RestartButton => _restartButton;
        public Button MainMenuButton => _mainMenuButton;
    }
}
