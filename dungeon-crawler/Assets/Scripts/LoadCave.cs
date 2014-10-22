using UnityEngine;
using System.Collections;

public class LoadCave : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider otherObj){
		Application.LoadLevel ("main");
	}
}
