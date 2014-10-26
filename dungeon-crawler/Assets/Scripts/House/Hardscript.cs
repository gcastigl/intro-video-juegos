using UnityEngine;
using System.Collections;

public class Hardscript : MonoBehaviour {

	void OnTriggerEnter(Collider otherObj)
	{
		PlayerPrefs.SetInt ("Difficulty", 0);
	}
}
