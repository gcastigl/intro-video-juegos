using UnityEngine;
using System.Collections;

public class CameraSelection : MonoBehaviour {

	private const int DRIVER_CAMERA = 1;
	private const int REAR_CAMERA = 2;

	private int selectedCamera = REAR_CAMERA;
	public CarCamera carCamera;
	public MouseLook mouseLook;

	public GameObject driverPosition;

	void Start () {
		setDriverCamera();
	}

	void Update () {
		if (selectedCamera != DRIVER_CAMERA && Input.GetKey ("1")) {
			setDriverCamera();
		} else if (selectedCamera != REAR_CAMERA && Input.GetKey ("2")) {
			selectedCamera = REAR_CAMERA;
			carCamera.enabled = true;
			mouseLook.enabled = false;
		}
	}

	private void setDriverCamera() {
		selectedCamera = DRIVER_CAMERA;
		carCamera.enabled = false;
		mouseLook.enabled = true;
		transform.position = driverPosition.transform.position;
	}
}
