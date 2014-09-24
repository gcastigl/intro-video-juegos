using UnityEngine;
using System.Collections;

public class UserCarController : MonoBehaviour {

	public SpeedometerUI speedometer;
	public Drivetrain driveTrain;

	// public float throtle;
	public bool shiftDown, shiftUp;
	public bool up, down, right, left;
	public bool lshift;
	public bool carBreak, handBreak;

	void Start () {
		speedometer.enabled = true;
		speedometer.dialPos = new Vector2(Screen.width - 180, Screen.height - 120);
	}

	void Update () {
		left = Input.GetKey(KeyCode.A);
		right = Input.GetKey(KeyCode.D);
		up = Input.GetKey(KeyCode.W);
		down = Input.GetKey(KeyCode.S);
		lshift = Input.GetKey (KeyCode.LeftShift);
		shiftDown = Input.GetKeyDown (KeyCode.Q);
		shiftUp = Input.GetKeyDown (KeyCode.E);
		handBreak = Input.GetKey (KeyCode.Space);
	}

	void OnGUI () {

		GUI.Label(new Rect(Screen.width * 0.88f, Screen.height - 70, 100, 20), "RPM: " + driveTrain.rpm);
		GUI.Label(new Rect(Screen.width * 0.88f, Screen.height - 100, 100, 20), "Gear: " + (driveTrain.gear - 1));
		// driveTrain.automatic = GUILayout.Toggle(automatic, "Automatic Transmission");
		speedometer.speed = rigidbody.velocity.magnitude * 3.6f;
	}
}
