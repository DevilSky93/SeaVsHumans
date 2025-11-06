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
            cursorIndicator.transform.position = new Vector3(Mathf.FloorToInt(mouseScreenPos.x) + .5f,
                Mathf.FloorToInt(mouseScreenPos.y) + .5f,
                0f);

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