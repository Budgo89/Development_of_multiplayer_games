using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace View
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private int diamondCount;
        [SerializeField] private NavMeshSurface2d _navMeshSurface2d;
        [SerializeField] private List<GameObject> _diamonds;
        [SerializeField] private List<GameObject> _enemys;

        public int DiamondCount => diamondCount;
        public NavMeshSurface2d NavMeshSurface => _navMeshSurface2d;
        public List<GameObject> Diamonds => _diamonds;
        public List<GameObject> Enemys => _enemys;
    }
}
