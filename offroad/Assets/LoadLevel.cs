using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {

	void OnCollisionEnter(Collision colission){
		Application.LoadLevel("offroad");
	}
}
