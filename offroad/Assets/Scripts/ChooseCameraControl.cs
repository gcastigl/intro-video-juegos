using UnityEngine;
using System.Collections;

public class ChooseCameraControl : MonoBehaviour {

	public float smoothFactor = 1;

	public Vector3 option1RotationEuler;
	public Vector3 option1Location;
	public GameObject option1;

	public Vector3 option2RotationEuler;
	public Vector3 option2Location;
	public GameObject option2;

	private Quaternion targetRotation;
	private Vector3 targetLocation;
	private GameObject target;
	public GameObject guiText1;
	public GameObject guiText2;
	public GameObject guiText3;
	public GameObject helpText;

	void Start () {
		targetRotation = transform.rotation;
		targetLocation = transform.position;
		guiText2.SetActive(false);
		guiText3.SetActive(false);
		Screen.showCursor = false;
		Screen.lockCursor = true;
	}
	
	void Update () {
		if (Input.GetKey(KeyCode.Escape)) {
			Application.Quit();
			return;
		}
		if (Input.GetKey("1")) {
			guiText1.SetActive(false);
			guiText2.SetActive(true);
			targetRotation = Quaternion.Euler(option1RotationEuler);
			targetLocation = option1Location;
			target = option1;
			setSelected(1);
		} else if (Input.GetKey("2")) {
			guiText1.SetActive(false);
			guiText2.SetActive(true);
			targetRotation = Quaternion.Euler(option2RotationEuler);
			targetLocation = option2Location;
			target = option2;
			setSelected(2);
		}
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothFactor);
		transform.position = Vector3.Lerp(transform.position, targetLocation, Time.deltaTime * smoothFactor);
		if (target != null && Input.GetKey("space")) {
			target.SetActive(true);
			UserCarController controller = target.gameObject.GetComponentInParent<UserCarController>();
			controller.enabled = true;
			gameObject.SetActive(false);
			guiText2.SetActive(false);
			guiText3.SetActive(true);
			helpText.SetActive(true);
		}
	}

	private void setSelected(int carType) {
		PlayerPrefs.SetInt("carType", carType);
	}
}
