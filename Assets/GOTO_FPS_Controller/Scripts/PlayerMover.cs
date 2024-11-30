using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMover : MonoBehaviour
{
	private Rigidbody rb;
	private PlayerControllerSettings settings; 
	
	//for displaying speed
	[SerializeField] private TextMeshProUGUI speedText; // Assign your Text UI element in the Inspector
	private const float UnityToHammerUnit = 1f ;
	
	private Transform myTransform;
	private Vector3 input;
	private ControllerSettings cachedSettings;
	private Vector3 currentVelocity = Vector3.zero;
	public bool isGrounded = false;
	
	
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
	{
		rb = GetComponent<Rigidbody>();    
		
		settings = ControllerSettings.Instance.PlayerSettings;
	}
    
	void Update()
	{
		//get input        // Capture input in Update
		input = new Vector3(
			Input.GetAxis("Horizontal"), 
			0, 
			Input.GetAxis("Vertical")
		).normalized;
	}
    
    
	void FixedUpdate()
	{
		ProcessMovement();
		ProcessJumping();
				
		// Calculate and display speed in Unity Units
		if(settings.showSpeed) DisplaySpeed();	
	}
	

	
	private void ProcessMovement()
	{
		if(IsJumping()) print("JUMPING");
		if(isGrounded) print("Is Grounded");
		
		//check if there is any input
		bool hasInput = input.magnitude > 0.1f;
		
		// Determine target speed based on sprint status
		float targetSpeed = IsSprinting() ? settings.runSpeed : settings.walkSpeed;
		
		//if no input, be more aggressive in velocity reduction
		if(!hasInput)
		{
			
			
						
			// Modify air control if not grounded
			if (!isGrounded)
			{
				print("NO INPUT IN AIR");
				currentVelocity = Vector3.Lerp(
					currentVelocity,
					Vector3.zero,
					settings.acceleration * Time.deltaTime * settings.airControlFactor
				);
			
			}else
			{
				print("NO INPUT ON GROUND");
				currentVelocity = Vector3.Lerp(
					currentVelocity,
					Vector3.zero,
					settings.acceleration * Time.deltaTime
				);
			}

			
		}else
		{
			Vector3 targetVelocity = transform.TransformDirection(input) * targetSpeed;
		
		
			
			// Modify air control if not grounded
			if (!isGrounded)
			{
				currentVelocity = Vector3.MoveTowards(
					currentVelocity,
					targetVelocity,
					settings.acceleration * Time.fixedDeltaTime * settings.airControlFactor
				);
			
			}else
			{
				currentVelocity = Vector3.MoveTowards(
					currentVelocity,
					targetVelocity,
					settings.acceleration * Time.fixedDeltaTime
				);
			
			}

		
			
			
		}
		

		//apply the horizontal velocity		
		rb.linearVelocity = new Vector3(
			currentVelocity.x,
			rb.linearVelocity.y,
			currentVelocity.z
		);
		
	}
	
	private void ProcessJumping()
	{
		isGrounded = CheckGrounded();
		
		if (IsJumping() && isGrounded)
		{
			// Apply Jumping
			rb.AddForce(Vector3.up	* settings.jumpForce, ForceMode.Impulse);
		}
		
		

	}
	
	private bool  CheckGrounded()
	{
		// You'll need to adjust the ground check method based on your specific game setup
		// This is a simple raycast method - you might want to replace it with a more robust solution
		float rayDistance = 1.5f; // Slightly above 0 to account for small imperfections
		return Physics.Raycast(transform.position, Vector3.down, rayDistance);
	}
	
	private bool IsJumping()
	{
		
		return Input.GetKey(KeyCode.Space);
	}
	
	private bool IsSprinting()
	{
		return Input.GetAxis("Sprint") > 0f;
	}
	
	private void DisplaySpeed()
	{
		if (speedText != null)
		{
			float speedInHammerUnits = currentVelocity.magnitude * UnityToHammerUnit;
			speedText.text = $"Speed: {speedInHammerUnits:F2} UU/s";
		}
	}
	
	
}
