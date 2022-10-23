using System.Collections.Generic;
using UnityEngine;

namespace Profile
{
    [CreateAssetMenu(fileName = nameof(LevelManager), menuName = "Configs/" + nameof(LevelManager))]
    public class LevelManager : ScriptableObject
    {
        [SerializeField] private List<GameObject> _levels;

        public List<GameObject> Levels => _levels;
    }
}
