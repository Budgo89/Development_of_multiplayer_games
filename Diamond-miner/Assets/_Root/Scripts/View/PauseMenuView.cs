using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class PauseMenuView : MonoBehaviour
    {
        [SerializeField] private Button _toTheGameButton;
        [SerializeField] private Button _repeatButton;
        [SerializeField] private Button _mainMenuButton;

        public Button ToTheGameButton => _toTheGameButton;
        public Button RepeatButton => _repeatButton;
        public Button MainMenuButton => _mainMenuButton;
    }
}
