using UnityEngine;
using System.Collections;

public class MushroomScript : MonoBehaviour {

	public float distanceToTurnOff = 5;
	public Light glowLight;

	private DungeonManager dungeonManager;
	private float lightMaxIntensity;

	void Start() {
		dungeonManager = GameObject.FindGameObjectWithTag ("DungeonManager").GetComponent<DungeonManager>();
		lightMaxIntensity = glowLight.intensity;
	}

	void Update () {
		Player player = dungeonManager.getPlayer ();
		if (Vector3.Distance (player.transform.position, transform.position) < distanceToTurnOff) {
			if (glowLight.intensity > 0) {
				glowLight.intensity -= Time.deltaTime * 2;
			}
		} else {
			if (glowLight.intensity < lightMaxIntensity) {
				glowLight.intensity += Time.deltaTime;
			}
		}
	}
}
