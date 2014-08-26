using UnityEngine;
using System.Collections;

public class Invader : MonoBehaviour {
	
	private Gun gun;
	public bool permissionToShoot = false;

	void Start() {
		gun = gameObject.GetComponentInChildren<Gun>();
	}
	
	void Update() {
		if (permissionToShoot && Random.value < 0.01f) {
			gun.shoot(new Vector3(0, -1, 0));
			permissionToShoot = false;
		}
	}

	void OnTriggerEnter(Collider other) {
		Destroy(other.gameObject);
	}
}
