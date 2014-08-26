using UnityEngine;
using System.Collections;

public class SpaceInvadersLogo : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnDestroy(){
		Debug.Log ("hola");
		Application.LoadLevel(1);
	}
}
