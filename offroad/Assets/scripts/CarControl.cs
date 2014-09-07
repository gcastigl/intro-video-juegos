using UnityEngine;
using System.Collections;

public class CarControl : MonoBehaviour {

	public WheelCollider frontWheelL;
	public WheelCollider frontWheelR;

	public WheelCollider rearWheelL;
	public WheelCollider rearWheelR;

	public float steerMax = 20f;
	public float motorMax = 10f;
	public float brakeMax = 100f;
	
	private float steer = 0f;
	private float motor = 0f;
	private float brake = 0f;
	
	void Start () {
		rigidbody.centerOfMass = new Vector3(0, 0f, 0.05f);
		Debug.Log (rigidbody.centerOfMass);
	}

	void Update() {
	}

	void FixedUpdate() {
		steer = Mathf.Clamp(Input.GetAxis("Horizontal"), -1, 1);
		motor = Mathf.Clamp(Input.GetAxis("Vertical"), 0, 1);
		brake = Mathf.Clamp(Input.GetAxis("Jump"), 0, 1);
		Debug.Log (brake);
		// rearWheel1.motorTorque = motorMax * motor;
		// rearWheel2.motorTorque = motorMax * motor;
		rearWheelL.brakeTorque = brakeMax * brake;
		rearWheelR.brakeTorque = brakeMax * brake;
		// Debug.Log (steerMax * steer);
		frontWheelL.motorTorque = motorMax * motor;
		frontWheelR.motorTorque = motorMax * motor;

		frontWheelL.steerAngle = steerMax * steer;
		frontWheelR.steerAngle = steerMax * steer;
	}
}
