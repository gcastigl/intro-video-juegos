using UnityEngine;
using System.Collections;

public class InvaderManager : MonoBehaviour {

	public GameObject invaderPrefab;
	public int rowsCount, invadersPerCol;
	public float xSeparation, ySeparation;
	public float xSpeed;
	public Vector2 xLimits;

	private Vector2 velocity;
	private GameObject[][] invaders;

	void Start() {
		float halfWidth = invadersPerCol * xSeparation / 2;
		float halfHeight = rowsCount * ySeparation / 2;
		float startX = transform.position.x - halfWidth;
		float startY = transform.position.y - halfHeight;
		invaders = new GameObject[rowsCount][];
		for (int row = 0; row < rowsCount; row++) {
			invaders[row] = new GameObject[invadersPerCol];
			for (int col = 0; col < invadersPerCol; col++) {
				invaders[row][col] = createInvader(startX, startY, row, col);
			}
		}
		velocity = new Vector2(xSpeed, 0);
	}

	private GameObject createInvader(float startX, float startY, int row, int col) {
		float x = startX + col * xSeparation;
		float y = startY + row * ySeparation;
		Vector3 position = new Vector3(x, y, transform.position.z);
		Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, 180));
		GameObject enemy = Object.Instantiate(invaderPrefab, position, rotation) as GameObject;
		enemy.transform.parent = transform;
		return enemy;
	}

	void Update() {
		if (escapedMinX() || escapedMaxX()) {
			velocity = velocity * -1;
		}
		shootWithClosestInvaders();
	}

	private bool escapedMaxX() {
		return transform.position.x < xLimits.x && velocity.x < 0; 
	}

	private bool escapedMinX() {
		return transform.position.x > xLimits.y && velocity.x > 0;
	}

	private void shootWithClosestInvaders() {
		for (int col = 0; col < invadersPerCol; col++) { 
			bool permissionToShootGranted = false;
			for (int row = 0; row < rowsCount && !permissionToShootGranted; row++) {
				if (invaders[row][col] != null) {
					Invader invader = invaders[row][col].GetComponent<Invader>();
					invader.permissionToShoot = true;
					permissionToShootGranted = true;
				}
			}
		}
	}

	void FixedUpdate () {
		rigidbody.velocity = velocity;
	}
}
