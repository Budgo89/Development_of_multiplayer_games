using MB;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

namespace Controllers
{
    internal class TilemapController : BaseController
    {
        private Player _player;
        private Tilemap _tileMap;
        private NavMeshSurface2d _navMeshSurface;

        public TilemapController(Player player, Tilemap tileMap, NavMeshSurface2d navMeshSurface)
        {
            _player = player;
            _tileMap = tileMap;
            _navMeshSurface = navMeshSurface;
        }

        public bool IsEdgeMap(int sign, Vector2 vector2)
        {
            var rays = Physics2D.RaycastAll(_player.transform.position, sign * vector2, 1);
            foreach (var ray in rays)
            {
                if (ray.rigidbody == null)
                {
                    return true;
                }
                var edge = ray.rigidbody.gameObject.GetComponent<Edge>();
                if (edge != null)
                    return false;
            }

            return true;
        }

        public void RemoveSoil(int sign, Vector2 vector2)
        {
            var ray = Physics2D.Raycast(_player.transform.position, sign * vector2, 0);
            if (ray.rigidbody == null)
                return;
            var priming = ray.rigidbody.gameObject.GetComponent<Priming>();
            if (priming == null)
                return;
            Vector3 world = new Vector3(ray.point.x, ray.point.y, -1);
            Vector3Int gridPos = _tileMap.WorldToCell(world);
            _tileMap.SetTile(new Vector3Int(gridPos.x, gridPos.y, 0), null);
        }
    }
}