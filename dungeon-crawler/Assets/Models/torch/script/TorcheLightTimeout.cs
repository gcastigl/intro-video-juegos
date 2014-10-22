using UnityEngine;
using System.Collections;

public class TorcheLightTimeout : MonoBehaviour {
	
	private Torchelight torchelight;
	public float timeout;
	public bool on = true;

	void Start () {
		torchelight = GetComponent<Torchelight>();
	}
	
	void Update () {
		if (on) {
			timeout -= Time.deltaTime;
			if (timeout <= 0) {
				torchelight.MaxLightIntensity = 0;
				torchelight.IntensityLight = 0;
				Destroy(this);
			}
		}
	}
}
