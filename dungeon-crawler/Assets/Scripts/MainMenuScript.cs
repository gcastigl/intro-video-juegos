﻿using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {
	
	void Start () {
	
	}

	void Update () {
		if (Input.GetKeyUp(KeyCode.Space)) {
			Application.LoadLevel("Start");
		}
	}
}
