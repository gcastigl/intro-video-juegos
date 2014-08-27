using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	public Vector2 velocity;
	public float ttd;
	public GameObject shooter;
	public int hitAmount;
	public GameObject explosionPrefab;

	void Start () {
		Destroy(gameObject, ttd);
	}
	
	void FixedUpdate () {
		rigidbody.velocity = velocity;
	}

	void OnTriggerEnter(Collider other) {
		if (shooter == null) {
			return;
		}
		if (other.gameObject.tag != shooter.tag) {
			Health health = other.gameObject.GetComponent<Health>();
			if (health != null) {
				health.damage(hitAmount);
			}
			shooter.audio.Play();
			Object.Instantiate (explosionPrefab, gameObject.transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
