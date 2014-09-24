using UnityEngine;
using System.Collections;

public class TargetReached : MonoBehaviour {

	public OffroadGame game;

	void Start () {
		
	}

	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		game.setBeaten();
	}
}
