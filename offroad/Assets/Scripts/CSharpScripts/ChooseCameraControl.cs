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

	void Start () {
		targetRotation = transform.rotation;
		targetLocation = transform.position;
	}
	
	void Update () {
		if (Input.GetKey("1")) {
			targetRotation = Quaternion.Euler(option1RotationEuler);
			targetLocation = option1Location;
			target = option1;
		} else if (Input.GetKey("2")) {
			targetRotation = Quaternion.Euler(option2RotationEuler);
			targetLocation = option2Location;
			target = option2;
		}
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothFactor);
		transform.position = Vector3.Lerp(transform.position, targetLocation, Time.deltaTime * smoothFactor);
		if (target != null && Input.GetKey("space")) {
			target.SetActive(true);
			gameObject.SetActive(false);

		}
	}
}
