using UnityEngine;
using System.Collections;

public class ExitDoorScript : MonoBehaviour {

	public DungeonManager dungeonManager;

	void Start() {
		dungeonManager = GameObject.FindGameObjectWithTag ("DungeonManager").GetComponent<DungeonManager>();
	}

	void OnTriggerEnter(Collider otherObj) {
		if (otherObj.gameObject.tag == dungeonManager.getPlayerGO().tag) {
			dungeonManager.dungeonStatus = DungeonManager.STATUS_BEATEN;
			otherObj.gameObject.GetComponent<Player>().disableMovement();
		}
	}
}
