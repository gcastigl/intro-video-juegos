using UnityEngine;
using System.Collections;

public class TeleportPlayer : MonoBehaviour {

	public GameObject roomStartingPosition;

	void OnTriggerEnter(Collider otherObj)
	{
		otherObj.transform.position = roomStartingPosition.transform.position;
	}
}
