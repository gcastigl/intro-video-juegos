using UnityEngine;
using System.Collections;

public class Dungeon {

	public float[,] heights;
	public bool valid;
	public int playerRow, playerCol;

	public Dungeon(float[,] heights) {
		this.heights = heights;
	}

	public int rowsCount() {
		return heights.GetLength (0);
	}

	public int columnCount() {
		return heights.GetLength (1);
	}

	public float value(int row, int col) {
		return heights [row, col];
	}

	public bool validPosition(int row, int col) {
		return !(row < 0 || col < 0 || row >= heights.GetLength (0) || col >= heights.GetLength (1));
	}

	public int countNeighborsMatching(int row, int col, int value) {
		int count = 0;
		for (int rowi = row - 1; rowi <= row + 1; rowi++) {
			for (int colj = col - 1; colj <= col + 1; colj++) {
				if (rowi == row && colj == col) {
					continue;
				}
				if (rowi < 0 || colj < 0 || rowi >= heights.GetLength(0) || colj >= heights.GetLength(1)) {
					count++;
				} else if (heights[rowi, colj] == value) {
					count++;
				}
			}
		}
		return count;
	}

}
