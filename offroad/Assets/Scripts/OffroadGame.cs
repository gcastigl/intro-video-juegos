using UnityEngine;
using System.Collections;

public class OffroadGame : MonoBehaviour {

	public Vector3 startLocation;
	public GameObject[] carPrefabs;
	public float remaningSeconds = 60;

	private bool lost;
	private bool beaten;

	void Start () {
		int carType = PlayerPrefs.GetInt("carType");
		GameObject car = GameObject.Instantiate(carPrefabs[carType - 1]) as GameObject;
		car.transform.position = startLocation;
	}

	void Update () {
		remaningSeconds -= Time.deltaTime;
		if (remaningSeconds <= 0) {
			setLost();
		}
	}

	void OnGUI() {
		if (beaten) {
			GUI.Label (new Rect (Screen.width / 2 - 50, Screen.height / 2, 100, 20), "A winner is you! =)");
		} else if (lost) {
			GUI.Label (new Rect (Screen.width / 2 - 110, Screen.height / 2, 220, 20), "Perdiste! Too hard for you?");
		} else {
			int seconds = (int) remaningSeconds;
			GUI.Label(new Rect(Screen.width - 120, 10, 100, 20), "Tiempo: " + seconds + " [Seg]");
		}
		if (isEndOfgame()) {
			GUI.Label (new Rect (Screen.width / 2 - 125, Screen.height / 2 + 20, 250, 20), "Enter para volver al menu principal");
			GUI.Label (new Rect (Screen.width / 2 - 50, Screen.height / 2 + 40, 100, 20), "R para reiniciar");
			if (Input.GetKey(KeyCode.Return)) {
				Application.LoadLevel("main-menu");
			}
			if (Input.GetKey("r")) {
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
