using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMover : MonoBehaviour
{
	private Rigidbody rb;
	
	//for displaying speed
	[SerializeField] private TextMeshProUGUI speedText; // Assign your Text UI element in the Inspector
	private const float UnityToHammerUnit = 1f ;
	
	private Transform myTransform;
	private Vector3 input;
	private ControllerSettings cachedSettings;
	private Vector3 currentVelocity = Vector3.zero;
	
	
	private PlayerControllerSettings settings; 
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
		
		
		//check if there is any input
		bool hasInput = input.magnitude > 0.5f;
		
		// Determine target speed based on sprint status
		float targetSpeed = IsSprinting() ? settings.runSpeed : settings.walkSpeed;
		
		//if no input, be more aggressive in velocity reduction
		if(!hasInput)
		{
			currentVelocity = Vector3.Lerp(currentVelocity, Vector3.zero, settings.acceleration * Time.fixedDeltaTime);
		}else
		{
			Vector3 targetVelocity = transform.TransformDirection(input) * targetSpeed;
		
			currentVelocity = Vector3.MoveTowards(
				currentVelocity,
				targetVelocity,
				settings.acceleration * Time.fixedDeltaTime
			);
		}
		

		//apply the horizontal velocity		
		rb.linearVelocity = new Vector3(
			currentVelocity.x,
			rb.linearVelocity.y,
			currentVelocity.z
		);
		
		// Calculate and display speed in Hammer units
		DisplaySpeed();	
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
