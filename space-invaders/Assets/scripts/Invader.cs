using UnityEngine;
using System.Collections;

public class Invader : MonoBehaviour {

	// Use this for initialization
	void Start() {
	
	}
	
	// Update is called once per frame
	void Update() {
	
	}

	public void hit() {
		Destroy(gameObject);
	}
}
