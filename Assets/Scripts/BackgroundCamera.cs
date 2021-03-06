﻿using UnityEngine;
using System.Collections;

public class BackgroundCamera : MonoBehaviour {

	const float NORMAL_ASPECT = 16/9f;
	const float COMPUTER_WIDE_ASPECT = 16/10f;
	const float EPSILON = 0.01f;
	
	void Start ()
	{		
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
}
