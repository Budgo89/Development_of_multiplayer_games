using MB;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

namespace Controllers
{
    internal class PlayerController : BaseController
    {
        private Player _player;
        private Tilemap _tileMap;
        private DiamondController _diamondController;
        private NavMeshSurface2d _navMeshSurface;
        private SwipeDetection _swipeDetection;

        private float _xAxisInput;
        
        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);

        private TilemapController _tilemapController;
        private StoneController _stoneController;

        public PlayerController(Player player, Tilemap tileMap, DiamondController diamondController, NavMeshSurface2d navMeshSurface, SwipeDetection swipeDetection)
        {
            _player = player;
            _tileMap = tileMap;
            _diamondController = diamondController;
            _navMeshSurface = navMeshSurface;
            _swipeDetection = swipeDetection;

            _swipeDetection.SwipeEvevt += OnSwipe;

            _tilemapController = new TilemapController(_player, tileMap, _navMeshSurface);
            _stoneController = new StoneController(_player, _tilemapController, _navMeshSurface);
        }

        private void OnSwipe(Vector2 direction)
        {
            if (direction == Vector2.up)
                Move(0, 1);
            if (direction == Vector2.down)
                Move(0, -1);
            if (direction == Vector2.left)
                Move(-1, 0);
            if(direction == Vector2.right)
                Move(1, 0);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Move(-1,0);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                Move(1,0);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                Move(0,1);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                Move(0,-1);
            }
        }

        private void Move(int x, int y)
        {
            if (x != 0)
            {
                var flaEdgeMap = _tilemapController.IsEdgeMap(x, Vector2.right);
                if (!flaEdgeMap)
                    return;
                if (_stoneController.IsObstacle(Vector2.right, x, y))
                    return;
                _xAxisInput = Input.GetAxis("Horizontal");
                _player.transform.position = new Vector3(_player.transform.position.x + x,
                    _player.transform.position.y, _player.transform.position.z);
                _player.transform.localScale = (_xAxisInput < 0 ? _leftScale : _rightScale);
                _tilemapController.RemoveSoil(x, Vector2.right);
                _diamondController.RaiseDiamond(x, Vector2.right);
                _navMeshSurface.BuildNavMesh();
            }

            if (y != 0)
            {
                if (_stoneController.IsObstacle(Vector2.up, x, y))
                    return;
                var flagEdgeMap = _tilemapController.IsEdgeMap(y, Vector2.up);
                if (!flagEdgeMap)
                    return;
                _player.transform.position = new Vector3(_player.transform.position.x,
                    _player.transform.position.y + y, _player.transform.position.z);
                _tilemapController.RemoveSoil(y, Vector2.up);
                _diamondController.RaiseDiamond(y, Vector2.up);
                _navMeshSurface.BuildNavMesh();
            }

        }

        protected override void OnDispose()
        {
            _tilemapController?.Dispose();
            _stoneController?.Dispose();
        }
    }

}
