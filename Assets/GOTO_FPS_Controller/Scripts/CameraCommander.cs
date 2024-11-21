using UnityEngine;

public class CameraCommander : MonoBehaviour
{
	
	private float curFov = ControllerSettings.i.fieldOfView;
	
    // Update is called once per frame
    void Update()
	{
		
		
		float smoothingFactor = ControllerSettings.i.zoomSmoothRate;
		
		float targetFov = IsAiming() ? ControllerSettings.i.zoomFieldOfView : ControllerSettings.i.fieldOfView;
		
		curFov = Mathf.Lerp(curFov, targetFov , 1f / smoothingFactor);
		
		Camera.main.fieldOfView = curFov;
	}
    
    
	private bool IsAiming()
	{
		return Input.GetAxis("Fire2") > 0;
	}
}
