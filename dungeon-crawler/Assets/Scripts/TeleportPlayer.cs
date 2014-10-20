using UnityEngine;
using System.Collections;

public class TeleportPlayer : MonoBehaviour {

	public GameObject roomStartingPosition;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider otherObj)
	{
		otherObj.transform.position = roomStartingPosition.transform.position;
	}
}
