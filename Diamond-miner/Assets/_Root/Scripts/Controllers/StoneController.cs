using MB;
using UnityEngine;
using UnityEngine.AI;

namespace Controllers
{
    internal class StoneController : BaseController
    {
        private Player _player;
        private TilemapController _tilemapController;
        private Stone _stone;
        private NavMeshSurface2d _navMeshSurface;
        public StoneController(Player player, TilemapController tilemapController, NavMeshSurface2d navMeshSurface)
        {
            _player = player;
            _tilemapController = tilemapController;
            _navMeshSurface = navMeshSurface;
        }
        /// <summary>
        /// Проверка препятствия
        /// </summary>
        /// <param name="vector2">направление рейкаста</param>
        /// <param name="x">шаг по оси x</param>
        /// <param name="y">шаг по оси y</param>
        /// <returns></returns>
        public bool IsObstacle(Vector2 vector2, int x, int y)
        {
            _stone = null;
            int sign;
            sign = x == 0 ? y : x;
            var rays = Physics2D.RaycastAll(_player.gameObject.transform.position, sign * vector2, 1);
            
            foreach (var ray in rays)
            {
                var stone = ray.rigidbody.GetComponent<Stone>();
                if (stone != null)
                    _stone = stone;
                var diamond = ray.rigidbody.GetComponent<Diamond>();
                if (diamond != null)
                    return false;
            }
            
            if(_stone == null)
                return false;


            var ray2s = Physics2D.RaycastAll(_player.gameObject.transform.position, sign * vector2, 2);
            int i = 0;
            foreach (var ray2 in ray2s)
            {
                var stone2 = ray2.rigidbody.GetComponent<Stone>();
                if (stone2 != null)
                    i++;
                var priming = ray2.rigidbody.GetComponent<Priming>();
                if (priming != null)
                    i++;
                var diamond = ray2.rigidbody.GetComponent<Diamond>();
                if (diamond != null)
                    i++;
                var edge = ray2.rigidbody.GetComponent<Edge>();
                if (edge != null)
                    i++;

            }
            if(i >1)
                return true;

            _stone.gameObject.transform.position = new Vector3(_stone.gameObject.transform.position.x + x,
                _stone.gameObject.transform.position.y + y, _stone.gameObject.transform.position.z);
            return false;
        }
    }
}