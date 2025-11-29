using System.Collections.Generic;
using System.Linq;
using Cards.Enum;
using JetBrains.Annotations;
using UnityEngine;
using Random = System.Random;

namespace Grid
{
    public class GridManager : MonoBehaviour
    {
        private const float Eps = 1e-6f;
        public static GridManager Instance { get; private set; }

        [SerializeField] private Transform minPoint, maxPoint;
        [SerializeField] private LayerMask gridAllowedBlockMask;
        [SerializeField] private GameObject boardTileSprite;
        [SerializeField] private float cellSize;

        public float CellSize => cellSize;

#if UNITY_EDITOR
        [SerializeField] private bool showGizmo;
#endif

        private (int width, int height) _gridSize;
        private readonly Dictionary<Vector2, BlockData> _tiles = new(new Vector2ComparerWithTolerance());
        private Vector2 _startPoint;

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            GenerateGrid();
        }

        private void GenerateGrid()
        {
            minPoint.position = new Vector3(Mathf.Round(minPoint.position.x), Mathf.Round(minPoint.position.y), 0);
            maxPoint.position = new Vector3(Mathf.Round(maxPoint.position.x), Mathf.Round(maxPoint.position.y), 0);

            _startPoint = minPoint.position + new Vector3(.5f, .5f) * cellSize;

            _gridSize = (Mathf.RoundToInt(maxPoint.position.x - minPoint.position.x),
                Mathf.RoundToInt(maxPoint.position.y - minPoint.position.y));

            for (int x = 0; x < _gridSize.width; x++)
            {
                for (int y = 0; y < _gridSize.height; y++)
                {
                    Vector2 cellPosition = _startPoint + new Vector2(x * cellSize, y * cellSize);

                    BlockData tile = new()
                    {
                        GridPosition = cellPosition
                    };
                    if (Physics2D.OverlapBox(cellPosition, new Vector2(cellSize, cellSize), 0f, gridAllowedBlockMask))
                    {
                        tile.IsOccupied = true;
                    }

                    tile.TileType = x < _gridSize.width / 2 ? TileType.Player : TileType.Enemy;

                    _tiles.Add(cellPosition, tile);
                    GameObject tileGo = Instantiate(boardTileSprite, cellPosition, Quaternion.identity, transform);
                    tileGo.transform.localScale = new Vector3(cellSize, cellSize, cellSize);
                }
            }
        }

        [CanBeNull]
        public Vector2? GetPositionInGrid(float x, float y)
        {
            Vector2 origin = minPoint ? minPoint.position : Vector2.zero;

            float localX = x - origin.x;
            float localY = y - origin.y;

            // Rejette clairement hors grille (à gauche / en bas / au-delà de la dernière cellule)
            float maxX = _gridSize.width * cellSize;
            float maxY = _gridSize.height * cellSize;
            if (localX < 0f || localY < 0f || localX >= maxX || localY >= maxY)
                return null;

            // Décale d’un epsilon pour éviter le cas exact sur la frontière droite/haute
            int gx = Mathf.FloorToInt(Mathf.Min(localX, maxX - Eps) / cellSize);
            int gy = Mathf.FloorToInt(Mathf.Min(localY, maxY - Eps) / cellSize);

            // Snap au centre
            float snapX = origin.x + (gx + 0.5f) * cellSize;
            float snapY = origin.y + (gy + 0.5f) * cellSize;
            return new Vector2(snapX, snapY);
        }

        [CanBeNull]
        public T GetObjectInGridFromDirection<T>(float x, float y, Vector2 direction) where T : class
        {
            Vector2? position = GetPositionInGrid(x + direction.x, y + direction.y);
            if (position == null) return null;
            Collider2D overlappingElement = Physics2D.OverlapBox(position.Value, new Vector3(cellSize, cellSize, cellSize), 0f,
                gridAllowedBlockMask);
            return overlappingElement?.GetComponent<T>();
        }
        
        [CanBeNull]
        public T GetObjectInGrid<T>(float x, float y) where T : class
        {
            Vector2? position = GetPositionInGrid(x, y);
            if (position == null) return null;
            Collider2D overlappingElement = Physics2D.OverlapBox(position.Value, new Vector3(cellSize, cellSize, cellSize), 0f,
                gridAllowedBlockMask);
            return overlappingElement?.GetComponent<T>();
        }
        
        public bool IsPositionInvalidInGrid(float x, float y)
        {
            Vector2? position = GetPositionInGrid(x, y);
            return position != null && (_tiles[position.Value].IsOccupied || _tiles[position.Value].TileType == TileType.Enemy);
        }

        public bool IsMouseHoveringOnGrid(float x, float y)
        {
            Vector2? position = GetPositionInGrid(x, y);
            return position != null;
        }
        
        public void SetPositionOccupiedInGrid(float x, float y, bool isOccupied)
        {
            Vector2? position = GetPositionInGrid(x, y);
            if (position != null)
            {
                _tiles[position.Value].IsOccupied = isOccupied;
            }
        }
        
        public bool IsPositionOccupiedInGrid(float x, float y)
        {
            Vector2? position = GetPositionInGrid(x, y);
            if (position != null)
            {
                return _tiles[position.Value].IsOccupied;
            }
            Debug.Log("Can't check position occupied, position is null");
            return false;
        }

        public void SetTileType(float x, float y, TileType tileType)
        {
            Vector2? position = GetPositionInGrid(x, y);
            if (position != null)
            {
                _tiles[position.Value].TileType = tileType;
            }
        }
        
        public void DisplayOccupiedTiles()
        {
            foreach (KeyValuePair<Vector2, BlockData> tile in _tiles.Where(t => t.Value.IsOccupied))
            {
                Debug.Log($"Position: {tile.Key}, Occupied: {tile.Value.IsOccupied}, Type: {tile.Value.TileType}");
            }
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

        public bool IsFieldEmpty()
        {
            bool areAllTilesEmpty = _tiles.All(t => !t.Value.IsOccupied) || _tiles.Any(t => t.Value.TileType is TileType.Field);
            return areAllTilesEmpty;
        }
        
        public Vector2 GetRandomFreeCell(TileType tileType)
        {
            Random rng = new();
            List<KeyValuePair<Vector2, BlockData>> freeTiles = _tiles.Where(t => !t.Value.IsOccupied && t.Value.TileType == tileType).ToList();

            if (freeTiles.Count == 0)
                return new Vector2Int(-1, -1); // aucune case dispo

            int index = rng.Next(freeTiles.Count);
            Vector2 gridPos = freeTiles[index].Key;

            // Vector2 origin = minPoint ? minPoint.position : Vector2.zero;
            // float localX = gridPos.x - origin.x;
            // float localY = gridPos.y - origin.y;
            // int gx = Mathf.FloorToInt(localX / cellSize);
            // int gy = Mathf.FloorToInt(localY / cellSize);

            return gridPos;
        }

        [PublicAPI]
        public void DebugBlock()
        {
            foreach (BlockData value in _tiles.Values)
            {
                Debug.Log($"Position : {value.GridPosition} - Occupied : {value.IsOccupied} - Type : {value.TileType}");
            }
        }
        
        private void OnDrawGizmos()
        {
            if (!showGizmo || _gridSize.width <= 0 || _gridSize.height <= 0) return;

            Vector3 origin = minPoint ? minPoint.position : Vector3.zero;

            Gizmos.color = Color.red;
            for (int x = 0; x < _gridSize.width; x++)
            {
                for (int y = 0; y < _gridSize.height; y++)
                {
                    Vector3 center = origin + new Vector3((x + 0.5f) * cellSize, (y + 0.5f) * cellSize, 0f);
                    Gizmos.DrawWireCube(center, Vector3.one * cellSize);
                }
            }

            // contour global pour visualiser les bornes exclues (droite/haut)
            Gizmos.color = Color.yellow;
            Vector3 size = new Vector3(_gridSize.width * cellSize, _gridSize.height * cellSize, 0f);
            Gizmos.DrawWireCube(origin + size / 2f, size);
        }
    }
}