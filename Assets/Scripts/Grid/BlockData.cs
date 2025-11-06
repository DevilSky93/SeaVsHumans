using Cards.Enum;
using UnityEngine;

namespace Grid
{
    public class BlockData
    {
        public bool IsOccupied { get; set; }
        public TileType TileType { get; set; }
        public Vector2 GridPosition { get; set; }
    }
}