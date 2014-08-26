using UnityEngine;
using System.Collections;

public class GuiScript : MonoBehaviour {

	public float xOffset;
	public float yOffset;
	public GameObject lifeLeftPrefav;
	public GameObject gameManagerObject;

	private GameManager gameManager;
	private GameObject[] lifesLeftPrefavs;
	
	void Start () {
		gameManager = gameManagerObject.GetComponent<GameManager>();
		lifesLeftPrefavs = new GameObject[gameManager.lives];
		for (int i = 0; i < gameManager.lives; i++) {
			Vector3 position = new Vector3(xOffset + i, yOffset, transform.position.z - 1);
			GameObject lifeLeft = Object.Instantiate(lifeLeftPrefav, position, Quaternion.identity) as GameObject;
			lifesLeftPrefavs[i] = lifeLeft;
		}
	}

	void Update () {
		int livesDiffs = lifesLeftPrefavs.Length - gameManager.lives;
		if (livesDiffs > 0) {
			for (int i = 0; i < livesDiffs; i++) {
				Destroy(lifesLeftPrefavs[i]);
			}
			GameObject[] updatedLifesLeft = new GameObject[gameManager.lives];
			for (int j = 0; j < gameManager.lives; j++) {
				updatedLifesLeft[j] = lifesLeftPrefavs[j + livesDiffs];
			}
			lifesLeftPrefavs = updatedLifesLeft;
		}
	}
	
}
