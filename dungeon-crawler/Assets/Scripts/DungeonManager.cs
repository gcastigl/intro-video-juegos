using UnityEngine;
using System.IO;

public class DungeonManager : MonoBehaviour {

	public GameObject player;
	public Texture2D texture;
	public Material ceilMaterial;

	public BuildDungeonConfig buildConfig;
	
	public GameObject[] enemies;
	public GameObject[] items;

	void Start () {
		Dungeon dungeon = new BuildDungeon(buildConfig, texture, ceilMaterial).Build(gameObject);
		new PopulateDungeon(enemies).Populate(gameObject, dungeon);
		if (!dungeon.valid) {
			Debug.Log ("Re hacer nivel");
			return;
		}
		player.transform.position = new Vector3(dungeon.playerCol, 1, dungeon.playerRow);
		player.SetActive (true);
	}


	void Update () {
	
	}
}
