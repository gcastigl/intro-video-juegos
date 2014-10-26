using UnityEngine;
using System.Collections;

public class EasyScript : MonoBehaviour {

	void OnTriggerEnter(Collider otherObj)
	{
		PlayerPrefs.SetInt ("Difficulty", 1);
	}
}
