using UnityEngine;
using System.Collections;

public class MonsterDisappear : MonoBehaviour {

	public Animator animator;
	public GameObject monster;

	void Start() {
		animator.SetBool("inDaHouse", true);
	}

	void OnTriggerEnter(Collider otherObj) {
		monster.SetActive (false);
		gameObject.SetActive (false);
	}
}
