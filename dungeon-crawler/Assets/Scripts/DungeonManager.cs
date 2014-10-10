using UnityEngine;
using System.IO;

public class DungeonManager : MonoBehaviour {

	public GameObject player;
	public Texture2D texture;
	public Material ceilMaterial;

	public BuildDungeonConfig buildConfig;

	public GameObject[] items;
	public GameObject[] spawns;

	void Start () {
		Dungeon dungeon = new BuildDungeon(buildConfig, texture, ceilMaterial).Build(gameObject);
		new PopulateDungeon().Populate(gameObject, dungeon);
		if (!dungeon.valid) {
			Debug.Log ("Re hacer nivel");
			return;
		}
		player.transform.position = new Vector3(dungeon.playerRow, 1, dungeon.playerCol);
		player.SetActive (true);
	}


	void Update () {
	
	}
}
