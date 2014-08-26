using UnityEngine;
using System.Collections;

public class SpaceInvadersLogo : MonoBehaviour {

	public TextMesh mesh;

	void Start () {
		InvokeRepeating("ChangeColor", 0f, 0.8f);
	}

	void OnDestroy(){
		Application.LoadLevel(1);
	}

	void ChangeColor(){
		mesh.color = new Color (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f), 1f);
	}
}
