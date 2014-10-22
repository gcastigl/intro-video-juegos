using UnityEngine;
using System.Collections;

public class AddToInventory : MonoBehaviour {

	void OnTriggerEnter(Collider otherObj){
		if (audio != null) {
			audio.Play();
		}
		this.gameObject.renderer.enabled = false;
	}
}
