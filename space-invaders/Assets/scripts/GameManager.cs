using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject defenderPrefab;
	public GameObject middleGround;
	public int lives;
	public GameObject invaders;
	public Vector3 startLocation;

	private GameObject defender = null;

	void Start () {
		newDefender();
	}

	void Update () {
		if (defender == null) {
			Debug.Log("Respawn");
			if (lives > 0) {
				lives--;
				newDefender();
				invaders.GetComponent<TimeDelay>().resetCounter();
			} else {
				Debug.Log("perdiste el juego!");
				// Application.LoadLevel("pepe");
			}
		} else if (invaders == null) {
			Debug.Log("Ganaster el juego!");
		}
	}

	private void newDefender() {
		defender = Object.Instantiate(defenderPrefab, middleGround.transform.position + startLocation, Quaternion.identity) as GameObject;
		defender.transform.parent = middleGround.transform;
	}
}
