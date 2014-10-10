using UnityEngine;
using System.Collections;

public class BuildDungeon {
	
	private Texture2D texture;
	private Material ceilMaterial;
	private BuildDungeonConfig config;

	public BuildDungeon(BuildDungeonConfig config, Texture2D texture, Material ceilMaterial) {
		this.config = config;
		this.texture = texture;
		this.ceilMaterial = ceilMaterial;
	}

	public float[,] Build(GameObject dungeon) {
		GameObject terrainGo = createTerrainGO(dungeon, "floor");
		Terrain terrain = terrainGo.GetComponent<Terrain>();
		if (config.useSeed) {
			Random.seed = config.seed;
		}
		TerrainData data = terrain.terrainData;
		float[,] heights = new float[config.heightmapResolution, config.heightmapResolution];
		addRandomWalls(heights);
		for (int i = 0; i < config.iterations; i++) {step (heights);}
		surroundWithWalls (heights);
		data.SetHeights (0, 0, heights);
		buildCeil(dungeon, terrain, heights);
		return heights;
	}

	private GameObject createTerrainGO(GameObject dungeon, string name) {
		TerrainData data = new TerrainData ();
		SplatPrototype splat = new SplatPrototype();
		splat.texture = texture;
		splat.tileOffset = new Vector2(0, 0);
		splat.tileSize = new Vector2(15, 15);
		SplatPrototype[] splats = new SplatPrototype[1];
		splats[0] = splat;
		data.splatPrototypes = splats;
		data.size = new Vector3 (config.width / config.magic, config.height, config.lenght / config.magic);
		data.heightmapResolution = config.heightmapResolution;
		data.baseMapResolution = 1024;
		data.SetDetailResolution(32, 8);
		GameObject terrainGo = Terrain.CreateTerrainGameObject(data);
		terrainGo.transform.parent = dungeon.transform;
		terrainGo.name = name;
		terrainGo.isStatic = false;
		return terrainGo;
	}

	private void buildCeil(GameObject dungeon, Terrain floorTerrain, float[,] floorHeights) {
		GameObject ceil = GameObject.CreatePrimitive(PrimitiveType.Plane) as GameObject;
		ceil.name = "ceil";
		ceil.transform.parent = dungeon.transform;
		float scale = config.heightmapResolution / 10f;
		float dx = config.heightmapResolution / 2f;
		ceil.transform.localScale = new Vector3 (scale, 1, scale);
		ceil.transform.localPosition = new Vector3(dx, config.height, dx);
		ceil.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, 180));
		MeshRenderer renderer = ceil.GetComponent<MeshRenderer>();
		renderer.material = ceilMaterial;
	}

	private void addRandomWalls(float[,] heights) {
		for (int x = 0; x < heights.GetLength(0); x++) {
			for (int z = 0; z < heights.GetLength(1); z++) {
				float random = Random.value;
				if (random < config.initialWallP) {
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
					bool starving = aliveNbs < config.starvationLimit;// || aliveNbs > config.overpopLimit
					setAlive = !starving;
				} else {
					setAlive = aliveNbs > config.birthNumber;
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
