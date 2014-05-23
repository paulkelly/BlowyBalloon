using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{

	public static SoundManager Instance { get; private set;}

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		} 
		else if(Instance != this)
		{
			Destroy(gameObject);
		}
		
		DontDestroyOnLoad (gameObject);
	}
	
	public float minPitch = 0.7f;
	public float maxPitch = 1.4f;
	
	public AudioClip[] pop;
	public float minPopVolume = 0.25f;
	public float maxPopVolume = 1f;
	
	public AudioClip[] cheer;
	public float minCheerVolume = 0.25f;
	public float maxCheerVolume = 1f;
	
	public AudioClip beep;
	public AudioClip beep2;
	public float pitch0 = 1.6f;
	public float pitch1 = 1.3f;
	public float pitch2 = 1.0f;
	public float pitch3 = 0.7f;
	
	private void PlayRandomSound(AudioClip clip, float pitch, float volume)
	{
		audio.pitch = pitch;
		audio.volume = volume;
		audio.PlayOneShot(clip);
	}
	
	public void PlayPop()
	{
		PlayRandomSound (pop[Random.Range (0, pop.Length)], 
		                 Random.Range (minPitch, maxPitch), 
		                 Random.Range (minPopVolume, maxPopVolume));
	}
	
	public void PlayCheer()
	{
		PlayRandomSound (cheer[Random.Range (0, cheer.Length)], 
		                 Random.Range (minPitch, maxPitch), 
		                 Random.Range (minCheerVolume, maxCheerVolume));
	}
	
	public void Beep3()
	{
		audio.pitch = pitch3;
		audio.volume = 1;
		audio.PlayOneShot(beep);
	}
	
	public void Beep2()
	{
		audio.pitch = pitch2;
		audio.volume = 1;
		audio.PlayOneShot(beep);
	}
	
	public void Beep1()
	{
		audio.pitch = pitch1;
		audio.volume = 1;
		audio.PlayOneShot(beep);
	}
	
	public void Beep0()
	{
		audio.pitch = pitch0;
		audio.volume = 1;
		audio.PlayOneShot(beep2);
	}

}
