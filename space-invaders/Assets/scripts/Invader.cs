using UnityEngine;
using System.Collections;

public class Invader : MonoBehaviour {
	
	private Gun gun;
	public bool permissionToShoot = false;
	public float pShooting;

	void Start() {
		gun = gameObject.GetComponentInChildren<Gun>();
	}
	
	void Update() {
		if (permissionToShoot && Random.value < pShooting) {
			gun.shoot(new Vector3(0, -1, 0));
			permissionToShoot = false;
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<Bullet>() == null) {
			Destroy(other.gameObject);
		}
	}
}
