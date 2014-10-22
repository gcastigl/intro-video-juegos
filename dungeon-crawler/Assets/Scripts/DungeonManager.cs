using UnityEngine;
using System.IO;

public class DungeonManager : MonoBehaviour {

	public GameObject player;
	public GameObject entranceDoor;
	public DrawDungeonOnMap drawDungeonOnMap;
	public Material floorNaturalMaterial;
	public Texture2D floorTexture;
	public Texture2D floorTextureNormal;
	public Material ceilMaterial;

	public BuildDungeonConfig buildConfig;

	void Start () {
		Debug.Log (Random.seed);
		Dungeon dungeon;
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
		player.transform.position = dungeon.worldPosition(dungeon.playerRow, dungeon.playerCol, buildConfig.height);
		player.SetActive (true);
		drawDungeonOnMap.OnDungeonCreated(dungeon);
	}

}
