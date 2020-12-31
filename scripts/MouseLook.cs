using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {
	[SerializeField]
	public float mouseSensitivity = 100f;

	[SerializeField]
	public Transform playerBody;

	float xRotation = 0f;

	public bool mouseLook = true;

	// Use this for initialization
	void Start () {
		// hide cursor
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
		if (mouseLook) {
			// Time.deltaTime accounts for higher framerate
			float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
			float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

			xRotation -= mouseY;
			// prevent over rotation
			xRotation = Mathf.Clamp(xRotation, -90f, 90f);
			transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
			playerBody.Rotate(Vector3.up * mouseX);
		}
	}
}
