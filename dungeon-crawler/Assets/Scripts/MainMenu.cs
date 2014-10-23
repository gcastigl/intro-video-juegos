using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	public GameObject helpMenu;

	void Start () {
	
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.H)) {
			helpMenu.SetActive(!helpMenu.activeInHierarchy);
		}
	}
}
