using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class CameraController : MonoBehaviour
{

    [SerializeField]
    private Transform _pointCloudVisualizer;

    public float RotateSpeed = 0.05f;

    private PlayerInput _playerInput;

    private InputAction _look;

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();

        _playerInput.SwitchCurrentActionMap("Player");
        var map = _playerInput.currentActionMap;
        _look = map.FindAction("Look");
    }

    void Update()
    {
        var lookValue = _look.ReadValue<Vector2>();
        _pointCloudVisualizer.Rotate(new Vector3(-1f * lookValue.y * RotateSpeed, lookValue.x * RotateSpeed, 0f));
    }
}
