using UnityEngine;
using System.Collections;

public class MushroomScript : MonoBehaviour {

	public float distanceToTurnOff = 5;
	public Light light;

	private GameObject player;
	private float lightMaxIntensity;

	void Start() {
		lightMaxIntensity = light.intensity;
	}

	void Update () {
		loadPlayer ();
		if (Vector3.Distance (player.transform.position, transform.position) < distanceToTurnOff) {
			if (light.intensity > 0) {
				light.intensity -= Time.deltaTime * 2;
			}
		} else {
			if (light.intensity < lightMaxIntensity) {
				light.intensity += Time.deltaTime;
			}
		}
}


	private void loadPlayer() {
		if (player == null) {
			player = GameObject.FindGameObjectWithTag("Player");
		}
	}
}
