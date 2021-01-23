using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Mover : MonoBehaviour
{
    [Header("Setup")] [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private float acceleration = 3f;
    [SerializeField] private float gravity = 50f;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float jumpSpeed = 5f;

    private Vector3 _movedir;
    private CharacterController _characterController;

    private void Awake()
        => _characterController = GetComponent<CharacterController>();

    private void LateUpdate()
    {
        PlayerHorizontalMovement();
        PlayerVerticalMovement();
    }

    private void PlayerHorizontalMovement()
    {
        float moveHorizontal = 0f;
        moveHorizontal = Input.GetAxis("Horizontal") * rotationSpeed;
        transform.Rotate(0, moveHorizontal, 0);
    }

    private void PlayerVerticalMovement()
    {
        if (_characterController.isGrounded)
        {
            _movedir = transform.TransformDirection(Vector3.forward);
            float verticalInput = Input.GetAxis("Vertical");

            _movedir *= verticalInput;
            if (Input.GetKeyDown(jumpKey))
                _movedir.y += jumpSpeed;
        }

        _movedir.y -= gravity * Time.deltaTime;
        _characterController.Move(_movedir * acceleration * Time.deltaTime);
    }
}