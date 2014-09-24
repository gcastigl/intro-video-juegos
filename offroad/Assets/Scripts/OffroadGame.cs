using UnityEngine;
using System.Collections;

public class OffroadGame : MonoBehaviour {

	private bool lost;
	private bool beaten;
	public bool paused;

	void Start () {
		paused = false;
	}

	void Update () {
		/*
		if (Input.GetKeyUp("p")) {
			paused = !paused;
			Time.timeScale = paused ? 0 : 1;
		}
		if (paused) {
			// GUIContent content = new GUIContent();

		}
		*/
		// Debug.Log (paused);
	}

	void OnGUI() {
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
