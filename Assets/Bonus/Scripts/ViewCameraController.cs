using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class ViewCameraController : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    private PlayerInput _playerInput;

    private InputAction _move;
    private InputAction _look;

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();

        _playerInput.SwitchCurrentActionMap("Player");
        var map = _playerInput.currentActionMap;
        _move = map.FindAction("Move");
        _look = map.FindAction("Look");
    }

    void Update()
    {
        var moveValue = _move.ReadValue<Vector2>();
        _camera.transform.Translate(moveValue.x * 0.5f, 0f, moveValue.y * 0.5f);

        var lookValue = _look.ReadValue<Vector2>();
        _camera.transform.Rotate(new Vector3(-1f * lookValue.y, lookValue.x, 0f));
    }
}
