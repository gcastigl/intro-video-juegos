using UnityEngine;
using System.Collections;

public class MonsterDisappear : MonoBehaviour {

	public Animator animator;

	void Start() {
		animator.SetBool("inDaHouse", true);
	}

	void OnTriggerEnter(Collider otherObj) {
		this.gameObject.SetActive (false);
	}
}
