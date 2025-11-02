using UnityEngine;

namespace Helpers
{
    public static class Vector2Helper
    {
        public static Vector2 GetDirectionFromAToB(Vector2 origin, Vector2 target)
        {
            return target - origin;
        }
    }
}