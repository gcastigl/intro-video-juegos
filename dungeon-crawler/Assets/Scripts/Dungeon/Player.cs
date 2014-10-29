using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject mapPrefab;

	private DungeonManager dungeonManager;
	private GameObject map;
	public bool alive;
	private AudioSource deathAudioSource;

	void Start () {
		alive = true;
		dungeonManager = GameObject.FindGameObjectWithTag ("DungeonManager").GetComponent<DungeonManager>();
		map = Object.Instantiate (mapPrefab) as GameObject;
		deathAudioSource = GetComponents<AudioSource>()[0];
	}

	void Update () {
		if (Input.GetKeyUp(KeyCode.Tab)) {
			map.SetActive(!map.activeSelf);
		}
	}

	public void kill() {
		if (alive) {
			alive = false;
			dungeonManager.dungeonStatus = DungeonManager.STATUS_LOST;
			deathAudioSource.Play();
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
