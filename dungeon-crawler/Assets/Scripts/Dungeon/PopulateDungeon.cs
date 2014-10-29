using UnityEngine;
using System.Collections;

public class PopulateDungeon {

	private BuildDungeonConfig config;
	private GameObject entranceDoor;

	public PopulateDungeon(BuildDungeonConfig config, GameObject entranceDoor) {
		this.config = config;
		this.entranceDoor = entranceDoor;
	}

	public void Populate(GameObject dungeonGO, Dungeon dungeon) {
		evaluateAndSetStartPosition(dungeon);
		if (dungeon.valid) {
			Debug.Log ("El nivel parece bueno");
			placeEntranceDoor(dungeonGO, dungeon);
			placeTreasures(dungeonGO, dungeon);
			placeEnemies(dungeonGO, dungeon);
		} else {
			Debug.Log ("El nivel no es muy accesible");
		}
	}

	private void evaluateAndSetStartPosition(Dungeon dungeon) {
		dungeon.valid = false;
		bool playerPosFound = false;
		bool[,] flooded = new bool[dungeon.rowsCount(), dungeon.columnCount()];
		int maxFlood = (dungeon.rowsCount() - 2) * (dungeon.columnCount() - 2);
		for (int row = 0; row < dungeon.rowsCount() && !playerPosFound; row++) {
			for (int col = 0; col < dungeon.columnCount() && !playerPosFound; col++) {
				if (dungeon.value(row, col) == 0 && !flooded[row, col] && dungeon.countNeighborsMatching(row, col, 0) == 8) {
					int[,] heightsCopy = dungeon.heights.Clone() as int[,];
					bool[,] accesiblesFromPosition = dungeon.accesibles.Clone() as bool[,];
					int flood = floodFill(heightsCopy, row, col, 0, -1, accesiblesFromPosition);
					markAll(accesiblesFromPosition, flooded);
					float p = flood / (float) maxFlood;
					dungeon.valid = p > 0.4;
					if (dungeon.valid) {
						dungeon.accesibles = accesiblesFromPosition;
						Debug.Log("P. accesible: " + p);
						Debug.Log("Area accesible: " + (p * dungeon.rowsCount() * dungeon.columnCount()));
						dungeon.playerRow = row;
						dungeon.playerCol = col;
						playerPosFound = true;
					}
				}
			}
		}
	}

	private void markAll(bool[,] accesiblesFromPosition, bool[,] flooded) {
		for (int row = 0; row < accesiblesFromPosition.GetLength(0); row++) {
			for (int col = 0; col < accesiblesFromPosition.GetLength(1); col++) {
				if (accesiblesFromPosition[row, col]) {
					flooded[row, col] = accesiblesFromPosition[row, col];
				}
			}
		}
	}

	private void placeEntranceDoor(GameObject dungeonGO, Dungeon dungeon) {
		int col;
		int row;
		bool placeFound;
		Vector2 playerPos = new Vector2 (dungeon.playerRow, dungeon.playerCol);
		int tries = 0;
		do { 
			row = (int) (Random.Range(2, dungeon.rowsCount() - 2));
			col = (int) (Random.Range(2, dungeon.columnCount() - 2));
			placeFound = dungeon.accesibles[row, col] 
				&& dungeon.countNeighborsMatching(row, col, 0) == 8
				&& Vector2.Distance(new Vector2(row, col), playerPos) > config.heightmapResolution * 0.8f;
			tries++;
		} while (!placeFound);
		dungeon.doorRow = row;
		dungeon.doorCol = col;
		Vector3 position = dungeon.worldPosition(row, col, 0);
		GameObject door = Object.Instantiate(entranceDoor, position, Quaternion.identity) as GameObject;
		door.transform.parent = dungeonGO.transform;
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
		float minDistanceToPlayerSq = 15 * 15;
		float maxTries = 1000;
		while(i < config.treasuresAmount && tries < maxTries) {
			int row = (int) (Random.value * (dungeon.rowsCount() - 3) + 2);
			int col = (int) (Random.value * (dungeon.columnCount() - 3) + 2);
			if (dungeon.accesibles[row, col]
					&& dungeon.distanceSq(row, col, dungeon.playerRow, dungeon.playerCol) > minDistanceToPlayerSq
			    	&& dungeon.countNeighborsMatching(row, col, 0) >= 6		// XXX: varios pisos a su alreadedor
			    	&& countNeighborsNUnitsApart(dungeon, row, col, 3, 1) >= 15) {	// XXX: Pero muchas paredes cerca...
				GameObject treasure = config.treasures[(int) (Random.value * config.treasures.Length)];
				treasure.name = "treasure_" + i;
				GameObject treasureGO = Object.Instantiate(treasure, dungeon.worldPosition(row, col, 8), Quaternion.identity) as GameObject;
				treasureGO.AddComponent<DestroyIfFreeFalling>();
				treasureGO.transform.parent = treasuresGO.transform;
				treasure.transform.localRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
				dungeon.accesibles[row, col] = false;
				dungeon.treasures.Add(new Treasure(new Vector2(row, col), false));
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
		float minDistanceToPlayerSq = 25 * 25;
		float maxTries = 300;
		while(i < config.enemiesAmount && tries < maxTries) {
			int row = (int) (Random.value * (dungeon.rowsCount() - 3) + 2);
			int col = (int) (Random.value * (dungeon.columnCount() - 3) + 2);
			if (dungeon.accesibles[row, col] 
			    	&& dungeon.distanceSq(row, col, dungeon.playerRow, dungeon.playerCol) > minDistanceToPlayerSq
			    	&& dungeon.countNeighborsMatching(row, col, 0) == 8) {
				GameObject enemy = config.enemies[(int) (Random.value * config.enemies.Length)];
				enemy.name = "enemy_" + i;
				GameObject enemyGO = Object.Instantiate(enemy, dungeon.worldPosition(row, col, 8), Quaternion.identity) as GameObject;
				enemyGO.transform.parent = enemiesGO.transform;
				i++;
			}
			tries++;
		}
	}

	private int countNeighborsNUnitsApart(Dungeon dungeon, int row, int col, int n, int value) {
		int count = 0;
		for (int rowi = -n; rowi <= n; rowi++) {
			for (int colj = -n; colj <= n; colj++) {
				if (rowi == -n || rowi == n || colj == -n || colj == n) {
					int nrow = row + rowi;
					int ncol = col + colj;
					if (!dungeon.validPosition(nrow, ncol) || dungeon.value(nrow, ncol) == value) {
							count++;
					}
				}
			}
		}
		return count;
	}
}
