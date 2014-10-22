using UnityEngine;
using System.Collections;

public class Monster1 : MonoBehaviour {

	static string attack01Name = "attack01";

	public int playerLayerMask;
	public float moveSpeed = 1;
	public float turnSpeed = 1;
	public float viewDistance = 15;

	private Animator animator;
	private Player player;

	// XXX: CharacterMotor is defined in JS, ignore compile error...
	private CharacterMotor motor;

	void Awake () {
		motor = GetComponent<CharacterMotor> ();
		animator = GetComponentInChildren<Animator> ();
	}

	void Update () {
		loadPlayer();
		float distance = Vector3.Distance(player.transform.position, transform.position);
		animator.SetFloat("playerDistance", distance);
		bool playerIsVisible = false;
		float computedViewDistance = player.isTorchHigh () ? viewDistance : viewDistance / 2;
		if (distance < computedViewDistance) {
			Vector3 direction = player.transform.position - transform.position;
			Ray ray = new Ray (transform.position, direction.normalized);
			RaycastHit hitInfo = new RaycastHit ();
			bool hit = Physics.Raycast(ray, out hitInfo, computedViewDistance);
			if (hit && hitInfo.transform.gameObject.layer == playerLayerMask) {
				playerIsVisible = true;
				Quaternion lookAt = Quaternion.LookRotation(direction);
				float str = Mathf.Min (turnSpeed * Time.deltaTime, 1); 
				transform.rotation = Quaternion.Lerp(transform.rotation, lookAt, str);
				motor.inputMoveDirection = transform.forward * 0.4f;
				bool hittingAnimation = animator.GetCurrentAnimatorStateInfo(0).IsName(attack01Name);
				if (hittingAnimation && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f) {
					animator.SetBool("reloadHit", true);
					Debug.Log("HIT!!!");
				} else {
					animator.SetBool("reloadHit", false);
				}
			}
		} else {
			motor.inputMoveDirection = transform.forward * 0.1f;
			transform.Rotate(new Vector3(0, Random.value * 5 - 2.5f, 0));
		}
		animator.SetBool ("playerVisible", playerIsVisible);
	}

	private void loadPlayer() {
		if (player == null) {
			GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
			player = playerGO.GetComponent<Player>();
		}
	}
}
