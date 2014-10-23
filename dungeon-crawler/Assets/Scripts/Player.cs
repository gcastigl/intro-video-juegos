using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject mapPrefab;

	public GameObject torchPrefab;
	public int torchTimeout = 60 * 5;
	public float switchTorchDelay = 5;
	public Vector2 highTorch = new Vector2(1.5f, 1.5f);
	public GameObject torch;
	public GameObject camera;

	private GameObject map;
	private bool torchIsHight;	
	private Torchelight torcheLight;
	private TorcheLightTimeout torcheLightTimeout;

	public bool alive;
	public int torchesLeft;
	private AudioSource ambientAudioSource;
	private AudioSource deathAudioSource;

	private float switchTorchDelayCounter;

	void Start () {
		alive = true;
		map = Object.Instantiate (mapPrefab) as GameObject;
		torcheLight = torch.GetComponent<Torchelight>();
		litNewTorchlight();
		ambientAudioSource = GetComponents<AudioSource>()[0];
		deathAudioSource = GetComponents<AudioSource>()[1];
	}

	void Update () {
		if (torchesLeft > 0 && Input.GetKeyUp(KeyCode.E)) {
			GameObject torch = Object.Instantiate(torchPrefab) as GameObject;
			TorcheLightTimeout torcheTimeout = torch.AddComponent<TorcheLightTimeout>();
			torcheTimeout.timeout = torchTimeout + (Random.value * torchTimeout / 10);
			Vector3 playerPos = gameObject.transform.position;
			torch.transform.position = new Vector3(playerPos.x, 0, playerPos.z);
			torchesLeft--;
		}
		if (Input.GetKeyUp(KeyCode.T)) {
			TorcheLightTimeout torcheTimeout = torch.GetComponent<TorcheLightTimeout>();
			torcheTimeout.on = !torchIsHight;
			if (torchIsHight) {
				torcheLight.IntensityLight = 0;
				torcheLight.MaxLightIntensity = 0;
			} else {
				torcheLight.IntensityLight = highTorch.x;
				torcheLight.MaxLightIntensity = highTorch.y;
			}
			torchIsHight = !torchIsHight;
		}
		if (Input.GetKeyUp(KeyCode.Tab)) {
			map.SetActive(!map.activeSelf);
		}
		if (torcheLightTimeout == null) {
			switchTorchDelayCounter -= Time.deltaTime;
			if (switchTorchDelayCounter < 0) {
				litNewTorchlight();
			}
		}
	}

	private void litNewTorchlight() {
		torchesLeft--;
		torcheLightTimeout = torch.AddComponent<TorcheLightTimeout>();
		torcheLightTimeout.timeout = torchTimeout + (Random.value * torchTimeout / 10);
		torchIsHight = true;
		torcheLight.IntensityLight = highTorch.x;
		torcheLight.MaxLightIntensity = highTorch.y;
		switchTorchDelayCounter = switchTorchDelay;
	}

	public bool isTorchHigh() {
		return torchIsHight;
	}

	public TorcheLightTimeout getLightTimeout() {
		return torcheLightTimeout;
	}
	public void kill() {
		if (alive) {
			alive = false;
			deathAudioSource.Play();
			GameObject deathOverlay = GameObject.FindGameObjectWithTag("DeathOverlay");
			deathOverlay.GetComponent<GUITexture>().enabled = true;
			disableMovement();
		}
	}

	public void disableMovement() {
		enabled = false;
		GetComponent<MouseLook>().enabled = false;
		GetComponent<CharacterMotor>().enabled = false;
		camera.GetComponent<MouseLook>().enabled = false;
		ambientAudioSource.enabled = false;
		GameObject.FindGameObjectWithTag("TorchInformation").SetActive(false);
	}
}
