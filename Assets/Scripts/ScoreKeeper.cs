using UnityEngine;
using System.Collections;

public class ScoreKeeper : MonoBehaviour, GameEvents.GameEventListener
{

	string bestScoreKey = "BestScore";

	int score = 0;

	bool newHighScore = false;	

	void Start()
	{
		if(!PlayerPrefs.HasKey(bestScoreKey))
		{
			PlayerPrefs.SetInt(bestScoreKey, 0);
		}
		
		PlayerPrefs.SetInt(bestScoreKey, 0);
		
		GameEvents.GameEventManager.registerListener(this);
	}
	
	void OnDisable()
	{
		GameEvents.GameEventManager.unregisterListener(this);
	}
	
	void PassGate()
	{
		score++;
		
		int bestScore = PlayerPrefs.GetInt(bestScoreKey);
		
		if(score > bestScore)
		{
			if(!newHighScore)
			{
				//Only celebrate once for a new high score.
				
				CelebrateEvent e = new CelebrateEvent();
				GameEvents.GameEventManager.post(e);
				
				SoundManager.Instance.PlayCheer();
				
				newHighScore = true;
			}

			PlayerPrefs.SetInt(bestScoreKey, score);
			
		}
	}
	
	public void receiveEvent(GameEvents.GameEvent e)
	{
		if(e.GetType().Name.Equals("PassGateEvent"))
		{
			PassGate();
		}
		
	}
	
}
