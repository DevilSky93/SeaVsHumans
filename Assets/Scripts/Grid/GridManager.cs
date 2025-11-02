using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Grid
{
    public class GridManager : MonoBehaviour
    {
        public static GridManager Instance { get; private set; }

        [SerializeField] private Transform minPoint, maxPoint;
        [SerializeField] private LayerMask gridAllowedBlockMask;
        [SerializeField] private GameObject boardTileSprite;

#if UNITY_EDITOR
        [SerializeField] private bool showGizmo;
#endif

        private (int width, int height) _gridSize;
        private readonly Dictionary<Vector2, BlockData> _tiles = new();
        private Vector2 _startPoint;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            GenerateGrid();
        }

        private void GenerateGrid()
        {
            minPoint.position = new Vector3(Mathf.Round(minPoint.position.x), Mathf.Round(minPoint.position.y), 0);
            maxPoint.position = new Vector3(Mathf.Round(maxPoint.position.x), Mathf.Round(maxPoint.position.y), 0);

            _startPoint = minPoint.position + new Vector3(.5f, .5f);

            _gridSize = (Mathf.RoundToInt(maxPoint.position.x - minPoint.position.x),
                Mathf.RoundToInt(maxPoint.position.y - minPoint.position.y));

            for (int x = 0; x < _gridSize.width; x++)
            {
                for (int y = 0; y < _gridSize.height; y++)
                {
                    Vector2 cellPosition = _startPoint + new Vector2(x, y);

                    BlockData tile = new()
                    {
                        GridPosition = cellPosition
                    };
                    if (Physics2D.OverlapBox(cellPosition, new Vector2(.9f, .9f), 0f, gridAllowedBlockMask))
                    {
                        tile.IsOccupied = true;
                    }

                    _tiles.Add(cellPosition, tile);
                    Instantiate(boardTileSprite, cellPosition, Quaternion.identity, transform);
                }
            }
        }

        [CanBeNull]
        public Vector2? GetPositionInGrid(float x, float y)
        {
            // 1) Monde -> Local
            float localX = x - minPoint.position.x;
            float localY = y - minPoint.position.y;

            // Si ta taille de case = 1, garde tel quel ; sinon divise par cellSize
            int gx = Mathf.FloorToInt(localX /* / cellSize */);
            int gy = Mathf.FloorToInt(localY /* / cellSize */);

            // 2) Bornes complètes (basses ET hautes)
            if (gx < 0 || gy < 0 || gx >= _gridSize.width || gy >= _gridSize.height)
                return null;

            // 3) Snap sur le centre de la cellule
            // ajoute +0.5f pour viser le centre ; enlève-le pour le coin inférieur-gauche
            float snapX = minPoint.position.x + (gx + 0.5f) /* * cellSize */;
            float snapY = minPoint.position.y + (gy + 0.5f) /* * cellSize */;

            return new Vector2(snapX, snapY);
        }

        [CanBeNull]
        public T GetObjectInGridFromDirection<T>(float x, float y, Vector2 direction) where T : class
        {
            Vector2? position = GetPositionInGrid(x + direction.x, y + direction.y);
            if (position == null) return null;
            Collider2D overlappingElement = Physics2D.OverlapBox(position.Value, new Vector3(.9f, .9f, .9f), 0f,
                gridAllowedBlockMask);
            return overlappingElement?.GetComponent<T>();
        }
        
        [CanBeNull]
        public T GetObjectInGrid<T>(float x, float y) where T : class
        {
            Vector2? position = GetPositionInGrid(x, y);
            if (position == null) return null;
            Collider2D overlappingElement = Physics2D.OverlapBox(position.Value, new Vector3(.9f, .9f, .9f), 0f,
                gridAllowedBlockMask);
            return overlappingElement?.GetComponent<T>();
        }

        /// <summary>
        /// Vérifie si une position choisie dans la grille est adjacente à la position du joueur.
        /// Deux positions sont considérées adjacentes si elles sont directement voisines
        /// horizontalement, verticalement ou diagonalement.
        /// </summary>
        /// <param name="playerPosition">Position actuelle du joueur dans le monde.</param>
        /// <param name="chosenPosition">Position choisie dans le monde.</param>
        /// <returns>Retourne true si les positions sont adjacentes, sinon false.</returns>
        public bool IsAdjacentOfPlayerInGrid(Vector2 playerPosition, Vector2 chosenPosition)
        {
            // Récupère les positions du joueur et de la position choisie dans la grille
            Vector2? playerGridPosition = GetPositionInGrid(playerPosition.x, playerPosition.y);
            Vector2? chosenGridPosition = GetPositionInGrid(chosenPosition.x, chosenPosition.y);

            // Vérifie que les deux positions sont valides dans la grille
            if (!playerGridPosition.HasValue || !chosenGridPosition.HasValue)
            {
                return false;
            }

            // Calcule la différence entre les positions sur les axes X et Y
            float deltaX = Mathf.Abs(playerGridPosition.Value.x - chosenGridPosition.Value.x);
            float deltaY = Mathf.Abs(playerGridPosition.Value.y - chosenGridPosition.Value.y);

            // Vérifie si les positions sont adjacentes horizontalement, verticalement ou diagonalement
            return (Mathf.Approximately(deltaX, 1) && Mathf.Approximately(deltaY, 1)) || // Diagonale
                   (Mathf.Approximately(deltaX, 1) && Mathf.Approximately(deltaY, 0)) || // Horizontal
                   (Mathf.Approximately(deltaX, 0) && Mathf.Approximately(deltaY, 1));   // Vertical
        }

        private void OnDrawGizmos()
        {
            if (!showGizmo) return;
            Gizmos.color = Color.red;
            for (int x = 0; x < _gridSize.width; x++)
            {
                for (int y = 0; y < _gridSize.height; y++)
                {
                    Gizmos.DrawWireCube(_startPoint + new Vector2(x, y), Vector3.one);
                }
            }
        }
    }
}