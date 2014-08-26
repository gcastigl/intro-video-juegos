using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	public GameObject bulletPrefab;
	public float delay;
	public float bulletSpeed;
	public int hitAmount;

	private float delayUnitNextShoot = 0;

	void Start() {

	}

	void Update() {
		if (delayUnitNextShoot > 0) {
			delayUnitNextShoot -= Time.deltaTime;
		}
	}

	public void shoot(Vector3 direction) {
		if (isReady()) {
			delayUnitNextShoot = delay;
			createBullet(transform.position, direction);
		}
	}

	private void createBullet(Vector3 position, Vector3 direction) {
		GameObject bulletGO = Object.Instantiate(bulletPrefab, position, Quaternion.identity) as GameObject;
		Bullet bullet = bulletGO.GetComponent<Bullet>();
		bullet.velocity = direction * bulletSpeed;
		bullet.ttd = 2;
		bullet.hitAmount = hitAmount;
		bullet.shooter = transform.parent.gameObject;
	}

	public bool isReady() {
		return delayUnitNextShoot <= 0;
	}

}
