using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject torchPrefab;
	public Vector2 highTorch = new Vector2(1.5f, 1.5f);
	public Vector2 lowTorch = new Vector2(0.2f, 0.9f);

	public int torchesLeft;

	private Torchelight torcheLight;
	private bool torchIsHight;

	void Start () {
		torchIsHight = true;
		torcheLight = GetComponentInChildren<Torchelight>();
	}
	
	void Update () {
		if (torchesLeft > 0 && Input.GetKeyUp(KeyCode.E)) {
			GameObject torch = Object.Instantiate(torchPrefab) as GameObject;
			Vector3 playerPos = gameObject.transform.position;
			torch.transform.position = new Vector3(playerPos.x, 0, playerPos.z);
			torchesLeft--;
		}
		if (Input.GetKeyUp(KeyCode.T)) {
			if (torchIsHight) {
				torcheLight.IntensityLight = lowTorch.x;
				torcheLight.MaxLightIntensity = lowTorch.y;
			} else {
				torcheLight.IntensityLight = highTorch.x;
				torcheLight.MaxLightIntensity = highTorch.y;
			}
			torchIsHight = !torchIsHight;
		}
	}

	public bool isTorchHigh() {
		return torchIsHight;
	}
}
