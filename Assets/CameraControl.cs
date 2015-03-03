using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	protected GameObject target;

	protected static float DefaultZoom = -10f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// Find our active ship
		target = GameObject.FindWithTag ("Player");

		// Just follow the player around everywhere
		if (transform.position != target.transform.position) {
//			Debug.Log ((DefaultZoom * (target.rigidbody2D.velocity.magnitude / 8f)));
			transform.position = new Vector3(target.transform.position.x, target.transform.position.y, DefaultZoom);
		}
	}
}
