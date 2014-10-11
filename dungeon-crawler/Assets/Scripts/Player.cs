using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject torchPrefab;

	public int torchesLeft;

	void Start () {
	
	}
	
	void Update () {
		if (torchesLeft > 0 && Input.GetKeyUp(KeyCode.E)) {
			GameObject torch = Object.Instantiate(torchPrefab) as GameObject;
			Vector3 playerPos = gameObject.transform.position;
			torch.transform.position = new Vector3(playerPos.x, 0, playerPos.z);
			torchesLeft--;
		}
	}
}
