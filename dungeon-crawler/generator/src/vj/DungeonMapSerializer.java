package vj;

import java.awt.image.BufferedImage;
import java.io.File;
import java.io.FileOutputStream;
import java.io.FileWriter;
import java.io.IOException;
import java.util.List;

import javax.imageio.ImageIO;

public class DungeonMapSerializer {

	private String _filepath;

	public DungeonMapSerializer(String filepath) {
		_filepath = filepath + File.separator;
	}

	public DungeonMapSerializer save(DungeonMap map, String name) {
		toPng(map, name);
		toRaw(map, name);
		return this;
	}

	private void toPng(DungeonMap map, String name) {
		BufferedImage image = new BufferedImage(map.width(), map.height(), BufferedImage.TYPE_INT_RGB);
		for (int row = 0; row < map.height(); row++) {
			for (int column = 0; column < map.width(); column++) {
				int grayscale = (map.tile(row, column)) * 255;
				int pixel = (grayscale << 16) | (grayscale << 8) | grayscale;
				image.setRGB(column, row, pixel);
			}
		}
		image.flush();
		File file = new File(_filepath + name + ".png");
		try {
			ImageIO.write(image, "png", file);
		} catch (IOException e) {
			throw new IllegalStateException(e);
		}
	}

	private void toRaw(DungeonMap map, String name) {
		int[] bytes = new int[map.width() * map.height()];
		int index = 0;
		for (int row = 0; row < map.height(); row++) {
			for (int col = 0; col < map.width(); col++) {
				int value = map.tile(row, col);
				bytes[index++] = (value == 1) ? 0xff : 0x0;
			}
		}
		try {
			File file = new File(_filepath + name + ".raw");
			FileOutputStream writer = new FileOutputStream(file);
			for (int b : bytes) {
				writer.write(b);
			}
			for (int b : bytes) {
				writer.write(b);
			}
			for (int b : bytes) {
				writer.write(b);
			}
			writer.close();
		} catch (IOException e) {
			throw new IllegalStateException(e);
		}
	}

	public DungeonMapSerializer saveMarkPoints(List<PlaceableMark> spawns, String name) {
		try {
			File file = new File(_filepath + name + ".properties");
			FileWriter writer = new FileWriter(file);
			int index = 0;
			for (PlaceableMark spawn : spawns) {
				writer.write(String.format("%d %d %d %d\n", index, spawn.row(), spawn.column(), spawn.type()));
				index++;
			}
			writer.close();
			return this;
		} catch (IOException e) {
			throw new IllegalStateException(e);
		}
	}

}
