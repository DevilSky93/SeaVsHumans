using Events.FloatFloat;
using Events.Trigger;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Grid
{
    public class GridCursor : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer cursorIndicator;
        [SerializeField] private Transform minPoint;
        [SerializeField] private Sprite blueCursorIndicator;
        [SerializeField] private Sprite redCursorIndicator;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private PlayerInputControls playerInputControls;
        [SerializeField] private GridCursorCoordinator cursorCoordinator;
        
        [Header("Events")]
        [SerializeField] private EventFloatFloat onPlaceUnit;
        [SerializeField] private EventTrigger onDestroyUnit;

        private float _cursorSize;
        private bool canPlace;

        private Vector2 _offset = Vector2.zero;

        private static Camera _camera;
        public bool IsValidToPlace { get; private set; }

        private void Awake()
        {
            _camera = mainCamera;
            playerInputControls.PlayerInput.Player.PlaceUnit.Enable();
        }

        private void Start()
        {
            _cursorSize = GridManager.Instance.CellSize;
            cursorIndicator.transform.localScale = new Vector3(_cursorSize, _cursorSize, _cursorSize);
        }

        private void Update()
        {
            Vector2 mouseScreenPos = GetMouseScreenPos();
            if (UnitWasReleaseOutsideOfGrid(mouseScreenPos))
            {
                onDestroyUnit.Raise();
                CanPlaceUnit(false);
                return;
            }
            if (!canPlace)
            {
                if (cursorIndicator.gameObject.activeSelf)
                {
                    cursorIndicator.enabled = false;
                }
                return;
            }

            if (MouseIsOutsideOfGrid(mouseScreenPos))
            {
                cursorIndicator.enabled = false;
                return;
            }

            TurnOnGridCursor();
            ChangeGridCursorColor(mouseScreenPos);
            
            UpdateCursorPositionOnGrid(mouseScreenPos);

            if (!playerInputControls.PlayerInput.Player.PlaceUnit.WasReleasedThisFrame()) return;
            if (GridManager.Instance.IsPositionInvalidInGrid(mouseScreenPos.x + _offset.x, mouseScreenPos.y + _offset.y))
            {
                onDestroyUnit.Raise();
                CanPlaceUnit(false);
                return;
            }

            if (cursorCoordinator.CanPlaceUnit())
            {
                onPlaceUnit.Raise(mouseScreenPos.x, mouseScreenPos.y);
                CanPlaceUnit(false);
            }
            else
            {
                CanPlaceUnit(false);
            }
        }

        public void CanPlaceUnit(bool value)
        {
            canPlace = value;
        }

        public void SetOffset(float x, float y)
        {
            _offset = new Vector2(x, y);
        }

        private void UpdateCursorPositionOnGrid(Vector2 mouseScreenPos)
        {
            Vector3 origin = minPoint.position; // your actual lower-left corner
            float step = _cursorSize;

            float localX = mouseScreenPos.x - origin.x;
            float localY = mouseScreenPos.y - origin.y;

            int gx = Mathf.FloorToInt(localX / step);
            int gy = Mathf.FloorToInt(localY / step);

            float snapX = origin.x + (gx + 0.5f) * step;
            float snapY = origin.y + (gy + 0.5f) * step;

            cursorIndicator.transform.position = new Vector3(snapX + _offset.x, snapY + _offset.y, 0f);
        }

        private void TurnOnGridCursor()
        {
            if (!cursorIndicator.enabled)
            {
                cursorIndicator.enabled = true;
            }
        }

        private void ChangeGridCursorColor(Vector2 mouseScreenPos)
        {
            if (GridManager.Instance.IsPositionInvalidInGrid(mouseScreenPos.x + _offset.x, mouseScreenPos.y + _offset.y))
            {
                cursorIndicator.sprite = redCursorIndicator;
                IsValidToPlace = false;
            }
            else
            {
                cursorIndicator.sprite = blueCursorIndicator;
                IsValidToPlace = true;
            }
        }

        private bool UnitWasReleaseOutsideOfGrid(Vector2 mouseScreenPos)
        {
            return playerInputControls.PlayerInput.Player.PlaceUnit.WasReleasedThisFrame() && 
                   (MouseIsOutsideOfGrid(mouseScreenPos) || GridManager.Instance.IsPositionInvalidInGrid(mouseScreenPos.x, mouseScreenPos.y));
        }

        private bool MouseIsOutsideOfGrid(Vector2 mouseScreenPos)
        {
            return GridManager.Instance.GetPositionInGrid(mouseScreenPos.x + _offset.x, mouseScreenPos.y + _offset.y) == null;
        }

        private static Vector2 GetMouseScreenPos()
        {
            Vector2 mouseScreenPos = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mouseScreenPos = new Vector3(mouseScreenPos.x, mouseScreenPos.y, 0);
            return mouseScreenPos;
        }
    }
}