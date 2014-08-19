using UnityEngine;
using System.Collections;

public class InvaderLine : MonoBehaviour {

	public float xSpeed;
	public Vector2 xLimits;

	private Vector2 velocity;

	void Start () {
		velocity = new Vector2(xSpeed, 0);
	}

	void Update() {
		if (escapedMinX() || escapedMaxX()) {
			velocity = velocity * -1;
		}
	}

	private bool escapedMaxX() {
		return transform.position.x < xLimits.x && velocity.x < 0; 
	}

	private bool escapedMinX() {
		return transform.position.x > xLimits.y && velocity.x > 0;
	}

	void FixedUpdate () {
		rigidbody2D.velocity = velocity;
	}
}
