using UnityEngine;
using System.Collections;

public class ShowPlayerStatus : MonoBehaviour {

	public GUIText timeLeftText;
	public int smallFont, bigFont;
	public Color lowBatteryColor, highBatteryColor;

	private DungeonManager dungeonManager;

	void Start () {
		dungeonManager = GameObject.FindGameObjectWithTag ("DungeonManager").GetComponent<DungeonManager>();
	}

	void Update () {
		FlashLight flashLight = dungeonManager.getPlayer ().flashLight;
		float time = flashLight.timeLeft;
		int min = (int) (time / 60);
		int seconds = (int) (time - min * 60);
		timeLeftText.text = min + " : " + (seconds < 10 ? "0" : "") + seconds;
		if (min == 0) {
			timeLeftText.color = lowBatteryColor;
			timeLeftText.fontSize = bigFont;
		} else {
			timeLeftText.color = highBatteryColor;
			timeLeftText.fontSize = smallFont;
		}
	}
}
