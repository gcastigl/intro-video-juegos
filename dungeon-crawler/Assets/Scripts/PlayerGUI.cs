using UnityEngine;
using System.Collections;

public class PlayerGUI : MonoBehaviour {

	public Player player;

	private GUIText text;

	void Start () {
		text = GetComponent<GUIText>();
	}

	void Update () {
		if (player.torchesLeft == 0) {
			text.color = Color.red;
			text.fontStyle = FontStyle.Bold;
		}
		text.text = "Torches: " + player.torchesLeft;
	}
}
