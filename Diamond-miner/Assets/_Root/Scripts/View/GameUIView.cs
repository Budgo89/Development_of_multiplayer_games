using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class GameUIView : MonoBehaviour
    {
        [SerializeField] private Button _pauseMenuButton;
        [SerializeField] private TMP_Text _diamondСounts;
        [SerializeField] private TMP_Text _gameTime;

        public Button PauseMenuButton => _pauseMenuButton;
        public TMP_Text GameTime => _gameTime;
        public TMP_Text DiamondСounts => _diamondСounts;
    }
}
