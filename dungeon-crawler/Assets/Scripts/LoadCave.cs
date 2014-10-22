using UnityEngine;
using System.Collections;

public class LoadCave : MonoBehaviour {

	void OnTriggerEnter(Collider otherObj){
		Application.LoadLevel ("main");
	}
}
