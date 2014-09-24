using UnityEngine;
using System.Collections;

public class UserCarController : MonoBehaviour {

	public Drivetrain driveTrain;

	// public float throtle;
	public bool shiftDown, shiftUp;
	public bool up, down, right, left;
	public bool lshift;
	public bool carBreak, handBreak;

	void Start () {
	}
	
	void Update () {
		left = Input.GetKey(KeyCode.A);
		right = Input.GetKey(KeyCode.D);
		up = Input.GetKey(KeyCode.W);
		down = Input.GetKey(KeyCode.S);
		lshift = Input.GetKey (KeyCode.LeftShift);
		shiftDown = Input.GetKey (KeyCode.Q);
		shiftUp = Input.GetKey (KeyCode.E);
		handBreak = Input.GetKey (KeyCode.Space);
	}

	void OnGUI () {
		GUI.Label(new Rect(10, 10, 100, 20), "RPM: " + driveTrain.rpm);
		GUI.Label(new Rect(10, 30, 100, 20), "Marcha: " + (driveTrain.gear - 1));
		GUI.Label(new Rect(10, 50, 100, 20), "Vel: " + rigidbody.velocity.magnitude * 3.6f + " [Km/hr]");
		// driveTrain.automatic = GUILayout.Toggle(automatic, "Automatic Transmission");
	}
}
