using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	
	private int hp;

	void Start () {
		hp = 1;
	}
	
	void Update () {

	}

	public void damage(int damageCount) {
		hp -= damageCount;
		if (hp <= 0) {
			Destroy(gameObject);
		}
	}
}

