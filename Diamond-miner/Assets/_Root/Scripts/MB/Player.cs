using UnityEngine;

namespace MB
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        public AudioSource AudioSource => _audioSource;
    }
}
