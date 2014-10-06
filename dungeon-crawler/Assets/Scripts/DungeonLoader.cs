using UnityEngine;
using System.IO;

public class DungeonLoader : MonoBehaviour {

	public GameObject[] items;
	public GameObject itemNode;
	public TextAsset itemsAsset;

	public GameObject[] spawns;
	public GameObject spawnsNode;
	public TextAsset spawnsAsset;

	void Start () {
		load(itemsAsset, items, itemNode);
		load(spawnsAsset, spawns, spawnsNode);
		Destroy (this);
	}

	private void load(TextAsset asset, GameObject[] objects, GameObject node) {
		string[] words = asset.text.Split('\n');
		foreach (string line in words) {
			string[] values = line.Split(' ');
			int x = int.Parse(values[1]);
			int z = int.Parse(values[2]);
			int type = int.Parse(values[3]);
			GameObject item = Object.Instantiate(objects[type]) as GameObject;
			item.name = values[0];
			item.transform.parent = node.transform;
			item.transform.position = new Vector3(x, 1, z);
		}
	}

	void Update () {
	
	}
}
