using UnityEngine;
using System.Collections;

public class Treasure  {

	public Vector2 position;
	public bool isOpen;

	public Treasure(Vector2 position, bool isOpen) {
		this.position = position;
		this.isOpen = isOpen;
	}

}
