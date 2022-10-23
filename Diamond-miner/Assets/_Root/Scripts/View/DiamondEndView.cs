using UnityEngine;

namespace View
{
    internal class DiamondEndView : MonoBehaviour
    {
        [SerializeField] AudioSource _audioSource;
        [SerializeField] GameObject _gameObject;

        public AudioSource AudioSource => _audioSource;
        public GameObject GameObject => _gameObject;
    }
}
