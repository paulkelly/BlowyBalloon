using UnityEngine;
using System.Collections;
using InControl;
using System.Collections.Generic;

public class InputHandler : MonoBehaviour
{

	public static InputHandler Instance { get; private set;}

	// Use this for initialization
	void Start ()
	{
	
		if (Instance == null)
		{
			Instance = this;
		} 
		else if(Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		//InputManager.EnableXInput = true;
		InputManager.Setup();
		
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update ()
	{
		InputManager.Update();
				
		int i=1;
		foreach(InputDevice inputDevice in InputManager.Devices)
		{
			InputEvent e = new InputEvent(inputDevice, i);
			GameEvents.GameEventManager.post(e);
			i++;
		}
		
	}
}
