using UnityEngine;
using System.Collections;

public class TimeDelay : MonoBehaviour {

	public float amount;
	public float count;

	// Use this for initialization
	void Start () {
		resetCounter();
	}
	
	// Update is called once per frame
	void Update () {
		if (count > 0) {
			count -= Time.deltaTime;
		}
	}

	public bool isReady() {
		return count <= 0;
	}

	public void resetCounter() {
		count = amount;
	}
}
