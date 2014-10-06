package vj;

public class PlaceableMark {

	public static final int SPAWN_BOSS = 0;
	public static final int SPAWN_TYPE_1 = 1;
	public static final int SPAWN_TYPE_2 = 2;
	public static final int SPAWN_TYPE_3 = 3;

	public static final int ITEM_TYPE_BOSS = 0;
	public static final int ITEM_TYPE_1 = 1;
	public static final int ITEM_TYPE_2 = 2;
	public static final int ITEM_TYPE_3 = 3;

	private int _row, _column;
	private int _type;

	public PlaceableMark(int row, int column, int type) {
		_row = row;
		_column = column;
		_type = type;
	}

	public int type() {
		return _type;
	}

	public int row() {
		return _row;
	}

	public int column() {
		return _column;
	}
}
