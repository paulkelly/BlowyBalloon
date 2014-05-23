using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnGates : MonoBehaviour, GameEvents.GameEventListener
{
	// Use the cameras x axis for spawning
	Camera mainCamera;
	float cameraWidth;

	public static float firstX = 10f;
	public float minXDistance = 8f;
	public float maxXDistance = 12f;
	
	// How close to spawn the object at the edge of the screen
	public float xBuffer = 2f;
	
	float nextX = firstX;


	public float minY = -2.8f;
	public float maxY = 3;
	
	
	public GameObject gatePrefab;
	Vector3 offscreen = new Vector3(-20, 0, 0);
	public int poolSize = 6;
	LinkedList<GameObject> gatePool = new LinkedList<GameObject>();
	//List<GameObject> gatesInUse = new List<GameObject>();
	

	// Use this for initialization
	void Start ()
	{
		GameEvents.GameEventManager.registerListener(this);
	
		mainCamera = Camera.main;
		cameraWidth = mainCamera.orthographicSize * mainCamera.aspect;
		
		for(int i=0; i<poolSize; i++)
		{
			InitializeGate();
		}
	}
	
	void Update()
	{
		float maxVisibleX = mainCamera.transform.position.x + cameraWidth;
		
		if(maxVisibleX > nextX - xBuffer)
		{
			SpawnGate(nextX);
		}
	}

	void SpawnGate (float x)
	{
		GameObject newGate = GetFirstFreeGate();
		
		float newYPosition = Random.Range(minY, maxY);
		newGate.GetComponent<GateScript>().Reset(new Vector3(x, newYPosition, newGate.transform.position.z));
		
		nextX = x + Random.Range(minXDistance, maxXDistance);
		
	}
	
	GameObject GetFirstFreeGate()
	{
		if(gatePool.Count < 1)
		{
			InitializeGate();	
		}
		
		GameObject newGate = gatePool.First.Value;
		gatePool.RemoveFirst();
		
		return newGate;
	}
	
	void AddGateToPool(GameObject gate)
	{
		//gatesInUse.Remove(gate);
		if(!gatePool.Contains(gate))
		{
			gatePool.AddLast(gate);
		}
	}
	
	void InitializeGate()
	{
		GameObject newGate = (GameObject) Instantiate(gatePrefab, offscreen, Quaternion.identity);
		gatePool.AddLast(newGate);
	}
	
	public void receiveEvent(GameEvents.GameEvent e)
	{
		string gateExistScreenEventName = "GateExitScreenEvent";
		
		if(e.GetType().Name.Equals(gateExistScreenEventName))
		{
			AddGateToPool(((GateExitScreenEvent) e).GetGate());
		}
	}
}