using UnityEngine;
using System.Collections;

public class UserCarController : MonoBehaviour {

	// public float throtle;
	public bool shiftDown, shiftUp;
	public bool up, down, right, left;
	public bool lshift;
	public bool carBreak, handBreak;

	void Start () {
	}
	
	void Update () {
		left = Input.GetKey(KeyCode.LeftArrow);
		right = Input.GetKey(KeyCode.RightArrow);
		up = Input.GetKey(KeyCode.UpArrow);
		down = Input.GetKey(KeyCode.DownArrow);
		lshift = Input.GetKey (KeyCode.LeftShift);
		shiftUp = Input.GetKey (KeyCode.A);
		shiftDown = Input.GetKey (KeyCode.Z);
		handBreak = Input.GetKey (KeyCode.Space);
	}
}
