using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	public Vector2 velocity;
	public float ttd;
	public string hitTag;

	void Update() {
		ttd -= Time.deltaTime;
		if (ttd <= 0) {
			Destroy(gameObject);
		}
	}
	
	void FixedUpdate () {
		rigidbody.velocity = velocity;
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == hitTag) {
			ttd = 0;
			Invader invader = other.gameObject.GetComponent<Invader>();
			invader.hit();
		}
	}

}
