using UnityEngine;
using System.Collections;

public class OffroadGame : MonoBehaviour {

	private bool lost;
	private bool beaten;
	public float remaningSeconds = 60 * 1000;

	void Start () {
	}

	void Update () {
		remaningSeconds -= Time.deltaTime;
	}

	void OnGUI() {
		int seconds = (int) remaningSeconds;
		GUI.Label(new Rect(Screen.width - 120, 10, 100, 20), "Tiempo: " + seconds + " [Seg]");
		if (beaten) {
			GUI.Label(new Rect(10, 10, 100, 20), "Ganaste!!");
		} else if (lost) {
			GUI.Label(new Rect(10, 10, 100, 20), "Perdiste =(");
		}
	}

	public void setBeaten() {
		beaten = true;
	}

	public void setLost() {
		beaten = true;
	}

}
