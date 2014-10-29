using UnityEngine;
using System.Collections;

public class Monster1 : MonoBehaviour {

	static string attack01Name = "attack01";
	public float roarTimeout = 5;

	public int playerLayerMask;
	public float moveSpeed = 1;
	public float turnSpeed = 1.5f;
	public float viewDistance = 15;

	private DungeonManager dungeonManager;
	private Animator animator;
	private AudioSource roarSource;
	private float lastRoarTimeout = 0;

	// XXX: CharacterMotor is defined in JS, ignore compile error...
	private CharacterMotor motor;

	void Awake () {
		dungeonManager = GameObject.FindGameObjectWithTag ("DungeonManager").GetComponent<DungeonManager>();
		motor = GetComponent<CharacterMotor> ();
		animator = GetComponentInChildren<Animator> ();
		roarSource = GetComponent<AudioSource> ();
	}

	void Update () {
		Player player = dungeonManager.getPlayer();
		if (player.alive && dungeonManager.dungeonStatus == DungeonManager.STATUS_UNSOLVED) {
			chaseAndAttack();
		} else {
			animator.SetBool ("playerVisible", false);
		}
	}

	private void chaseAndAttack() {
		Player player = dungeonManager.getPlayer();
		float distance = Vector3.Distance(player.transform.position, transform.position);
		animator.SetFloat("playerDistance", distance);
		bool playerIsVisible = false;
		float computedViewDistance = player.isTorchHigh () ? viewDistance : viewDistance / 2;
		if (lastRoarTimeout > 0) {
			lastRoarTimeout -= Time.deltaTime;
		}
		if (distance < computedViewDistance) {
			Vector3 direction = player.transform.position - transform.position;
			Ray ray = new Ray (transform.position, direction.normalized);
			RaycastHit hitInfo = new RaycastHit ();
			bool hit = Physics.Raycast(ray, out hitInfo, computedViewDistance);
			if (hit && hitInfo.transform.gameObject.layer == playerLayerMask) {
				playerIsVisible = true;
				if (!roarSource.isPlaying && lastRoarTimeout <= 0) {
					roarSource.Play();
					lastRoarTimeout = Random.Range(roarTimeout, roarTimeout * 2);
				}
				Quaternion lookAt = Quaternion.LookRotation(direction);
				float str = Mathf.Min (turnSpeed * Time.deltaTime, 1); 
				float angleDiff = Quaternion.Angle(transform.rotation, lookAt);
				transform.rotation = Quaternion.Lerp(transform.rotation, lookAt, str);
				motor.inputMoveDirection = transform.forward * moveSpeed * (angleDiff < 15 ? 1 : 0);
				bool hittingAnimation = animator.GetCurrentAnimatorStateInfo(0).IsName(attack01Name);
				if (hittingAnimation && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f) {
					player.kill();
					animator.SetBool("reloadHit", true);
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

}
