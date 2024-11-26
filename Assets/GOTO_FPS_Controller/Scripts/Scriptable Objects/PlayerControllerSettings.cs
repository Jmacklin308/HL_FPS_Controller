using UnityEngine;

[CreateAssetMenu(fileName = "PlayerControllerSettings", menuName = "Scriptable Objects/PlayerControllerSettings")]
public class PlayerControllerSettings : ScriptableObject
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
	
	
	[Header("Movement Settings")]
	[Range(1f,40f)] public float walkSpeed = 20f; 
	[Range(1f,40f)] public float runSpeed = 22f; 
	[Range(1f,100f)] public float acceleration = 60f; 
	
	
	[Header("Jump Settings")]
	[Range(10, 40f)] public float jumpForce = 15f; 
	[Range(0f, 1f)] public float airControlFactor = 0.5f;
	
	
	[Header("Crouch Settings")]
	[Range(0.1f, 1f)] public float crouchSpeedFactor = 0.5f; // Movement speed multiplier when crouching.
	[Range(0.1f, 2f)] public float crouchHeight = 0.5f; // Height multiplier when crouching.
	
	
	[Header("Gravity Settings")]
	[Range(0f, 50f)] public float gravityMultiplier = 9.8f; // For controlling how fast the player falls.
	
	
	[Header("Traversal Settings")]
	[Range(0f, 1f)] public float stepHeight = 0.3f; // Maximum height of obstacles the player can walk over.

}
