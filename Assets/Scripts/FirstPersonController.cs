using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    public Vector3 Velocity { get { return _velocity; } }
    private Vector3 _velocity = Vector3.zero;

    public float movementSpeed = 5.0f;
    public float mouseSensitivity = 5.0f;
    public float jumpSpeed = 20.0f;

    float verticalRotation = 0;
    public float upDownRange = 60.0f;

    CharacterController characterController;

    public GameObject Camera;

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Rotation

        float rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, rotLeftRight, 0);


        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        Camera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);


        // Movement

        _velocity.z = Input.GetAxis("Vertical") * movementSpeed;
        _velocity.x = Input.GetAxis("Horizontal") * movementSpeed;

        _velocity.y += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded && Input.GetButton("Jump"))
        {
            _velocity.y = jumpSpeed;
        }
        else if (characterController.isGrounded)
        {
            _velocity.y = 0;
        }

        _velocity = transform.rotation * _velocity;
        
        characterController.Move(_velocity * Time.deltaTime);
    }
}
