using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    internal class EnemyController : BaseController
    {
        private List<GameObject> _emenys;
        private float _speed = 5f;
        private Vector2 _newpoint;
        private System.Random _random;

        public EnemyController(List<GameObject> emenys)
        {
            _emenys = emenys;
            _random = new System.Random();
            _newpoint = new Vector2(_random.Next(-8,7), _random.Next(-4, 4));
        }

        public void FixedUpdate()
        {
            //foreach (var emeny in _emenys)
            //{
            //    emeny
            //}
        }
        

    }
}