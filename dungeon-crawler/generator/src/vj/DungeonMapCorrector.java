package vj;

public class DungeonMapCorrector {

	private DungeonMap _map;

	public DungeonMapCorrector(DungeonMap map) {
		_map = map;
	}

	public DungeonMapCorrector surroundWithWalls() {
		for (int row = 0; row < _map.height(); row++) {
			for (int col = 0; col < _map.width(); col++) {
				if (row == 0 || row == _map.height() - 1 || col == 0 || col == _map.width() - 1) {
					_map.setTile(row, col, DungeonMap.WALL);
				}
			}
		}
		return this;
	}

	public DungeonMapCorrector removeSmallHeights() {
		DungeonMap modified = new DungeonMap(_map.height(), _map.width());
		for (int row = 0; row < _map.height(); row++) {
			for (int col = 0; col < _map.width(); col++) {
				int tile = _map.tile(row, col);
				if (tile == DungeonMap.WALL && _map.countNeighborsMatching(row, col, DungeonMap.FLOOR) >= 7) {
					tile = DungeonMap.FLOOR;
				}
				modified.setTile(row, col, tile);
			}
		}
		_map = modified;
		return this;
	}

	public DungeonMap getMap() {
		return _map;
	}

	@Override
	public String toString() {
		return _map.toString();
	}
}
