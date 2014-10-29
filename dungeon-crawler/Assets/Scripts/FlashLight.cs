using UnityEngine;

public class FlashLight : MonoBehaviour {

	public Light light1;
	public float timeLeft;

	private bool lowLight;

	void Start(){
		light1.enabled = false;
		lowLight = false;
	}

	void Update() {
		if (isOn ()) {
			timeLeft -= Time.deltaTime;
			if (timeLeft < 30 && !lowLight) {
				light1.intensity /= 2;
				lowLight = true;
			}
		}
		if (timeLeft < 0) {
			turnOff();
		}
		if (Input.GetKeyDown ("f")) {
			toggleStatus();
		}
	}

	public void toggleStatus() {
		light1.enabled = !light1.enabled;
	}

	public void turnOff() {
		light1.enabled = false;
	}

	public void turnOn() {
		light1.enabled = true;
	}

	public bool isOn() {
		return light1.enabled;
	}
}
