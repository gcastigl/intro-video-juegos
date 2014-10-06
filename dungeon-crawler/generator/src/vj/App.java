package vj;

import java.util.Random;

public class App {

	public static void main(String[] args) {
		Random random = new Random();
		random.setSeed(10l);
		DungeonGenerator generator = new DungeonGenerator(random).setIterations(5).setWallsInitial(0.45f).setDimentions(129, 129);
		DungeonMap map = new DungeonMapCorrector(generator.create()).removeSmallHeights().surroundWithWalls().getMap();
		new PlaceSpawners(random).on(map, 15);
		new PlaceItems(random).on(map, 1f);
		new DungeonMapSerializer("D:/projects/intro-vj/dungeon-crawler/terrains")
			.save(map, "floor")
			.save(generator.mirrorY(map), "ceil")
			.saveMarkPoints(map.spawns(), "spawns")
			.saveMarkPoints(map.items(), "items");
		System.out.println(map);
	}

}
