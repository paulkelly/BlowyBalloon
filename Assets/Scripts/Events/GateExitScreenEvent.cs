using UnityEngine;
using System.Collections;

public class GateExitScreenEvent : GameEvents.GameEvent
{
	GameObject gate;
	
	public GateExitScreenEvent(GameObject obj)
	{
		gate = obj;
	}
	
	public GameObject GetGate()
	{
		return gate;
	}
}
