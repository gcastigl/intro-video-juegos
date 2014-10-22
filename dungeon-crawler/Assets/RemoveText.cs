using UnityEngine;
using System.Collections;

public class RemoveText : MonoBehaviour {
	public GameObject text1;
	public GameObject text2;

	void OnTriggerEnter(Collider otherObj)
	{
		text1.SetActive(false);
		text2.SetActive(false);
	}
}
