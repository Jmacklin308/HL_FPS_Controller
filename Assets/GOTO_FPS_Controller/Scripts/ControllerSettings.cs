using UnityEngine;

public class ControllerSettings : MonoBehaviour
{
	
	
	[Header("Mouse Settings")]
	[Range(0f,100f)] public float mouse_sensitivity = 50f;
	[Range(0f,90f)] public float lowerViewLimit = 90f;
	[Range(0f,-90f)] public float upperViewLimit = -90f;
	[Range(5f,20f)] public float smoothRate = 10f;
	public bool mouseSmoothEnable = true;
	
	[Header("Camera Settings")]
	[Range(20f,160f)] public float fieldOfView = 75f;
	[Range(20f,160f)] public float zoomFieldOfView = 55f;
	[Range(0f,20f)] public float zoomSmoothRate = 20f;
	
	
	
	//singleton
	public static ControllerSettings i;
	
	
	// Awake is called when the script instance is being loaded.
	protected void Awake()
	{
		if(i == null)
		{
			i = this;
		}else if (i != this)
		{
			Destroy(this);	
		}
	}
	
}
