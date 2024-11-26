using UnityEngine;

public class CameraPitchControl : MonoBehaviour
{
	private float pitch = 0f; // Track the pitch

	// Smoothing variables
	private Vector2 smoothedMouseDelta;
	private Vector2 currentMouseDelta;
	private Vector2 mouseDeltaVelocity;

	private PlayerControllerSettings settings; 

	void Start()
	{
		settings = ControllerSettings.Instance.PlayerSettings;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void Update()
	{
		// Get raw mouse input
		float mouseY = Input.GetAxisRaw("Mouse Y") * settings.mouse_sensitivity;

		// Smoothing logic
		if (settings.mouseSmoothEnable)
		{
			float smoothingFactor = settings.smoothRate;
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
		pitch = Mathf.Clamp(pitch, settings.upperViewLimit, settings.lowerViewLimit);

		// Apply pitch rotation to the transform
		transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);

	}
}