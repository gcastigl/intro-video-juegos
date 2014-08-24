using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	public Vector2 velocity;
	public float ttd;
	public GameObject shooter;

	void Start () {
		Destroy(gameObject, ttd);
	}
	
	void FixedUpdate () {
		rigidbody.velocity = velocity;
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject != shooter) {
			Health health = other.gameObject.GetComponent<Health>();
			health.damage(1);
			Destroy(gameObject);
		}
	}

}
