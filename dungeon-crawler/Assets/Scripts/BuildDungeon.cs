using UnityEngine;
using System.Collections;

public class BuildDungeon : MonoBehaviour {

	public Texture2D texture;

	public float width = 129;
	public float lenght = 129;
	public float height = 4;

	public float magic = 4f;

	// MUST be power of two + 1
	public int heightmapResolution = 129;

	public bool useSeed = false;
	public int seed;

	// number of times we perform the simulation step.
	public int iterations = 5;

	// how dense the initial grid is with living cells
	public float initialWallP  = 0.38f;

	// number of neighbours that cause a dead cell to become alive.
	public int birthNumber = 2;

	// lower neighbour limit at which cells start dying.
	public int starvationLimit = 4;
	/*
	// upper neighbour limit at which cells start dying.
	public int overpopLimit = 5;
	*/

	void Start () {
		Terrain terrain = createTerrainGO();
		if (useSeed) {
			Random.seed = seed;
		}
		TerrainData data = terrain.terrainData;
		float[,] heights = new float[heightmapResolution, heightmapResolution];
		addRandomWalls(heights);
		for (int i = 0; i < iterations; i++) {step (heights);}
		surroundWithWalls (heights);

		data.SetHeights (0, 0, heights);
	}

	private Terrain createTerrainGO() {
		TerrainData data = new TerrainData ();
		SplatPrototype splat = new SplatPrototype();
		splat.texture = texture;
		splat.tileOffset = new Vector2(0, 0);
		splat.tileSize = new Vector2(15, 15);
		SplatPrototype[] splats = new SplatPrototype[1];
		splats[0] = splat;
		data.splatPrototypes = splats;
		data.size = new Vector3 (width / magic, height, lenght / magic);
		data.heightmapResolution = heightmapResolution;
		data.baseMapResolution = 1024;
		data.SetDetailResolution(32, 8);
		GameObject terrainGo = Terrain.CreateTerrainGameObject(data);
		terrainGo.transform.parent = transform;
		Terrain terrain = terrainGo.GetComponent<Terrain> ();
		return terrain;
	}

	private void addRandomWalls(float[,] heights) {
		for (int x = 0; x < heights.GetLength(0); x++) {
			for (int z = 0; z < heights.GetLength(1); z++) {
				float random = Random.value;
				if (random < initialWallP) {
					heights[x, z] = 1;
				}
			}
		}
	}

	private void step(float[,] heights) {
		float[,] clone = heights.Clone () as float[,];
		for (int row = 0; row < heights.GetLength(0); row++) {
			for (int col = 0; col < heights.GetLength(1); col++) {
				float value = heights[row, col];
				int aliveNbs = countNeighborsMatching(clone, row, col, 1);
				bool setAlive = false;
				if (value == 1) {
					bool starving = aliveNbs < starvationLimit;// || aliveNbs > overpopLimit
					setAlive = !starving;
				} else {
					setAlive = aliveNbs > birthNumber;
				}
				heights[row, col] = setAlive ? 1 : 0;
			}
		}
	}

	private int countNeighborsMatching(float[,] heights, int row, int col, int value) {
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
	
	private void surroundWithWalls(float[,] heights) {
		for (int row = 0; row < heights.GetLength(0); row++) {
			for (int col = 0; col < heights.GetLength(1); col++) {
				if (row == 0 || row == heights.GetLength(0) - 1 || col == 0 || col == heights.GetLength(1) - 1) {
					heights[row, col] = 1;
				}
			}
		}
	}

}
