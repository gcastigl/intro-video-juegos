using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	public Vector2 velocity;
	public float ttd;

	void Start () {
		Destroy(gameObject, ttd);
	}
	
	void FixedUpdate () {
		rigidbody.velocity = velocity;
	}

	void OnTriggerEnter(Collider other) {
		Health health = other.gameObject.GetComponent<Health>();
		health.damage(1);
		Destroy(gameObject);
	}

}
