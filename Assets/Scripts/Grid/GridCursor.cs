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
        [SerializeField] private float cursorSize;
        [SerializeField] private Sprite blueCursorIndicator;
        [SerializeField] private Sprite redCursorIndicator;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private PlayerInputControls playerInputControls;
        
        [Header("Events")]
        [SerializeField] private EventFloatFloat onPlaceUnit;
        [SerializeField] private EventTrigger onDestroyUnit;
        
        private bool _canPlace;

        private static Camera _camera;

        private void Awake()
        {
            _camera = mainCamera;
            cursorIndicator.transform.localScale = new Vector3(cursorSize, cursorSize, cursorSize);
            playerInputControls.PlayerInput.Player.PlaceUnit.Enable();
            
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
            if (!_canPlace)
            {
                if (cursorIndicator.gameObject.activeSelf)
                {
                    cursorIndicator.gameObject.SetActive(false);
                }
                return;
            }

            if (MouseIsOutsideOfGrid(mouseScreenPos))
            {
                cursorIndicator.gameObject.SetActive(false);
                return;
            }

            TurnOnGridCursor();
            ChangeGridCursorColor(mouseScreenPos);
            
            UpdateCursorPositionOnGrid(mouseScreenPos);

            if (!playerInputControls.PlayerInput.Player.PlaceUnit.WasReleasedThisFrame()) return;
            if (GridManager.Instance.IsPositionInvalidInGrid(mouseScreenPos.x, mouseScreenPos.y))
            {
                onDestroyUnit.Raise();
                return;
            }

            PlaceUnit(mouseScreenPos);
        }

        private void PlaceUnit(Vector2 mouseScreenPos)
        {
            onPlaceUnit.Raise(mouseScreenPos.x, mouseScreenPos.y);
            CanPlaceUnit(false);
        }

        public void CanPlaceUnit(bool canPlace)
        {
            _canPlace = canPlace;
        }

        private void UpdateCursorPositionOnGrid(Vector2 mouseScreenPos)
        {
            Vector3 origin = minPoint.position; // your actual lower-left corner
            float step = cursorSize;

            float localX = mouseScreenPos.x - origin.x;
            float localY = mouseScreenPos.y - origin.y;

            int gx = Mathf.FloorToInt(localX / step);
            int gy = Mathf.FloorToInt(localY / step);

            float snapX = origin.x + (gx + 0.5f) * step;
            float snapY = origin.y + (gy + 0.5f) * step;

            cursorIndicator.transform.position = new Vector3(snapX, snapY, 0f);
        }

        private void TurnOnGridCursor()
        {
            if (!cursorIndicator.gameObject.activeSelf)
            {
                cursorIndicator.gameObject.SetActive(true);
            }
        }

        private void ChangeGridCursorColor(Vector2 mouseScreenPos)
        {
            cursorIndicator.sprite = GridManager.Instance.IsPositionInvalidInGrid(mouseScreenPos.x, mouseScreenPos.y) ? redCursorIndicator : blueCursorIndicator;
        }

        private bool UnitWasReleaseOutsideOfGrid(Vector2 mouseScreenPos)
        {
            return playerInputControls.PlayerInput.Player.PlaceUnit.WasReleasedThisFrame() && 
                   (MouseIsOutsideOfGrid(mouseScreenPos) || GridManager.Instance.IsPositionInvalidInGrid(mouseScreenPos.x, mouseScreenPos.y));
        }

        private static bool MouseIsOutsideOfGrid(Vector2 mouseScreenPos)
        {
            return GridManager.Instance.GetPositionInGrid(mouseScreenPos.x, mouseScreenPos.y) == null;
        }

        private static Vector2 GetMouseScreenPos()
        {
            Vector2 mouseScreenPos = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mouseScreenPos = new Vector3(mouseScreenPos.x, mouseScreenPos.y, 0);
            return mouseScreenPos;
        }
    }
}