using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class FlyCamera : MonoBehaviour
{

    [Header("Controls Settings")]
    public float cameraSensitivity = 50;
    public float verticalSpeed = 4;
    public float normalMoveSpeed = 10;
    public float slowMoveSpeed = 5;
    public float fastMoveSpeed = 15;

    [Header("Misc Settings")]
    public LayerMask groundMask;

    private GameManager gameManager;
    private InputMaster controls;
    private Vector2 moveDir;
    private Vector2 rotation;
    private float movementSpeed;
    private float speedMultiplier;
    private float upMultiplier;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        controls = new InputMaster();
        
        controls.Camera.Escape.performed += ctx => ToggleEscape();

        controls.Camera.MousePosition.performed += ctx => CalculateRotation(ctx.ReadValue<Vector2>());

        controls.Camera.Speed.performed += ctx => speedMultiplier = ctx.ReadValue<float>();
        controls.Camera.Speed.canceled += ctx => speedMultiplier = 0;

        controls.Camera.Movement.performed += ctx => moveDir = ctx.ReadValue<Vector2>();
        controls.Camera.Movement.canceled += ctx => moveDir = Vector2.zero;

        controls.Camera.Height.performed += ctx => upMultiplier = ctx.ReadValue<float>();
        controls.Camera.Height.canceled += ctx => upMultiplier = 0;

        controls.Camera.LeftClick.performed += ctx => SetMarkerPosition(gameManager.start);
        controls.Camera.RightClick.performed += ctx => SetMarkerPosition(gameManager.end);

        ToggleEscape();
    }

    private void OnEnable() => controls.Enable();

    private void OnDisable() => controls.Disable();

    private void ToggleEscape()
    {
        Cursor.lockState = (Cursor.lockState == CursorLockMode.None) ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = (Cursor.lockState == CursorLockMode.None);
    }

    private void CalculateRotation(Vector2 dir)
    {
        if (Cursor.lockState == CursorLockMode.None)
            return;

        rotation += dir * cameraSensitivity * Time.deltaTime;
        rotation.y = Mathf.Clamp(rotation.y, -90, 90);
        transform.localRotation = Quaternion.AngleAxis(rotation.x, Vector3.up);
        transform.localRotation *= Quaternion.AngleAxis(rotation.y, Vector3.left);
    }

    private void SetSpeed(float speed)
    {
        if (speed > 0)
            movementSpeed = fastMoveSpeed;
        else if (speed < 0)
            movementSpeed = slowMoveSpeed;
        else
            movementSpeed = normalMoveSpeed;
    }

    private void Update()
    {
        SetSpeed(speedMultiplier);
        transform.position += (transform.forward * moveDir.y + transform.right * moveDir.x) * movementSpeed * Time.deltaTime;
        transform.position += transform.up * verticalSpeed * upMultiplier * Time.deltaTime;
    }

    private void SetMarkerPosition(Transform marker)
    {
        RaycastHit hit;
        if (Physics.Raycast(new Ray(transform.position, transform.forward), out hit, Mathf.Infinity, groundMask))
            marker.transform.position = hit.point;
    }

}