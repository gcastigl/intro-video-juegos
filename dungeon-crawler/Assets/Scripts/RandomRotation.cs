﻿using UnityEngine;
using System.Collections;

public class RandomRotation : MonoBehaviour {

	public GameObject axisGO;
	public float maxDistance = 0.5f;
	public float rotationV = 20;

	public float yRotationSide = 1;
	public float yFrequency = 1;
	public float yAmplitude = 1;
	public float yOffset = 1;
	private float index;

	void Start () {
		index = Random.Range (-10, 10);
		Vector3 dir = new Vector3(Random.Range(0, 1), 0, Random.Range(1, 1)).normalized;
		Vector3 dist = dir * maxDistance;
		transform.localPosition = transform.localPosition + dist;
		Debug.Log (transform.localPosition);
	}

	void Update () {
		index += Time.deltaTime;
		transform.RotateAround(axisGO.transform.position, Vector3.up * yRotationSide, rotationV * Time.deltaTime);
		float y = Mathf.Sin(yFrequency * index) * yAmplitude + yOffset;
		Vector3 localPos = transform.localPosition;
		transform.localPosition = new Vector3(localPos.x, y, localPos.z);
	}
}
