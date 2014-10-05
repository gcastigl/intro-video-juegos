using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

	public Room[] rooms;
	public GameObject dungeon;

	// Use this for initialization
	void Start () {
		Room initialRoom = Object.Instantiate(rooms[0]) as Room;
		initialRoom.transform.parent = dungeon.transform;
		createDungeon(initialRoom, 0);
	}
	
	private void createDungeon(Room room, int depth) {
		if (depth >= 1) {
			return;
		}
		for (int i = 0; i < room.doors.Length; i++) {
			Transform doorTransform = room.doors[i];
			// int roomIndex = Random.Range(1, rooms.Length);
			Room nextRoom = Object.Instantiate(rooms[2]) as Room;
			nextRoom.transform.parent = room.transform;
			Transform nextRoomTransform = doorTransform;
			createDungeon(nextRoom, depth + 1);
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
