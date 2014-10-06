package vj;

import java.util.Random;

public class PlaceItems {

	private Random _random;

	public PlaceItems(Random random) {
		_random = random;
	}

	public PlaceItems on(DungeonMap map, float p) {
		int radix = 5;
		for (PlaceableMark spawn : map.spawns()) {
			if (_random.nextFloat() > p) {
				continue;
			}
			boolean valid;
			int row, column;
			do {
				row = spawn.row() + (int) (radix * _random.nextFloat());
				column = spawn.column() + (int) (radix * _random.nextFloat());
				valid = map.isValid(row, column);
			} while (!valid);
			map.items().add(new PlaceableMark(row, column, getType(spawn)));
		}
		return this;
	}

	private int getType(PlaceableMark mark) {
		int type = mark.type();
		return type == PlaceableMark.SPAWN_BOSS ? PlaceableMark.ITEM_TYPE_BOSS : 
			type == PlaceableMark.SPAWN_TYPE_1 ? PlaceableMark.ITEM_TYPE_1 : 
			type == PlaceableMark.SPAWN_TYPE_2 ? PlaceableMark.ITEM_TYPE_2 : 
				PlaceableMark.ITEM_TYPE_3;
	}
}
