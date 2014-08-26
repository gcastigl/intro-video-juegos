using UnityEngine;
using System.Collections;

public class Defender : MonoBehaviour {
	
	public Vector2 speed;
	private Vector2 movement;
	private Gun gun;

	void Start() {
		gun = gameObject.GetComponentInChildren<Gun>() as Gun;
	}

	void Update() {
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");
		movement = new Vector2(speed.x * inputX, 0);
		if (Input.GetKey(KeyCode.Space)) {
			gun.shoot(new Vector3(0, 1, 0));
		}
	}

	void FixedUpdate() {
		rigidbody.velocity = movement;
	}
	
}
