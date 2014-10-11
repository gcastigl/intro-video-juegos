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

	public Dungeon Build(GameObject dungeonGO) {
		GameObject terrainGo = createTerrainGO(dungeonGO, "floor");
		Terrain terrain = terrainGo.GetComponent<Terrain>();
		if (config.useSeed) {
			Random.seed = config.seed;
		}
		TerrainData data = terrain.terrainData;
		int[,] heights = new int[config.heightmapResolution, config.heightmapResolution];
		Dungeon dungeon = new Dungeon(heights, config.width, config.lenght);
		addRandomWalls(heights);
		for (int i = 0; i < config.iterations; i++) {
			step (dungeon);
		}
		removeLonnelyPeeks(dungeon);
		surroundWithWalls(dungeon);
		data.SetHeights(0, 0, toFloatArray(dungeon));
		buildCeil(dungeonGO, terrain);
		return dungeon;
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

	private void buildCeil(GameObject dungeon, Terrain floorTerrain) {
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

	private void addRandomWalls(int[,] heights) {
		for (int x = 0; x < heights.GetLength(0); x++) {
			for (int z = 0; z < heights.GetLength(1); z++) {
				float random = Random.value;
				if (random < config.initialWallP) {
					heights[x, z] = 1;
				}
			}
		}
	}

	private void step(Dungeon dungeon) {
		int[,] heights = dungeon.heights;
		Dungeon cloned = new Dungeon(heights.Clone() as int[,]);
		for (int row = 0; row < dungeon.rowsCount(); row++) {
			for (int col = 0; col < dungeon.columnCount(); col++) {
				int wallNbs = cloned.countNeighborsMatching(row, col, 1);
				bool setWall = false;
				if (cloned.heights[row, col] == 1) {
					bool starving = wallNbs < config.starvationLimit;// || wallNbs > config.overpopLimit
					setWall = !starving;
				} else {
					setWall = wallNbs > config.birthNumber;
				}
				heights[row, col] = setWall ? 1 : 0;
			}
		}
	}
	
	private void removeLonnelyPeeks(Dungeon dungeon) {
		int[,] heights = dungeon.heights;
		Dungeon clone = new Dungeon(heights.Clone () as int[,]);
		for (int row = 0; row < dungeon.rowsCount(); row++) {
			for (int col = 0; col < dungeon.columnCount(); col++) {
				if (heights[row, col] == 1 && clone.countNeighborsMatching(row, col, 0) == 8) {
					heights[row, col] = 0;
				} else if (heights[row, col] == 0 && clone.countNeighborsMatching(row, col, 1) == 8) {
					heights[row, col] = 1;
				}
			}
		}
	}

	private void surroundWithWalls(Dungeon dungeon) {
		int[,] heights = dungeon.heights;
		for (int row = 0; row < dungeon.rowsCount(); row++) {
			for (int col = 0; col < dungeon.columnCount(); col++) {
				if (row == 0 || row == dungeon.rowsCount() - 1 || col == 0 || col == dungeon.columnCount() - 1) {
					heights[row, col] = 1;
				}
			}
		}
	}

	private float[,] toFloatArray(Dungeon dungeon) {
		float[,] heights = new float[dungeon.rowsCount(), dungeon.columnCount()];
		for (int row = 0; row < dungeon.rowsCount(); row++) {
			for (int col = 0; col < dungeon.columnCount(); col++) {
				// TODO: possible add some little noise?
				heights[row, col] = dungeon.heights[row, col];
			}
		}
		return heights;
	}
}
