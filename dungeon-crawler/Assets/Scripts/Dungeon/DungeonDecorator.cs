using UnityEngine;
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
				}
			} while(!placed);
		}
		/*
		 * TODO: descomentar esto para agregar un particleSystem al dungeon
		GameObject particlesGO = new GameObject("particles");
		particlesGO.transform.parent = decorations.transform;
		particlesGO.transform.position = new Vector3(dungeon.worldWidth/ 2 , 2, dungeon.worldLenght / 2);
		ParticleSystem particles = particlesGO.AddComponent<ParticleSystem>();
		// particles.duration = 1;
		Gradient gradient = new Gradient ();
		// TODO: color del gradietne
		particles.startColor = new Color(1, 1, 1);
		particles.simulationSpace = ParticleSystemSimulationSpace.World;
		*/

	}
}
