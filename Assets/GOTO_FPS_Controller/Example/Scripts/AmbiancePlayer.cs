using UnityEngine;
using System.Collections.Generic;

public class AmbiancePlayer : MonoBehaviour
{
	
	//references
	[SerializeField] private AudioSource _audioSource;
	[SerializeField] private AudioClip[] _audioClips;
	[SerializeField] [Range(0f,10f)] private float _pitchVariance = 0;
	[SerializeField] [Range(0f,10f)] private float _volumeVariance = 0;
	
	//bools
	[SerializeField] private bool _canLoop = true;
	[SerializeField] private bool _isWeighted = true;
	
	
	
	//variables
	private AudioClip _currentClip;
	private List<AudioClip> _clipPool;
	private int _currentIndex = -1;

	// Awake is called when the script instance is being loaded.
	protected void Awake()
	{
		//check audio source
		if(_audioSource == null ) _audioSource = GetComponent<AudioSource>();
		
		//Assert error if no audio clips exsist
		Debug.Assert(_audioClips.Length <= 0,"There are no Audio Clips in this component");
		
		//Initialize the shuffle pool
		InitializeShuffleBag();
		PlayNextClip();
	}
	
	
	
	private void InitializeShuffleBag()
	{
		//create a new list and shuffle the audio clips
		_clipPool = new List<AudioClip>(_audioClips);
		Shuffle(_clipPool);
		
	}
	
	private void Shuffle(List<AudioClip> list)
	{
		for (int i = 0; i < list.Count; i++)
		{
			int randomIndex = Random.Range(0, list.Count);
			AudioClip temp = list[i];
			list[i] = list[randomIndex];
			list[randomIndex] = temp;
		}
	}
	
	private void PlayNextClip()
	{
		if(_clipPool.Count == 0)
		{
			if(_canLoop)
			{
				InitializeShuffleBag();
			}else
			{
				return;
			}
		}
		
		//get the next clip
		_currentClip = _clipPool;
		_clipPool.RemoveAt(0);
		
		// Apply pitch and volume variance
		_audioSource.pitch = 1f + Random.Range(-_pitchVariance, _pitchVariance);
		_audioSource.volume = 1f + Random.Range(-_volumeVariance, _volumeVariance);

		// Play the clip
		_audioSource.clip = _currentClip;
		_audioSource.Play();

		// Schedule the next clip when the current one finishes
		Invoke(nameof(PlayNextClip), _currentClip.length);
		
	}
}
