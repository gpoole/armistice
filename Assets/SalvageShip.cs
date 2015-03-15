using UnityEngine;
using System.Collections;
using System;

public class SalvageShip : MonoBehaviour {

	protected Vector2 thrust;

	protected bool thrusting;
	
	public float fuel;

	public float maxFuel;

	public float damage;

	public float maxDamage;

	public Sprite normalSprite;

	public Sprite burnSprite;

	public GameObject attachedTo;

	protected GameObject tractorBeam;

	protected ParticleSystem smokeEffect;

	protected SpriteRenderer spriteRenderer;

	void Start () {
		smokeEffect = GameObject.Find ("WhiteSmoke").GetComponent<ParticleSystem>();
		spriteRenderer = GetComponent<SpriteRenderer> ();

		tractorBeam = GameObject.Find ("TractorBeam");
	}

	void Update () {

		// Show effects and apply forces depending on whether we're thrusting or not
		if (thrusting && fuel > 0) {
			// Actually apply the thrust force
			rigidbody2D.AddForce (thrust);
			fuel -= .025f * thrust.magnitude;

			if(!smokeEffect.isPlaying) {
				smokeEffect.Play();
			}
			spriteRenderer.sprite = burnSprite;
		} else {
			if(smokeEffect.isPlaying) {
				smokeEffect.Stop();
			}
			spriteRenderer.sprite = normalSprite;
		}

		SpringJoint2D joint = GetComponent<SpringJoint2D> ();

		if (attachedTo) {
			float distanceToAttachment = Vector3.Distance (attachedTo.transform.position, transform.position);

			if (!tractorBeam.particleSystem.isPlaying) {
				tractorBeam.particleSystem.Play ();
			}

			if (!joint.enabled) {
				joint.enabled = true;
				joint.connectedBody = attachedTo.rigidbody2D;
				joint.distance = distanceToAttachment;
			}

			tractorBeam.transform.position = new Vector3 (attachedTo.transform.position.x, attachedTo.transform.position.y, tractorBeam.transform.position.z);
			tractorBeam.transform.LookAt (transform.position);
			tractorBeam.particleSystem.startLifetime = distanceToAttachment / tractorBeam.particleSystem.startSpeed;
		} else {
			if(tractorBeam.particleSystem.isPlaying) {
				tractorBeam.particleSystem.Stop();
			}

			if(joint.enabled) {
				joint.enabled = false;
			}
		}
		 
		// Press left mouse button
		if (Input.GetMouseButtonDown (0)) {
			// Calculate where the mouse was clicked in the game world
			Vector3 projectedMousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			// Produce a vector that points in the direction of the mouse relative to the ship, with the magnitude
			// based on how far away the mouse is (up to 2 units)
			thrust = Vector2.ClampMagnitude((Vector2)projectedMousePoint - rigidbody2D.position, 2f);
			thrusting = true;

			rigidbody2D.angularVelocity = 0;
		}

		// Stop thrusting when left button is released
		if (Input.GetMouseButtonUp (0)) {
			thrusting = false;
		}

		if (Input.GetKey (KeyCode.Space)) {
			attachedTo = GameObject.Find ("ship_piece_4");
		} else {
			attachedTo = null;
		}
	}

	void FixedUpdate() {
		if (thrusting) {
			// Figure out the direction of the thrust vector by calculating the angle between it and a vector pointing
			// straight up. This calculation only works up to 180 degrees, so we have to figure out which direction the thrust vector is pointing
			// relative to the "up" vector by using the cross product.
			float angle = Vector2.Angle (Vector2.up, thrust);
			if (Vector3.Cross (Vector2.up, thrust).z < 0) {
				angle = 360 - angle;
			}
			rigidbody2D.MoveRotation (angle);
		}
	}

}
