using UnityEngine;
using System.Collections;

public class Invader : MonoBehaviour {

	private Gun gun;

	void Start() {
		gun = gameObject.GetComponentInChildren<Gun>();
	}
	
	void Update() {
		if (gun.isReady() && Random.value < 0.005f) {
			gun.shoot(new Vector3(0, -1, 0));
		}
	}

}
