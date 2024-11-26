using UnityEngine;

public class CameraCommander : MonoBehaviour
{
	
	private float curFov = ControllerSettings.Instance.PlayerSettings.fieldOfView;
	
    // Update is called once per frame
    void Update()
	{
		float smoothingFactor = ControllerSettings.Instance.PlayerSettings.zoomSmoothRate;
		float targetFov = IsAiming() ? ControllerSettings.Instance.PlayerSettings.zoomFieldOfView : ControllerSettings.Instance.PlayerSettings.fieldOfView;
		curFov = Mathf.Lerp(curFov, targetFov , 1f / smoothingFactor);
		Camera.main.fieldOfView = curFov;
	}
    
    
	private bool IsAiming()
	{
		return Input.GetAxis("Fire2") > 0;
	}
}
