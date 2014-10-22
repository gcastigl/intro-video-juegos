using UnityEngine;
using System.Collections;

[System.Serializable]
public struct BuildDungeonConfig {

	public float width;		// 129
	public float lenght;	// 129
	public float height;	// 4
	
	public float magic;		// 4
	
	// MUST be power of two + 1
	public int heightmapResolution;	// 129
	
	public bool useSeed;			// false

	public int seed;
	
	// number of times we perform the simulation step.
	public int iterations;			// 5
	
	// how dense the initial grid is with living cells
	public float initialWallP;		// 0.38f
	
	// number of neighbours that cause a dead cell to become alive.
	public int birthNumber;			// 3
	
	// lower neighbour limit at which cells start dying.
	public int starvationLimit;		// 4

	// upper neighbour limit at which cells start dying.
	// public int overpopLimit;		// 5

	public int enemiesAmount;		// 10

	public GameObject[] enemies;

	public int treasuresAmount;			// 15

	public GameObject[] treasures;

	public GameObject[] decorations;
}
