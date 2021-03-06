﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
	public float xSmooth = 1f;		// How smoothly the camera catches up with it's target movement in the x axis.
	public float ySmooth = 1f;		// How smoothly the camera catches up with it's target movement in the y axis.

	public Vector2 offset = new Vector2(0, 0f);

	public Transform target;

	const float NORMAL_ASPECT = 4/3f;
	const float COMPUTER_WIDE_ASPECT = 16/10f;
	const float EPSILON = 0.01f;
	
	Vector3 lastPosition = Vector3.zero;
	
	void Start ()
	{
		lastPosition = target.transform.position;
		
		float aspectRatio = Screen.width / ((float)Screen.height);
		
		if (Mathf.Abs(aspectRatio - NORMAL_ASPECT) < EPSILON)
		{
			camera.rect = new Rect(0f, 0.125f, 1f, 0.75f); // 16:9 viewport in a 4:3 screen res
		}
		else if (Mathf.Abs(aspectRatio - COMPUTER_WIDE_ASPECT) < EPSILON)
		{
			camera.rect = new Rect(0f, 0.05f, 1f, 0.9f); // 16:9 viewport in a 16:10 screen res
		}
	}


	
	void FixedUpdate()
	{
		TrackTarget();
		
		float movementX = transform.position.x - lastPosition.x;
		float movementY = 0;
		
		BackgroundScrollEvent scroll = new BackgroundScrollEvent (movementX, movementY);
		GameEvents.GameEventManager.post (scroll);
		
		lastPosition = transform.position;
	}

	void TrackTarget ()
	{
		// By default the target x and y coordinates of the camera are it's current x and y coordinates.
		float targetX = target.transform.position.x + offset.x;
		float targetY = transform.position.y + offset.y;

		// ... the target x coordinate should be a Lerp between the camera's current x position and the player's current x position.
		targetX = Mathf.Lerp(transform.position.x, targetX, xSmooth * Time.deltaTime);

		// ... the target y coordinate should be a Lerp between the camera's current y position and the player's current y position.
		targetY = Mathf.Lerp(transform.position.y, targetY, ySmooth * Time.deltaTime);

		// Set the camera's position to the target position with the same z component.
		transform.position = new Vector3(targetX, targetY, transform.position.z);

	}
}
