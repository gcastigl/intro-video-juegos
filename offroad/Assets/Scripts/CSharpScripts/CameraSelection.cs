using UnityEngine;
using System.Collections;

public class CameraSelection : MonoBehaviour {

	private const int DRIVER_CAMERA = 1;
	private const int REAR_CAMERA = 2;

	private int selectedCamera = REAR_CAMERA;
	public CarCamera carCamera;
	public MouseLook mouseLook;

	void Start () {
	}

	void Update () {
		if (selectedCamera != DRIVER_CAMERA && Input.GetKey ("1")) {
			selectedCamera = DRIVER_CAMERA;
			carCamera.enabled = false;
			mouseLook.enabled = true;
			transform.position = transform.parent.position + mouseLook.preferedPosition;
		} else if (selectedCamera != REAR_CAMERA && Input.GetKey ("2")) {
			selectedCamera = REAR_CAMERA;
			carCamera.enabled = true;
			mouseLook.enabled = false;
		}
	}
}
