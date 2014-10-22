using UnityEngine;
using System.IO;

public class DungeonManager : MonoBehaviour {

	public GameObject playerPrefab;
	public GameObject entranceDoor;
	public Material floorNaturalMaterial;
	public Texture2D floorTexture;
	public Texture2D floorTextureNormal;
	public Material ceilMaterial;

	private Dungeon dungeon;

	public BuildDungeonConfig buildConfig;

	void Start () {
		Debug.Log (Random.seed);
		do {
			GameObject shapeGo = new GameObject("shape");
			shapeGo.transform.parent = transform;
			dungeon = new BuildDungeon(buildConfig, floorNaturalMaterial, floorTexture, floorTextureNormal, ceilMaterial).Build(shapeGo);
			new PopulateDungeon(buildConfig, entranceDoor).Populate(gameObject, dungeon);
			if (!dungeon.valid) {
				Debug.Log("Nivel generado invalido. Generando nuevamente");
				buildConfig.seed++;
				shapeGo.SetActive(false);
			}
		} while (!dungeon.valid);
		GameObject player = Object.Instantiate(playerPrefab) as GameObject;
		player.transform.position = dungeon.worldPosition(dungeon.playerRow, dungeon.playerCol, buildConfig.height);
		player.SetActive (true);
	}

	public Dungeon getDungeon() {
		return dungeon;
	}
}
