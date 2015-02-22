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

			thrust = new Vector2(projectedMousePoint.x, projectedMousePoint.y) - rigidbody2D.position;
			thrust.Normalize();
			thrust.x *= 2f;
			thrust.y *= 2f;
			thrusting = true;
		}

		if (Input.GetMouseButtonUp (0)) {
			thrusting = false;
		}
	}

}
