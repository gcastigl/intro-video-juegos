using UnityEngine;
using System.Collections;

public class Defender : MonoBehaviour {
	
	public Vector2 speed;
	public Vector2 xBounds;
	private Vector2 movement;
	private Gun gun;

	void Start() {
		gun = gameObject.GetComponentInChildren<Gun>() as Gun;
	}

	void Update() {
		float inputX = Input.GetAxis("Horizontal");
		if (outOfBoundsLeft()) {
			inputX = Mathf.Max(0, inputX);
		} else if (outOfBoundsRight()) {
			inputX = Mathf.Min(0, inputX);
		}
		movement = new Vector2(speed.x * inputX, 0);
		if (Input.GetKey(KeyCode.Space)) {
			gun.shoot(new Vector3(0, 1, 0));
		}
	}

	public bool outOfBoundsLeft() {
		return transform.position.x < xBounds.x;
	}

	public bool outOfBoundsRight() {
		return transform.position.x > xBounds.y;
	}

	void FixedUpdate() {
		rigidbody.velocity = movement;
	}
	
}
