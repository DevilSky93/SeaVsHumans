using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    public class Vector2ComparerWithTolerance : IEqualityComparer<Vector2>
    {
        private readonly float _tolerance;

        public Vector2ComparerWithTolerance(float tolerance = 0.001f)
        {
            this._tolerance = tolerance;
        }

        public bool Equals(Vector2 a, Vector2 b)
        {
            return Mathf.Abs(a.x - b.x) < _tolerance &&
                   Mathf.Abs(a.y - b.y) < _tolerance;
        }

        public int GetHashCode(Vector2 v)
        {
            // On "quantize" les valeurs pour générer un hash cohérent avec la tolérance
            int hx = Mathf.RoundToInt(v.x / _tolerance);
            int hy = Mathf.RoundToInt(v.y / _tolerance);
            return hx * 73856093 ^ hy * 19349663; // mix simple de hash
        }
    }
}