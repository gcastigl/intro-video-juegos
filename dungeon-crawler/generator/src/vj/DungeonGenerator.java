package vj;

import java.util.Random;

public class DungeonGenerator {

	private final Random _random;
	private float _wallsInitialP;
	private int _width;
	private int _heigth;
	private int _iterations = 5;

	public DungeonGenerator(Random random) {
		_random = random;
	}

	public DungeonGenerator setDimentions(int height, int width) {
		_heigth = height;
		_width = width;
		return this;
	}

	public DungeonGenerator setWallsInitial(float wallsInitial) {
		_wallsInitialP = Math.max(0, Math.min(1, wallsInitial));
		return this;
	}

	public DungeonGenerator setIterations(int iterations) {
		_iterations = iterations;
		return this;
	}

	public DungeonMap create() {
		DungeonMap map = new DungeonMap(_heigth, _width);
		placeRandomWalls(map, _wallsInitialP);
		for (int iteration = 0; iteration < _iterations; iteration++) {
			DungeonMap next = new DungeonMap(map.height(), map.width());
			for (int row = 0; row < map.height(); row++) {
				for (int column = 0; column < map.width(); column++) {
					int tile = map.tile(row, column);
					int wallsCount = map.countNeighborsMatching(row, column, DungeonMap.WALL);
					boolean isWall = tile == DungeonMap.WALL && wallsCount >= 4;
					isWall |= tile != DungeonMap.WALL && wallsCount >= 5;
					next.setTile(row, column, isWall ? DungeonMap.WALL : DungeonMap.FLOOR);
				}
			}
			map = next;
		}
		return map;
	}

	private void placeRandomWalls(DungeonMap map, float p) {
		for (int row = 0; row < map.height(); row++) {
			for (int col = 0; col < map.width(); col++) {
				if (_random.nextFloat() <= p) {
					map.tiles()[row][col] = DungeonMap.WALL;
				}
			}
		}
	}

	public DungeonMap invert(DungeonMap map) {
		DungeonMap inverted = new DungeonMap(map.height(), map.width());
		for (int col = 0; col < map.width(); col++) {
			for (int row = 0; row < map.height(); row++) {
				inverted.setTile(row, col, 1 - map.tile(row, col));
			}
		}
		return inverted;
	}

	public DungeonMap mirrorY(DungeonMap map) {
		int columnsCount = map.width() - 1;
		DungeonMap inverted = new DungeonMap(map.height(), map.width());
		for (int col = 0; col < map.width(); col++) {
			for (int row = 0; row < map.height(); row++) {
				inverted.setTile(row, col, map.tile(row, columnsCount - col));
			}
		}
		return inverted;
	}
}
