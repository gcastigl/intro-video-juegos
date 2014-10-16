using UnityEngine;
using System.Collections;

public class FreezeLocalPostition : MonoBehaviour {

	public Vector3 position;

	void Update () {
		if (Mathf.Abs(transform.localPosition.x) > 0.1f || Mathf.Abs(transform.localPosition.y) > 0.1f || Mathf.Abs(transform.localPosition.z) > 0.1f) {
			transform.localPosition = position;
		}
		Vector3 localRotation = transform.localEulerAngles;
		if (Mathf.Abs(localRotation.x) > 10 || Mathf.Abs(localRotation.y) > 10 || Mathf.Abs(localRotation.z) > 10) {
			transform.localEulerAngles = Vector3.zero;
		}
	}
}
