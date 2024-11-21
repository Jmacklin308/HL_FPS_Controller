using UnityEngine;

public class CameraPitchControl : MonoBehaviour
{
	private float pitch = 0f; // Track the pitch

	// Smoothing variables
	private Vector2 smoothedMouseDelta;
	private Vector2 currentMouseDelta;
	private Vector2 mouseDeltaVelocity;

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void Update()
	{
		// Get raw mouse input
		float mouseY = Input.GetAxisRaw("Mouse Y") * ControllerSettings.i.mouse_sensitivity;

		// Smoothing logic
		if (ControllerSettings.i.mouseSmoothEnable)
		{
			float smoothingFactor = ControllerSettings.i.smoothRate;
			currentMouseDelta.y = Mathf.Lerp(currentMouseDelta.y, mouseY, 1f / smoothingFactor);

			// Apply smoothing to pitch
			pitch -= currentMouseDelta.y * Time.deltaTime;
		}
		else
		{
			// No smoothing: direct input application
			pitch -= mouseY * Time.deltaTime;
		}

		// Clamp pitch to allowed range
		pitch = Mathf.Clamp(pitch, ControllerSettings.i.upperViewLimit, ControllerSettings.i.lowerViewLimit);

		// Apply pitch rotation to the transform
		transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);

		// Debugging: Output current pitch
		Debug.Log($"Pitch (X): {pitch}");
	}
}