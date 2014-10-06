using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject prefab;
	public int amount;
	public int id;

	void Start () {
		for (int i = 0; i < amount; i++) {
			GameObject spawn = Object.Instantiate(prefab) as GameObject;
			spawn.transform.parent = transform;
			spawn.transform.position = transform.position;
		}
	}
	
	void Update () {
	
	}
}
