using UnityEngine;

public class ControllerSettings : MonoBehaviour
{
	// Serialized field for the ScriptableObject containing persistent settings
	[SerializeField] private PlayerControllerSettings playerControllerSettings;
    
	// Public property to access the settings
	public PlayerControllerSettings PlayerSettings => playerControllerSettings;

	// Singleton instance
	public static ControllerSettings Instance { get; private set; }
    
	// Awake is called when the script instance is being loaded
	private void Awake()
	{
		// Singleton pattern implementation
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
