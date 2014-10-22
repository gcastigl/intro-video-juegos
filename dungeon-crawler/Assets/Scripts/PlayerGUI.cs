using UnityEngine;
using System.Collections;

public class PlayerGUI : MonoBehaviour {
	
	public GUIText torchesLeftText;
	public GUIText torchTimeoutText;

	private Player player;

	void Update () {
		loadPlayer ();
		if (player.torchesLeft == 0) {
			torchesLeftText.color = Color.red;
			torchesLeftText.fontStyle = FontStyle.Bold;
		}
		torchesLeftText.text = "Torches: " + player.torchesLeft;
		TorcheLightTimeout torchTimeout = player.getLightTimeout();
		int totalSeconds = (torchTimeout == null) ? 0 : (int) torchTimeout.timeout;
		int minutesLeft = totalSeconds / 60;
		int secondsLeft = totalSeconds - minutesLeft * 60;
		torchTimeoutText.text = minutesLeft + " : " + (secondsLeft < 10 ? "0" : "") + secondsLeft;
	}

	private void loadPlayer() {
		if (player == null) {
			player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		}
	}
}
