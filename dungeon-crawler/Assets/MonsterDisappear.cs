using UnityEngine;
using System.Collections;

public class MonsterDisappear : MonoBehaviour {

	private Animator animator;

	void start(){
		animator.SetBool ("inHouse", true);
	}

	void OnTriggerEnter(Collider otherObj){
		this.gameObject.SetActive (false);
	}
}
