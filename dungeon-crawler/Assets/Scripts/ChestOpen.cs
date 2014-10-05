using UnityEngine;
using System.Collections;

public class ChestOpen : MonoBehaviour {

	public Animation chestAnimation;
	
	void Start () {

	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == 9) {
			Debug.Log ("Treasure chest opened!!");
			chestAnimation.Play();
			Destroy(this);
		}
	}
}
