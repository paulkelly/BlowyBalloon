using UnityEngine;
using System.Collections;
using InControl;

public class InputEvent : GameEvents.GameEvent
{

	private InputDevice device;
	private int playerNumber;
	
	public InputEvent(InputDevice theDevice, int number)
	{
		device = theDevice;
		playerNumber = number;
	}
	
	public InputDevice GetDevice()
	{
		return device;
	}
	
	public int GetPlayerNumber()
	{
		return playerNumber;
	}
}
