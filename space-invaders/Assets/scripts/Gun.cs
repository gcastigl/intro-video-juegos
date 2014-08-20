using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	public string hitTag;
	public GameObject bulletPrefab;
	public float delay;
	public float bulletSpeed;
	private float delayUnitNextShoot = 0;

	// Use this for initialization
	void Start() {

	}

	void Update() {
		if (delayUnitNextShoot > 0) {
			delayUnitNextShoot -= Time.deltaTime;
			delayUnitNextShoot = Mathf.Max(0f, delayUnitNextShoot);
		}
		if (delayUnitNextShoot == 0 && Input.GetKey(KeyCode.Space)) {
			delayUnitNextShoot = delay;
			Vector3 position = transform.position;
			GameObject bulletGO = Object.Instantiate(bulletPrefab, position, Quaternion.identity) as GameObject;
			Bullet bullet = bulletGO.GetComponent<Bullet>();
			bullet.velocity = new Vector2(0, 1) * bulletSpeed;
			bullet.ttd = 2;
			bullet.hitTag = hitTag;
		}
	}

}
