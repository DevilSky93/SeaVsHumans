using Grid;
using UnityEngine;

namespace Helpers
{
    public static class AngleHelper
    {
        public static Direction GetDirection(this float angle)
        {
            // Ajuster l'angle pour qu'il soit compris entre 0 et 360
            if (angle < 0)
            {
                angle += 360;
            }

            // Convertir l'angle en une des 8 directions
            int directionIndex = Mathf.RoundToInt(angle / 45f) % 8;

            return directionIndex switch
            {
                0 => Direction.Right,
                1 => Direction.UpRight,
                2 => Direction.Up,
                3 => Direction.UpLeft,
                4 => Direction.Left,
                5 => Direction.DownLeft,
                6 => Direction.Down,
                7 => Direction.DownRight,
                _ => Direction.None
            };
        }

        public static float RadianToDegree(Vector2 direction)
        {
            return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }

        public static Vector2 ConvertDirectionToVector2(Direction direction)
        {
            return direction switch
            {
                Direction.Right => Vector2.right,
                Direction.UpRight => new Vector2(1, 1).normalized,
                Direction.Up => Vector2.up,
                Direction.UpLeft => new Vector2(-1, 1).normalized,
                Direction.Left => Vector2.left,
                Direction.DownLeft => new Vector2(-1, -1).normalized,
                Direction.Down => Vector2.down,
                Direction.DownRight => new Vector2(1, -1).normalized,
                _ => Vector2.zero
            };
        }
    }
}