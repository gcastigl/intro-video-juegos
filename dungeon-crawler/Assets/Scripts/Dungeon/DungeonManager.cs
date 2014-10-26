using UnityEngine;
using System.IO;

public class DungeonManager : MonoBehaviour {

	public const int STATUS_LOST = -1;
	public const int STATUS_UNSOLVED = 0;
	public const int STATUS_BEATEN = 1;

	public GameObject playerPrefab;
	public GameObject entranceDoor;
	public Material floorNaturalMaterial;
	public Texture2D floorTexture;
	public Texture2D floorTextureNormal;
	public Material ceilMaterial;
	public BuildDungeonConfig buildConfig;

	public int dungeonStatus = STATUS_UNSOLVED;
	private Dungeon dungeon;
	private GameObject playerGO;
	private Player player;

	void Start () {
		Debug.Log (Random.seed);
		if(PlayerPrefs.GetInt("dificulty") == 0){
			buildConfig.enemiesAmount = 6;
			buildConfig.treasuresAmount = 3;
		}
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
		new DungeonDecorator (buildConfig).Decorate(gameObject, dungeon);
		playerGO = Object.Instantiate(playerPrefab) as GameObject;
		playerGO.transform.position = dungeon.worldPosition(dungeon.playerRow, dungeon.playerCol, buildConfig.height);
		player = playerGO.GetComponent<Player> ();
	}

	public Dungeon getDungeon() {
		return dungeon;
	}

	public GameObject getPlayerGO() {
		return playerGO;
	}

	public Player getPlayer() {
		return player;
	}
}
