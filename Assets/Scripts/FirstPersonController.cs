using UnityEngine;
using System.Collections;

public class FirstPersonController : MonoBehaviour {

    public float movementSpeed = 7.5f;

    public float mouseSensiivity = 3.5f;

    float verticalRotation = 0;
    public float lookUpDownRange = 60.0f;

    public Camera MainCamera;

	// Use this for initialization
	void Start () {
        Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void Update () {
        //View

        float rotLeftRight = Input.GetAxis("Mouse X") * mouseSensiivity;
        transform.Rotate(0, rotLeftRight, 0);

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensiivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -lookUpDownRange, lookUpDownRange);
        MainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        //Movement
        float forewardSpeed = Input.GetAxis("Vertical") * movementSpeed;
        float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;

        Vector3 speed = new Vector3(sideSpeed, 0, forewardSpeed);

        speed = transform.rotation * speed;

        CharacterController cc = GetComponent<CharacterController>();

        cc.SimpleMove(speed);

	}
}
