using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject mapPrefab;

	private DungeonManager dungeonManager;
	private GameObject map;
	public bool alive;
	public Camera camera;
	public FlashLight flashLight;

	void Start () {
		alive = true;
		dungeonManager = GameObject.FindGameObjectWithTag ("DungeonManager").GetComponent<DungeonManager>();
		map = Object.Instantiate (mapPrefab) as GameObject;
	}

	void Update () {
		if (Input.GetKeyUp(KeyCode.Tab)) {
			map.SetActive(!map.activeSelf);
		}
		if (flashLight.timeLeft < 0) {
			dungeonManager.dungeonStatus = DungeonManager.STATUS_LOST;
		}
	}

	public void kill() {
		if (alive) {
			audio.Play();
			alive = false;
			dungeonManager.dungeonStatus = DungeonManager.STATUS_LOST;
			disableMovement();
		}
	}

	public void disableMovement() {
		enabled = false;
		GetComponent<MouseLook>().enabled = false;
		GetComponent<CharacterMotor>().enabled = false;
		camera.GetComponent<MouseLook>().enabled = false;
	}
}
