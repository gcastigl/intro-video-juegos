using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	
	public int hp;

	void Start () {
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

