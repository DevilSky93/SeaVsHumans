using Helpers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Grid
{
    public class GridCursor : MonoBehaviour
    {
        [SerializeField] private Transform cursorIndicator;
        [SerializeField] private Camera mainCamera;

        private static Camera _camera;

        private void Awake()
        {
            _camera = mainCamera;
        }

        private void Update()
        {
            Vector2 mouseScreenPos = GetMouseScreenPos();
            if (GridManager.Instance.GetPositionInGrid(mouseScreenPos.x, mouseScreenPos.y) == null)
            {
                Debug.Log("cc");
                cursorIndicator.gameObject.SetActive(false);
                return;
            }

            if (!cursorIndicator.gameObject.activeSelf)
            {
                cursorIndicator.gameObject.SetActive(true);
            }
            cursorIndicator.position = new Vector3(Mathf.FloorToInt(mouseScreenPos.x) + .5f,
                Mathf.FloorToInt(mouseScreenPos.y) + .5f,
                0f);
        }

        public static Vector2 GetMouseScreenPos()
        {
            Vector2 mouseScreenPos = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mouseScreenPos = new Vector3(mouseScreenPos.x, mouseScreenPos.y, 0);
            return mouseScreenPos;
        }

        public static Direction GetMouseDirection(Vector2 mousePosition, Vector2 recordedMousePosition)
        {
            Vector2 worldMousePosition = _camera.ScreenToWorldPoint(mousePosition);
            Vector2 direction = Vector2Helper.GetDirectionFromAToB(recordedMousePosition, worldMousePosition);

            float angle = AngleHelper.RadianToDegree(direction);
            Direction mouseDirection = angle.GetDirection();
            return mouseDirection;
        }
    }
}