using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour {

	private bool hasPlayed = false;

	void OnTriggerEnter(Collider otherObj){
		if (!audio.isPlaying && !hasPlayed) {
			audio.Play ();
			hasPlayed = true;
		}
	}

}
