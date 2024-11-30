using UnityEngine;

public class CameraCommander : MonoBehaviour
{
	private PlayerControllerSettings settings;
	
	private float curFov = 65f;
	
	// Awake is called when the script instance is being loaded.
	protected void Awake()
	{
		settings = ControllerSettings.Instance.PlayerSettings;
		curFov = settings.fieldOfView;
	}
	
	
	
	
    // Update is called once per frame
    void Update()
	{
		float smoothingFactor = settings.zoomSmoothRate;
		float targetFov = IsAiming() ? settings.zoomFieldOfView : settings.fieldOfView;
		curFov = Mathf.Lerp(curFov, targetFov , 1f / smoothingFactor);
		Camera.main.fieldOfView = curFov;
	}
    
    
	private bool IsAiming()
	{
		return Input.GetAxis("Fire2") > 0;
	}
}
