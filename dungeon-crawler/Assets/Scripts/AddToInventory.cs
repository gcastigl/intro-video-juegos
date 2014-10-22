using UnityEngine;
using System.Collections;

public class AddToInventory : MonoBehaviour {

	void OnTriggerEnter(Collider otherObj){
		audio.Play();
		this.gameObject.renderer.enabled = false;
	}
}
