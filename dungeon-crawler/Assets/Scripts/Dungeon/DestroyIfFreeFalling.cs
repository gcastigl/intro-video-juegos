using UnityEngine;
using System.Collections;

public class DestroyIfFreeFalling : MonoBehaviour {
	
	void Start () {
		Destroy (this, 10);
	}
	
	void Update () {
		if (transform.position.y < -50) {
			Debug.Log(gameObject.name + " is falling. Destroyed!");
			Destroy(gameObject);
		}
	}
}
