using UnityEngine;
using System.Collections;

public class InvaderLine : MonoBehaviour {

	public GameObject invaderPrefab;
	public int invadersCount;
	public float invaderSeparation;
	public float xSpeed;
	public Vector2 xLimits;

	private Vector2 velocity;

	void Start() {
		float halfTotalWidth = invadersCount * invaderSeparation / 2;
		for (int i = 0; i < invadersCount; i++) {
			float x = i * invaderSeparation - halfTotalWidth;
			Vector3 position = new Vector3(x, transform.position.y, transform.position.z);
			Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, 180));
			GameObject enemy = Object.Instantiate(invaderPrefab, position, rotation) as GameObject;
			enemy.transform.parent = transform;
		}
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
		rigidbody.velocity = velocity;
	}
}
