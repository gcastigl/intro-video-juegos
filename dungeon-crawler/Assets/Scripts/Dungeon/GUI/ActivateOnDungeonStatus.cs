using UnityEngine;
using System.Collections;

public class ActivateOnDungeonStatus : MonoBehaviour {

	public DungeonManager dungeonManager;
	public GameObject[] gos;
	public int statusToActivate;

	void Update () {
		if (dungeonManager.dungeonStatus == statusToActivate) {
			foreach (GameObject go in gos) {
				go.SetActive(!go.activeSelf);
			}
			Destroy(this);
		}
	}
}
