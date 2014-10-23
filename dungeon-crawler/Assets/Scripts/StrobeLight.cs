using UnityEngine;
using System.Collections;

public class StrobeLight : MonoBehaviour {

	public float time = .5f; //time between on and off
	
	void Start () {
		StartCoroutine("Flicker");
	}

	IEnumerator Flicker(){
		while(true){
			light.enabled = false;
			yield return new WaitForSeconds(time);
			light.enabled = true;
			yield return new WaitForSeconds(time);
		}
	}
}
