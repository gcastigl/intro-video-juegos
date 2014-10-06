using UnityEngine;
using System.Collections;

public class Monster1 : MonoBehaviour {

	public float moveSpeed = 2;
	public float turnSpeed = 2;

	private GameObject player;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update () {
		// FIXME: no existe un distanceSq?? (Chequeo mas eficiente)
		float distance = Vector3.Distance(player.transform.position, transform.position);
		if (distance > 50) {
			return;
		}
		if (distance < 2) {
			return;
		}
		Vector3 direction = player.transform.position - transform.position;
		Vector3 moveVector = direction.normalized * moveSpeed * Time.deltaTime;
		transform.position += moveVector;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
	}

}
