using UnityEngine;

namespace Profile
{
    [CreateAssetMenu(fileName = nameof(AudioEffectsManager), menuName = "Configs/" + nameof(AudioEffectsManager))]
    public class AudioEffectsManager : ScriptableObject
    {
        [SerializeField] private AudioClip _gameOverClip;
        [SerializeField] private AudioClip _endClip;

        public AudioClip GameOverClip => _gameOverClip;
        public AudioClip EndClip => _endClip;
    }
}
