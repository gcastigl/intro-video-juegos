﻿using UnityEngine;
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
		GameObject shapeGo = new GameObject("shape");
		shapeGo.transform.parent = transform;
		Dungeon dungeon = new BuildDungeon(buildConfig, floorNaturalMaterial, floorTexture, floorTextureNormal, ceilMaterial).Build(shapeGo);
		new PopulateDungeon(buildConfig, entranceDoor).Populate(gameObject, dungeon);
		if (!dungeon.valid) {
			Debug.Log ("Re hacer nivel");
			return;
		}
		player.transform.position = new Vector3(dungeon.columnToWorld(dungeon.playerCol), 2, dungeon.rowToWorld(dungeon.playerRow));
		player.SetActive (true);
		drawDungeonOnMap.OnDungeonCreated(dungeon);
	}

}
