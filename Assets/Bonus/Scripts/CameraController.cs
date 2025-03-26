using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private Transform _pointCloudVisualizer;

    public float MoveSpeedFactor = 0.05f;
    public float RotateSpeedFactor = 0.05f;

    private Vector3 _moveSpeed = Vector3.zero;

    private void Update()
    {
        _camera.transform.Translate(_moveSpeed);
    }

    private void OnMove(InputValue value)
    {
        var moveValue = value.Get<Vector2>();

        _moveSpeed = new Vector3(moveValue.x * MoveSpeedFactor, 0f, moveValue.y * MoveSpeedFactor);
    }

    private void OnLook(InputValue value)
    {
        if (CountPressed() == 1 &&
        IsOnUI(Touchscreen.current.primaryTouch.position.ReadValue()))
            return;


        var lookValue = value.Get<Vector2>();
        _pointCloudVisualizer.Rotate(new Vector3(lookValue.y * RotateSpeedFactor, -1 * lookValue.x * RotateSpeedFactor, 0f), Space.World);
    }

    private int CountPressed()
    {
        int count = 0;
        foreach (var touch in Touchscreen.current.touches)
        {
            if (touch.press.isPressed)
            {
                count++;
            }
        }

        return count;
    }

    private bool IsOnUI(Vector2 screenPosition)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = screenPosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        return results.Count > 0;
    }
}
