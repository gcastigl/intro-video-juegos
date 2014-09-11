using UnityEngine;
using System.Collections;

public class TerrainDestinationScript : MonoBehaviour {

	public GameObject trigger;

	void Start () {
	
	}
	
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("LPMM!!");
	}
}
