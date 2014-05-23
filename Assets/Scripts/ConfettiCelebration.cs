using UnityEngine;
using System.Collections;

public class ConfettiCelebration : MonoBehaviour , GameEvents.GameEventListener
{
	ParticleSystem confetti;
	
	// Use this for initialization
	void Start ()
	{
		GameEvents.GameEventManager.registerListener(this);
		
		confetti = GetComponent<ParticleSystem>();
	}
	
	void OnDisable()
	{
		GameEvents.GameEventManager.unregisterListener(this);
	}
	
	public void receiveEvent(GameEvents.GameEvent e)
	{
		if(e.GetType().Name.Equals("CelebrateEvent"))
		{
			confetti.Play();
		}
		
	}

}
