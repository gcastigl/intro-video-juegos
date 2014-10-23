using UnityEngine;
using System.Collections;

public class WinScript : MonoBehaviour {

	void OnTriggerEnter(Collider otherObj) {
		if (otherObj.gameObject.tag == "Player") {
			GameObject winScreen = GameObject.FindGameObjectWithTag("WinScreen");
			winScreen.GetComponentInChildren<GUIText>().enabled = true;
			winScreen.GetComponentInChildren<GUITexture>().enabled = true;
			otherObj.gameObject.GetComponent<Player>().disableMovement();
		}
	}
}
