using UnityEngine;
using System.Collections;

public class Dungeon {

	public bool valid;
	public int[,] heights;
	public bool[,] accesibles;
	public int playerRow, playerCol;
	public float worldWidth, worldLenght;

	public Dungeon(int[,] heights) : this(heights, 0, 0) {
	}

	public Dungeon(int[,] heights, float worldWidth, float worldLenght) {
		this.heights = heights;
		this.accesibles = new bool[rowsCount (), columnCount ()];
		this.worldWidth = worldWidth;
		this.worldLenght = worldLenght;
	}

	public int rowsCount() {
		return heights.GetLength (0);
	}

	public int columnCount() {
		return heights.GetLength (1);
	}

	public int value(int row, int col) {
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

	public float rowToWorld(int row) {
		return row *  worldWidth / (float) rowsCount();
	}

	public float columnToWorld(int col) {
		return col *  worldLenght / (float) columnCount();
	}

	public float distanceSq(int row1, int col1, int row2, int col2) {
		float row1World = rowToWorld (row1);
		float col1World = columnToWorld (col1);
		float row2World = rowToWorld (row2);
		float col2World = columnToWorld (col2);
		float dx = row1World - row2World;
		float dy = col1World - col2World;
		return dx * dx + dy * dy;
	}
}
