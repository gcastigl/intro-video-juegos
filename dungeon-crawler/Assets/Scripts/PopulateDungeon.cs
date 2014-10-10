using UnityEngine;
using System.Collections;

public class PopulateDungeon {
	
	public Dungeon Populate(GameObject dungeonGO, Dungeon dungeon) {
		dungeon.valid = false;
		float[,] heighs = dungeon.heights;
		bool playerPosFound = false;
		for (int row = 0; row < dungeon.rowsCount() && !playerPosFound; row++) {
			for (int col = 0; col < dungeon.columnCount() && !playerPosFound; col++) {
				if (dungeon.value(row, col) == 0 && dungeon.countNeighborsMatching(row, col, 0) == 8) {
					float[,] heightsCopy = dungeon.heights.Clone() as float[,];
					float maxFlood = (dungeon.rowsCount() - 2) * (dungeon.columnCount() - 2);
					float flood = floodFill(heightsCopy, row, col, 0, -1);
					float p = flood / maxFlood;
					if (p < 0.5) {
						Debug.Log ("El nivel no sirve! " + p);
						dungeon.valid = false;
						return dungeon;
					} else {
						dungeon.playerRow = row;
						dungeon.playerCol = col;
						dungeon.valid = true;
						playerPosFound = true;
					}
				}
			}
		}
		Debug.Log ("El nivel parece bueno");
		return dungeon;
	}

	/**
	Flood-fill (node, target-color, replacement-color):
		 1. If target-color is equal to replacement-color, 
		 	return.
		 2. If the color of node is not equal to target-color, 
		 	return.
		 3. Set the color of node to replacement-color.
		 4. Perform Flood-fill (one step to the west of node, target-color, replacement-color).
		    Perform Flood-fill (one step to the east of node, target-color, replacement-color).
		    Perform Flood-fill (one step to the north of node, target-color, replacement-color).
		    Perform Flood-fill (one step to the south of node, target-color, replacement-color).
		 5. Return.
	*/ 
	private int floodFill(float[,] heights, int row, int col, float target, float replacement) {
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
		heights [row, col] = replacement;
		int sum = 1;
		sum += floodFill (heights, row + 1, col, target, replacement);
		sum += floodFill (heights, row, col + 1, target, replacement);
		sum += floodFill (heights, row - 1, col, target, replacement);
		sum += floodFill (heights, row, col - 1, target, replacement);
		return sum;
	}
}
