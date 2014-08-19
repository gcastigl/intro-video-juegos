using UnityEngine;
using System.Collections;

public class Defender : MonoBehaviour {

	public Vector2 speed;
	private Vector2 movement;

	void Update() {
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");
		movement = new Vector2(speed.x * inputX, speed.y * inputY);
	}

	void FixedUpdate() {
		// Debug.Log (movement);
		rigidbody2D.velocity = movement;
	}
}
