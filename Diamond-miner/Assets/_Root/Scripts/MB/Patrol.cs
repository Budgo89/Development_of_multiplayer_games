using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MB
{
    internal class Patrol : MonoBehaviour
    {
        [SerializeField] private List<Transform> _points;

        private EnemyView _enemyView;
        private NavMeshAgent _navMeshAgent;
        private float _maxDestination = 0.1f;
        private int _point = 0;
        private System.Random _random;
        private Vector3 _position;

        public void Awake()
        {
            _enemyView = gameObject.GetComponent<EnemyView>();
            _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
            _navMeshAgent.updateRotation = false;
            _navMeshAgent.updateUpAxis = false;
            _random = new System.Random();
        }

        public void Update()
        {
            if (Vector2.Distance(_points[_point].position, _enemyView.gameObject.transform.position) >=
                _maxDestination && _enemyView.gameObject.transform.position != _position)
            {
                _navMeshAgent.SetDestination(_points[_point].position);
                _position = _enemyView.gameObject.transform.position;
            }
                
            
            else
            {
                if (_points.Count > 2)
                {
                    _point = _random.Next(0, _points.Count);
                }
                else if (_point < _points.Count - 1)
                {
                    _point++;
                }
                else
                {
                    _point = 0;
                }

                _position = new Vector3(0, 0, 0);
            }
        }
        
    }
}
