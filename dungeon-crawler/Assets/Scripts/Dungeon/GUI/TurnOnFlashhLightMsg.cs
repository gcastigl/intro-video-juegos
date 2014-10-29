using UnityEngine;
using System.Collections;

public class TurnOnFlashhLightMsg : MonoBehaviour {

	public float startTime;
	public GUIText guiText;
	private float elapsed;
	private DungeonManager dungeonManager;

	void Start () {
		elapsed = 0;
		Color c = guiText.color;
		guiText.color = new Color(c.r, c.g, c.b, 0);
		dungeonManager = GameObject.FindGameObjectWithTag ("DungeonManager").GetComponent<DungeonManager>();
	}
	
	void Update () {
		elapsed += Time.deltaTime;
		if (startTime < elapsed) {
			Color c = guiText.color;
			float a = c.a + Time.deltaTime * 2;
			guiText.color = new Color(c.r, c.g, c.b, a);
			guiText.enabled = true;
			guiText.text = "So dark in here... Maybe I should turn the flashlight on";
		}
		if (1 < elapsed && dungeonManager.getPlayer().flashLight.isOn()) {
			guiText.enabled = false;
			Destroy(this);
		}
	}
}
