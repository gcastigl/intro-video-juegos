﻿using UnityEngine;
using System.Collections;

public class ChestOpen : MonoBehaviour {

	public Animation chestAnimation;
	public bool opening = false;
	public GameObject contents;

	void Start () {

	}

	void Update() {
		if (opening && !chestAnimation.isPlaying) {
			Destroy(contents);
			Destroy(this);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == 9) {
			Debug.Log ("Treasure chest opened!!");
			chestAnimation.Play();
			opening = true;
		}
	}
}
