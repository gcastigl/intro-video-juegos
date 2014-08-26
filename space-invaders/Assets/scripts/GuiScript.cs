using UnityEngine;
using System.Collections;

public class GuiScript : MonoBehaviour {

	public GameObject lifeLeftPrefav;
	public GameObject gameManagerObject;

	private GameManager gameManager;
	private GameObject[] lifesLeftPrefav;

	// Use this for initialization
	void Start () {
		gameManager = gameManagerObject.GetComponent<GameManager>();
		lifesLeftPrefav = new GameObject[gameManager.lives];
		for (int i = 0; i < gameManager.lives; i++) {
			Vector3 position = new Vector3(7 + 1.3f * i, 4.5f, 0);
			GameObject lifeLeft = Object.Instantiate(lifeLeftPrefav, position, Quaternion.identity) as GameObject;
			lifesLeftPrefav[i] = lifeLeft;
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
	
}
