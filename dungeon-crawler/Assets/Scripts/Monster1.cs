using UnityEngine;
using System.Collections;

public class Monster1 : MonoBehaviour {

	public int playerLayerMask;
	public float moveSpeed = 1;
	public float turnSpeed = 1;
	public float viewDistance = 15;

	private Player player;

	// XXX: CharacterMotor is defined in JS, ignore compile error...
	private CharacterMotor motor;

	void Start () {
		GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
		player = playerGO.GetComponent<Player>();
	}

	void Awake () {
		motor = GetComponent<CharacterMotor> ();
	}

	void Update () {
		motor.inputMoveDirection = Vector3.zero;
		// FIXME: no existe un distanceSq?? (Chequeo mas eficiente)
		float distance = Vector3.Distance(player.transform.position, transform.position);
		if (!player.isTorchHigh()) {	// Cant see in the dark!
			Vector3 forward = transform.TransformDirection(Vector3.forward);
			motor.inputMoveDirection = forward.normalized * 0.05f;
			if (Random.value < 0.3f) {
				float angle = Random.value * 15 - 7.5f;
				transform.Rotate(new Vector3(0, angle, 0));
			}
			return;
		}
		if (distance > viewDistance) {		// Player is too far away
			return;
		}
		Vector3 direction = player.transform.position - transform.position;
		Ray ray = new Ray (transform.position, direction.normalized);
		RaycastHit hitInfo = new RaycastHit ();
		bool hit = Physics.Raycast(ray, out hitInfo, viewDistance);
		if (hit && hitInfo.transform.gameObject.layer != playerLayerMask) {
			// PLayer is behind something...
			return;
		}
		Quaternion lookAt = Quaternion.LookRotation (direction);
		float str = Mathf.Min (turnSpeed * Time.deltaTime, 1); 
		transform.rotation = Quaternion.Lerp(transform.rotation, lookAt, str);
		if (distance < 2) {
			return;
		}
		if (Mathf.Abs(str) < 0.04f) {
			motor.inputMoveDirection = direction.normalized;
		}
	}

}
