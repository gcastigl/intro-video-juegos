using UnityEngine;
using System.Collections;

public class WinScript : MonoBehaviour {

	void OnTriggerEnter(Collider otherObj){
		Debug.Log("Collide");
		GameObject playerGO = GameObject.FindGameObjectWithTag ("Player");
		if (otherObj.gameObject.Equals (playerGO)) {
			Debug.Log("Hola");
		}
	}
}
