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
		float[,] heights = new BuildDungeon(buildConfig, texture, ceilMaterial).Build(gameObject);
		// new PopulateDungeon ().Populate (gameObject, heights);
		player.SetActive (true);
	}
	
	public void spawnDestroyed() {

	}

	void Update () {
	
	}
}
