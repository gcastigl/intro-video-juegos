﻿using UnityEngine;
using System.Collections;

public class DungeonDecorator {

	private BuildDungeonConfig _buildConfig;

	public DungeonDecorator(BuildDungeonConfig buildConfig) {
		_buildConfig = buildConfig;
	}

	public void Decorate(GameObject dungeonGO, Dungeon dungeon) {
		GameObject decorations = new GameObject("decorations");
		decorations.transform.parent = dungeonGO.transform;
		for (int i = 0; i < 10; i++) {
			int index = Random.Range(0, _buildConfig.decorations.Length);
			GameObject prefab = _buildConfig.decorations[index];
			bool placed = false;
			do {
				int row = Random.Range(1, _buildConfig.heightmapResolution - 1);
				int col = Random.Range(1, _buildConfig.heightmapResolution - 1);
				if (dungeon.accesibles[row, col] && dungeon.countNeighborsMatching(row, col, 0) == 8) {
					float y = dungeon.heightsWithNoise[row, col] + 0.3f;
					GameObject decoration = Object.Instantiate(prefab, dungeon.worldPosition(row, col, y), Quaternion.identity) as GameObject;
					decoration.transform.parent = decorations.transform;
					placed = true;
					dungeon.decorations.Add(new Decoration(new Vector2(row, col)));
				}
			} while(!placed);
		}
		for (int i = 0; i < 15; i++) {
			int index = Random.Range(0, _buildConfig.rocks.Length);
			bool placed = false;
			do {
				int row = Random.Range(1, _buildConfig.heightmapResolution - 1);
				int col = Random.Range(1, _buildConfig.heightmapResolution - 1);
				if (dungeon.accesibles[row, col] && dungeon.countNeighborsMatching(row, col, 0) == 8) {
					float y = 8;
					GameObject rock = Object.Instantiate(_buildConfig.rocks[index], dungeon.worldPosition(row, col, y), Quaternion.identity) as GameObject;
					rock.transform.parent = decorations.transform;
					placed = true;
				}
			} while(!placed);
		}
	}
}
