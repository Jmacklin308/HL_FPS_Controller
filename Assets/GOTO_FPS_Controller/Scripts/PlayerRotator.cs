using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
	private float yaw = 0f; // Tracks the current yaw angle
	private PlayerControllerSettings settings;
	void Start()
	{
		
		settings = ControllerSettings.Instance.PlayerSettings;
		// Optionally lock the cursor for mouse-based rotation
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void Update()
	{
		// Get the horizontal input (mouse or joystick)
		float mouseX = Input.GetAxis("Mouse X") * settings.mouse_sensitivity * Time.deltaTime;

		// Adjust the yaw based on input
		yaw += mouseX;

		// Apply the rotation to the player object
		transform.localRotation = Quaternion.Euler(0f, yaw, 0f);

	}
}