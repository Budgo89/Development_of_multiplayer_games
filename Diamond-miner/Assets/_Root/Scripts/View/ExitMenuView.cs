using UnityEngine;
using UnityEngine.UI;

namespace View
{
    internal class ExitMenuView : MonoBehaviour
    {
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _backButton;

        public Button ExitButton => _exitButton;
        public Button BackButton => _backButton;
    }
}
