using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class LevelMenuView : MonoBehaviour
    {
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _previousButtons;
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _setButton;

        public TMP_Text LevelText => _levelText;
        public Button BackButton => _backButton;
        public Button NextButton => _nextButton;
        public Button PrevButton => _previousButtons;
        public Button SetButton => _setButton;
    }
}
