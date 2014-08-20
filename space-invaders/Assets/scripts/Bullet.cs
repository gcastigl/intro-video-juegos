using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	public Vector2 velocity;

	void Start () {
		
	}
	
	void FixedUpdate () {
		rigidbody2D.velocity = velocity;
	}
}
