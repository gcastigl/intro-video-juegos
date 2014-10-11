using UnityEngine;
using System.Collections;

public class Monster1 : MonoBehaviour {

	public float moveSpeed = 1;
	public float turnSpeed = 1;
	public float viewDistance = 30;

	private Player player;

	void Start () {
		GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
		player = playerGO.GetComponent<Player>();
	}
	
	void Update () {
		// FIXME: no existe un distanceSq?? (Chequeo mas eficiente)
		float distance = Vector3.Distance(player.transform.position, transform.position);
		float computedViewDistance = distance * (player.isTorchHigh() ? 1 : 0.5f);
		if (computedViewDistance > viewDistance) {
			return;
		}
		if (distance < 2) {
			return;
		}
		Debug.Log ("Detectado: " + name);
		Vector3 direction = player.transform.position - transform.position;
		Vector3 moveVector = direction.normalized * moveSpeed * Time.deltaTime;
		transform.position += moveVector;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
	}

}
