using UnityEngine;
using System.Collections;

public class MonsterDisappear : MonoBehaviour {

	void OnTriggerEnter(Collider otherObj){
		this.gameObject.SetActive (false);
	}
}
