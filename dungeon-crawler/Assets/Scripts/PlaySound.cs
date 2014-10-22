using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour {

	void OnTriggerEnter(Collider otherObj){
		if (!audio.isPlaying) {
			audio.Play ();
		}
	}

}
