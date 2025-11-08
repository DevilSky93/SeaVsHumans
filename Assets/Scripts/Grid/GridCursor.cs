using Events.FloatFloat;
using Events.Trigger;
using Helpers;
using JetBrains.Annotations;
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
        [SerializeField] private EventFloatFloat onPlaceUnit;
        [SerializeField] private EventTrigger onDestroyUnit;
        private bool _canPlace;

        private static Camera _camera;

        private void Awake()
        {
            _camera = mainCamera;
            cursorIndicator.transform.localScale = new Vector3(cursorSize, cursorSize, cursorSize);
        }

        private void Update()
        {
            Vector2 mouseScreenPos = GetMouseScreenPos();
            if (UnitWasReleaseOutsideOfGrid(mouseScreenPos))
            {
                onDestroyUnit.Raise();
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
            // cursorIndicator.transform.position = new Vector3(Mathf.FloorToInt(mouseScreenPos.x) + cursorSize,
            //     Mathf.FloorToInt(mouseScreenPos.y) + cursorSize,
            //     0f);
            // float step = cursorSize;
            // float x = Mathf.Floor(mouseScreenPos.x / step) * step + step * 0.5f;
            // float y = Mathf.Floor(mouseScreenPos.y / step) * step + step * 0.5f;
            //
            // cursorIndicator.transform.position = new Vector3(x, y, 0f);
            
            Vector3 origin = minPoint.position; // ton coin bas-gauche réel
            float step = cursorSize;

// position souris en monde (déjà convertie depuis ScreenToWorldPoint)
            float localX = mouseScreenPos.x - origin.x;
            float localY = mouseScreenPos.y - origin.y;

// indices de cellule
            int gx = Mathf.FloorToInt(localX / step);
            int gy = Mathf.FloorToInt(localY / step);

// position centrée de la cellule
            float snapX = origin.x + (gx + 0.5f) * step;
            float snapY = origin.y + (gy + 0.5f) * step;

            cursorIndicator.transform.position = new Vector3(snapX, snapY, 0f);
            

            if (!Mouse.current.leftButton.wasReleasedThisFrame) return;
            if (GridManager.Instance.IsPositionInvalidInGrid(mouseScreenPos.x, mouseScreenPos.y))
            {
                onDestroyUnit.Raise();
                return;
            }
            GridManager.Instance.SetPositionOccupiedInGrid(mouseScreenPos.x, mouseScreenPos.y, true);
            onPlaceUnit.Raise(mouseScreenPos.x, mouseScreenPos.y);
        }

        [UsedImplicitly]
        public void CanPlaceUnit(bool canPlace)
        {
            _canPlace = canPlace;
            if (canPlace)
            {
                playerInputControls.PlayerInput.Player.PlaceUnit.Enable();
            }
            else
            {
                playerInputControls.PlayerInput.Player.PlaceUnit.Disable();
            }
        }

        public static Direction GetMouseDirection(Vector2 mousePosition, Vector2 recordedMousePosition)
        {
            Vector2 worldMousePosition = _camera.ScreenToWorldPoint(mousePosition);
            Vector2 direction = Vector2Helper.GetDirectionFromAToB(recordedMousePosition, worldMousePosition);

            float angle = AngleHelper.RadianToDegree(direction);
            Direction mouseDirection = angle.GetDirection();
            return mouseDirection;
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

        private static bool UnitWasReleaseOutsideOfGrid(Vector2 mouseScreenPos)
        {
            return Mouse.current.leftButton.wasReleasedThisFrame && 
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