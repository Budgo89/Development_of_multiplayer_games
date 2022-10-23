using UnityEngine;
using UnityEngine.UI;

namespace View
{
    internal class LanguageMenuView : MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _russianButton;
        [SerializeField] private Button _englishButton;

        public Button BackButton => _backButton;
        public Button RussianButton => _russianButton;
        public Button EnglishButton => _englishButton;
    }
}
