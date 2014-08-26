using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject defenderPrefab;
	public GameObject middleGround;
	public int lives;
	public GameObject invaders;
	public Vector3 startLocation;
	public GameObject gameOverLayer;

	private bool gameEnded;
	private GameObject defender = null;

	void Start () {
		newDefender();
	}

	void Update () {
		if (gameEnded) {
			if (Input.GetKey(KeyCode.R)) {
				Application.LoadLevel("mainmenu-scene");
			}
		} else {
			if (defender == null) {
				if (lives > 0) {
					lives--;
					newDefender();
					invaders.GetComponent<TimeDelay>().resetCounter();
				} else {
					SpriteRenderer render = gameOverLayer.GetComponent<SpriteRenderer>();
					if (!render.enabled) {
						render.enabled = true;
					}
					gameEnded = true;
				}
			} else if (invaders == null) {
				gameEnded = true;
				Debug.Log("Ganaster el juego!");
			}
		}
	}

	private void newDefender() {
		defender = Object.Instantiate(defenderPrefab, middleGround.transform.position + startLocation, Quaternion.identity) as GameObject;
		defender.transform.parent = middleGround.transform;
	}
}
