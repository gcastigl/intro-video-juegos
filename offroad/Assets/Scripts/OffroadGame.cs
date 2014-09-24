using UnityEngine;
using System.Collections;

public class OffroadGame : MonoBehaviour {

	public Vector3 startLocation;
	public GameObject[] carPrefabs;
	public float remaningSeconds = 60;
	public int defaultChoice = 1;

	public GameObject winScreen;
	public GameObject lostScreen;

	private bool lost;
	private bool beaten;
	private GameObject player;
	
	void Start () {
		int carType = PlayerPrefs.GetInt("carType");
		if (carType == 0) {
			carType = defaultChoice;
		}
		player = GameObject.Instantiate(carPrefabs[carType - 1]) as GameObject;
		player.transform.position = startLocation;
	}

	void Update () {
		remaningSeconds -= Time.deltaTime;
		if (remaningSeconds <= 0) {
			setLost();
		}
	}

	void OnGUI() {
		if (beaten) {
			winScreen.SetActive(true);
		} else if (lost) {
			lostScreen.SetActive(true);
		}
		if (isEndOfgame()) {
			gameObject.GetComponent<AudioSource>().enabled = false;
			player.GetComponentInChildren<AudioListener>().enabled = false;
			player.GetComponentInChildren<UserCarController>().enabled = false;
			if (Input.GetKey(KeyCode.Return)) {
				Time.timeScale = 1;
				Application.LoadLevel("main-menu");
			}
			if (Input.GetKey("r")) {
				Time.timeScale = 1;
				Application.LoadLevel (Application.loadedLevelName);
			}
		}
	}

	public void setBeaten() {
		beaten = true;
		Time.timeScale = 0;
	}

	public void setLost() {
		lost = true;
		Time.timeScale = 0;
	}

	public bool isEndOfgame() {
		return beaten || lost;
	}
}
