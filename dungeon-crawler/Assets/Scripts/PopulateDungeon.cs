using UnityEngine;
using System.Collections;

public class PopulateDungeon {

	private BuildDungeonConfig config;

	public PopulateDungeon(BuildDungeonConfig config) {
		this.config = config;
	}

	public void Populate(GameObject dungeonGO, Dungeon dungeon) {
		evaluateAndSetStartPosition(dungeon);
		if (dungeon.valid) {
			Debug.Log ("El nivel parece bueno");
			placeTreasures(dungeonGO, dungeon);
			placeEnemies(dungeonGO, dungeon);
		} else {
			Debug.Log ("El nivel no es muy accesible");
		}
	}

	private void evaluateAndSetStartPosition(Dungeon dungeon) {
		dungeon.valid = false;
		bool playerPosFound = false;
		for (int row = 0; row < dungeon.rowsCount() && !playerPosFound; row++) {
			for (int col = 0; col < dungeon.columnCount() && !playerPosFound; col++) {
				if (dungeon.value(row, col) == 0 && dungeon.countNeighborsMatching(row, col, 0) == 8) {
					int[,] heightsCopy = dungeon.heights.Clone() as int[,];
					int maxFlood = (dungeon.rowsCount() - 2) * (dungeon.columnCount() - 2);
					int flood = floodFill(heightsCopy, row, col, 0, -1, dungeon.accesibles);
					float p = flood / (float) maxFlood;
					dungeon.valid = p > 0.5;
					if (dungeon.valid) {
						Debug.Log("Area accesible: " + (p * dungeon.rowsCount() * dungeon.columnCount()));
						dungeon.playerRow = dungeon.rowToWorld(row);
						dungeon.playerCol = dungeon.columnToWorld(col);
						playerPosFound = true;
					}
				}
			}
		}
	}
	
	private int floodFill(int[,] heights, int row, int col, int target, int replacement, bool[,] accesibles) {
		if (row < 0 || col < 0 || row >= heights.GetLength(0) || col >= heights.GetLength(1)) {
			return 0;
		}
		if (target == replacement) {
			return 0;
		}
		float value = heights[row, col];
		if (value != target) {
			return 0;
		}
		heights[row, col] = replacement;
		accesibles[row, col] = true;
		int sum = 1;
		sum += floodFill(heights, row + 1, col, target, replacement, accesibles);
		sum += floodFill(heights, row, col + 1, target, replacement, accesibles);
		sum += floodFill(heights, row - 1, col, target, replacement, accesibles);
		sum += floodFill(heights, row, col - 1, target, replacement, accesibles);
		return sum;
	}

	private void placeTreasures(GameObject dungeonGO, Dungeon dungeon) {
		GameObject treasuresGO = new GameObject("treasures");
		treasuresGO.transform.parent = dungeonGO.transform;
		int i = 0;
		int tries = 0;
		while(i < config.treasuresAmount && tries < 600) {
			int row = (int) (Random.value * (dungeon.rowsCount() - 3) + 2);
			int col = (int) (Random.value * (dungeon.columnCount() - 3) + 2);
			if (dungeon.accesibles[row, col] && dungeon.countNeighborsMatching(row, col, 1) >= 5) {
				GameObject treasure = config.treasures[(int) (Random.value * config.treasures.Length)];
				float x = dungeon.columnToWorld(col);
				float z = dungeon.rowToWorld(row);
				GameObject treasureGO = Object.Instantiate(treasure, new Vector3(x, 1, z), Quaternion.identity) as GameObject;
				treasureGO.AddComponent<DestroyIfFreeFalling>();
				treasureGO.transform.parent = treasuresGO.transform;
				dungeon.accesibles[row, col] = false;
				i++;
			}
			tries++;
		}
	}

	private void placeEnemies(GameObject dungeonGO, Dungeon dungeon) {
		GameObject enemiesGO = new GameObject("enemies");
		enemiesGO.transform.parent = dungeonGO.transform;
		int i = 0;
		int tries = 0;
		while(i < config.enemiesAmount && tries < 300) {
			int row = (int) (Random.value * (dungeon.rowsCount() - 3) + 2);
			int col = (int) (Random.value * (dungeon.columnCount() - 3) + 2);
			if (dungeon.accesibles[row, col] && dungeon.countNeighborsMatching(row, col, 0) == 8) {
				GameObject enemy = config.enemies[(int) (Random.value * config.enemies.Length)];
				float x = dungeon.columnToWorld(col);
				float z = dungeon.rowToWorld(row);
				GameObject enemyGO = Object.Instantiate(enemy, new Vector3(x, 2f, z), Quaternion.identity) as GameObject;
				enemyGO.transform.parent = enemiesGO.transform;
				i++;
			}
			tries++;
		}
	}
}
