using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MB
{
    public class TileMapScanner : MonoBehaviour
    {
        public Tilemap GetTileMap()
        {
            return (FindObjectsOfType(typeof(Priming)) as Priming[]).FirstOrDefault().gameObject.GetComponent<Tilemap>();
        }
    }
}
