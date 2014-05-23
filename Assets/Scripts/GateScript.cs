using UnityEngine;
using System.Collections;

public class GateScript : MonoBehaviour
{
	bool hasBeenPassed = false;
	bool hasBeenRecycled = true;
		
	public void Reset(Vector3 position)
	{
		transform.position = position;
		hasBeenPassed = false;
		hasBeenRecycled = false;
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.tag.Equals("Player"))
		{
			if(!hasBeenPassed)
			{
				hasBeenPassed = true;
				PassGateEvent e = new PassGateEvent();
				GameEvents.GameEventManager.post(e);	
			}
		}
		else if(collider.tag.Equals("Cleanup"))
		{
			if(!hasBeenRecycled)
			{
				hasBeenRecycled = true;
				GateExitScreenEvent e = new GateExitScreenEvent(gameObject);
				GameEvents.GameEventManager.post(e);
			}
		}

	}

}
