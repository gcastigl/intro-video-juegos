using UnityEngine;
using System.Collections;

public class TimerCountDown : MonoBehaviour {

	public Font font;
	public OffroadGame game;
	public int fontSizeWarning;

	private GUIText text;

	void Start () {
		text = GetComponent<GUIText> ();
	}
	
	// Update is called once per frame
	void OnGUI () {
		if (!game.isEndOfgame()) {
			int seconds = (int) game.remaningSeconds;
			if (seconds <= 10) {
				text.fontSize = fontSizeWarning;
				text.color = Color.red;
			}
			text.text = seconds + "";
		}
	}
}
