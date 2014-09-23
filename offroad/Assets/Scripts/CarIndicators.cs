using UnityEngine;
using System.Collections;

public class CarIndicators : MonoBehaviour {

	// public CarScript car;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		// GUI.Label(new Rect(10, 10, 100, 20), "RPM: " + car.currRPM());
		// GUI.Label(new Rect(10, 25, 100, 20), "Gear: " + car.currGear());
		// GUI.Label(new Rect(10, 40, 100, 20), "Torque: " + car.getWFR().motorTorque);
		// float wrpm = car.getWFR ().rpm;
		// Debug.Log(wrpm);
		// GUI.Label(new Rect(10, 55, 100, 20), "Wheel RPM: " + (int) wrpm);
	}
}
