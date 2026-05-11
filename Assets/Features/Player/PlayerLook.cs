using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLookInputSystem : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 0.12f;
    [SerializeField] private Transform cameraTransform;

    private Vector2 lookInput;
    private float yaw;
    private float pitch;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (cameraTransform != null)
        {
            Vector3 angles = cameraTransform.localEulerAngles;
            yaw = angles.y;
            pitch = angles.x;
        }
    }

    private void Update()
    {
        if (cameraTransform == null)
            return;

        yaw += lookInput.x * mouseSensitivity;

        pitch -= lookInput.y * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }
}