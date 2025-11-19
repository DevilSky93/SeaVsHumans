using System.Collections.Generic;
using System.Linq;
using Events.FloatFloat;
using UnityEngine;

namespace Grid
{
    public class GridCursorCoordinator : MonoBehaviour
    {
        [SerializeField] private List<GridCursor> cursors;

        public void IsTwoByTwo(bool isTwoByTwo)
        {
            if (isTwoByTwo)
            {
                for (int i = 0; i < cursors.Count / 2; i++)
                {
                    for (int j = i; j < i + cursors.Count / 2; j++)
                    {
                        GridCursor cursor = cursors[i + j];
                        cursor.gameObject.SetActive(true);
                        cursor.CanPlaceUnit(true);
                        cursor.SetOffset(i * GridManager.Instance.CellSize, -(j - i) * GridManager.Instance.CellSize);
                    }
                }
            }
            else
            {
                cursors[0].gameObject.SetActive(true);
                cursors[0].CanPlaceUnit(true);
            }
        }

        public bool CanPlaceUnit()
        {
            return cursors.All(c => c.IsValidToPlace);
        }
    }
}