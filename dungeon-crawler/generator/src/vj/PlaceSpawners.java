package vj;

import java.util.Random;

public class PlaceSpawners {

	private Random _random;

	public PlaceSpawners(Random random) {
		_random = random;
	}

	public PlaceSpawners on(DungeonMap map, int amount) {
		int index = 0;
		do {
			int row = (int) (_random.nextFloat() * map.height());
			int column = (int) (_random.nextFloat() * map.width());
			int tile = map.tile(row, column);
			if (tile == DungeonMap.FLOOR && map.countNeighborsMatching(row, column, DungeonMap.FLOOR) >= 7) {
				index++;
				int type = index == amount ? PlaceableMark.SPAWN_BOSS : randomType();
				map.spawns().add(new PlaceableMark(row, column, type));
			}
		} while (index < amount);
		return this;
	}

	private int randomType() {
		float r = _random.nextFloat();
		return r < 0.33f ? PlaceableMark.SPAWN_TYPE_3 : r < 0.67f ? PlaceableMark.SPAWN_TYPE_2 : PlaceableMark.SPAWN_TYPE_1;
	}
}
