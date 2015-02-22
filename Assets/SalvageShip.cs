using UnityEngine;
using System.Collections;
using System;

public class SalvageShip : MonoBehaviour {

	protected Vector2 thrust;

	protected bool thrusting;
	
	void Start () {

	}

	void Update () {
		if (thrusting) {
			rigidbody2D.AddForce (thrust);
		}

		if (Input.GetMouseButtonDown (0)) {
			Vector3 projectedMousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			thrust = (Vector2)projectedMousePoint - rigidbody2D.position;
			thrust.Normalize();
			thrust *= 2;
			thrusting = true;
		}

		if (Input.GetMouseButtonUp (0)) {
			thrusting = false;
		}
	}

	void FixedUpdate() {
		if (thrusting) {
			float angle = Vector2.Angle (Vector2.up, thrust);
			if (Vector3.Cross (Vector2.up, thrust).z < 0) {
				angle = 360 - angle;
			}
			rigidbody2D.MoveRotation (angle);
		}
	}

}
