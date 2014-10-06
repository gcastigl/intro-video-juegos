package vj;

import java.util.LinkedList;
import java.util.List;

public class DungeonMap {

	public static final int FLOOR = 0;
	public static final int WALL = 1;

	private int _width, _height;
	private int[][] _tiles;
	private final List<PlaceableMark> _spawns = new LinkedList<>();
	private final List<PlaceableMark> _items = new LinkedList<>();

	public DungeonMap(int height, int width) {
		_height = height;
		_width = width;
		_tiles = new int[height][width];
	}

	public final int height() {
		return _height;
	}

	public final int width() {
		return _width;
	}

	public final int[][] tiles() {
		return _tiles;
	}

	public void setTile(int row, int column, int value) {
		_tiles[row][column] = value;
	}

	public final int tile(int row, int column) {
		return _tiles[row][column];
	}

	public boolean isValid(int row, int column) {
		return row >= 0 && column >= 0 && row < height() && column < width();
	}

	public int countNeighborsMatching(int row, int column, int value) {
		int count = 0;
		for (int rowi = row - 1; rowi <= row + 1; rowi++) {
			for (int colj = column - 1; colj <= column + 1; colj++) {
				if (rowi == row && colj == column) {
					continue;
				}
				if (rowi < 0 || rowi >= height() || colj < 0 || colj >= width() || tile(rowi, colj) == value) {
					count++;
				}
			}
		}
		return count;
	}

	public List<PlaceableMark> spawns() {
		return _spawns;
	}

	public List<PlaceableMark> items() {
		return _items;
	}

	@Override
	public String toString() {
		char[][] values = new char[height()][width()];
		for (int row = 0; row < height(); row++) {
			for (int column = 0; column < width(); column++) {
				values[row][column] = tile(row, column) == FLOOR ? ' ' : '#';
			}
		}
		for (PlaceableMark spawn : spawns()) {
			int c = (int) '0' + spawn.type();
			values[spawn.row()][spawn.column()] = (char) c;
		}
		for (PlaceableMark item : items()) {
			int c = (int) '_' + item.type();
			values[item.row()][item.column()] = (char) c;
		}
		StringBuilder s = new StringBuilder();
		for (char[] row : values) {
			for (char c : row) {
				s.append(c).append(" ");
			}
			s.append("\n");
		}
		return s.toString();
	}
}
