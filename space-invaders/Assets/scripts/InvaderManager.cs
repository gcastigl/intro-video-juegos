using UnityEngine;
using System.Collections;

public class InvaderManager : MonoBehaviour {

	public GameObject invaderTopPrefab;
	public GameObject invaderMidPrefab;
	public GameObject invaderBottomPrefab;
	public int rowsCount, invadersPerCol;
	public float xSeparation, ySeparation;
	public float xSpeed;
	public float xSpeedIncrement;
	public Vector2 xLimits;

	private Vector2 velocity;
	private GameObject[][] invaders;
	public TimeDelay shootingDelay;
	private bool goingDown;
	private float lastVelX;
	private float targetPosY = 0;

	void Start() {
		initializeInvaders();
		velocity = new Vector2(xSpeed, 0);
		shootingDelay = GetComponent<TimeDelay>();
	}

	private void initializeInvaders() {
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
	}

	private GameObject createInvader(float startX, float startY, int row, int col) {
		float x = startX + col * xSeparation;
		float y = startY + row * ySeparation;
		Vector3 position = new Vector3(x, y, transform.position.z);
		Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, 0));
		GameObject prefab = row < 2 ? invaderBottomPrefab : (row < 4 ? invaderMidPrefab : invaderTopPrefab);
		GameObject enemy = Object.Instantiate(prefab, position, rotation) as GameObject;
		enemy.transform.parent = transform;
		return enemy;
	}

	void Update() {
		if (escapedMinX() || escapedMaxX()) {
			lastVelX = velocity.x;
			velocity = new Vector3(0, -1f);
			targetPosY = transform.position.y - 0.5f;
			goingDown = true;
		}
		if (transform.position.y < targetPosY && goingDown) {
			goingDown = false;
			velocity = new Vector2(-lastVelX, 0);
		}
		if (shootingDelay.isReady()) {
			bool hasInvadersLeft = shootWithClosestInvaders();
			if (!hasInvadersLeft) {
				Destroy(gameObject);
			}
		}
	}

	private bool escapedMaxX() {
		return transform.position.x < xLimits.x && velocity.x < 0; 
	}

	private bool escapedMinX() {
		return transform.position.x > xLimits.y && velocity.x > 0;
	}

	private bool shootWithClosestInvaders() {
		int aliveInvaders = 0;
		for (int col = 0; col < invadersPerCol; col++) { 
			bool permissionToShootGranted = false;
			for (int row = 0; row < rowsCount && !permissionToShootGranted; row++) {
				if (invaders[row][col] != null) {
					Invader invader = invaders[row][col].GetComponent<Invader>();
					invader.permissionToShoot = true;
					permissionToShootGranted = true;
					aliveInvaders++;
				}
			}
		}
		return aliveInvaders > 0;
	}

	void FixedUpdate () {
		rigidbody.velocity = velocity * 1.5f;
	}
}
