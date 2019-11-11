﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravity : Player {

	GameObject[] planets;
	float distance;
	Collider2D collider;

	// Use this for initialization
	new void Start () {
		base.Start();

		planets = GameObject.FindGameObjectsWithTag("planet");
		distance = int.MaxValue;
		collider = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	new void Update () {
		// input
		if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) {
			rb.AddForce(new Vector2(0, jumpSpeed/2), ForceMode2D.Impulse);
			jumped++;
		}
		if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) {
			rb.AddForce(new Vector2(0, -jumpSpeed/2), ForceMode2D.Impulse);
			jumped++;
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
			rb.AddForce(new Vector2(-jumpSpeed/2, 0), ForceMode2D.Impulse);
			jumped++;
		}
		if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
			rb.AddForce(new Vector2(jumpSpeed/2, 0), ForceMode2D.Impulse);
			jumped++;
		}

		if(rb.velocity.magnitude > 10)
			rb.velocity = Vector2.ClampMagnitude(rb.velocity, 10);

        if (transform.position.y < -10) {
            transform.localPosition = new Vector2(startPos.transform.position.x, startPos.transform.position.y);
        }

		Physics.gravity = new Vector3(0f, 0f, 0f);

		float tempDistance = 0, angle = 0;
		Vector2 normal = new Vector2();

		foreach(GameObject planet in planets) {

			Collider2D planetCollider = planet.GetComponent<Collider2D>();

			tempDistance = Mathf.Abs(planetCollider.Distance(collider).distance);
			if(tempDistance < distance) {
				distance = tempDistance;
				normal = planetCollider.Distance(collider).normal;
			}
		}

		print(angle + ", " + distance);

		if(!onGround && distance > 0) {
			float force = 0.01f * Mathf.Pow(distance, 2);
			rb.AddForce(Vector2.Scale(new Vector2(force, force), normal), ForceMode2D.Impulse);
		}

		distance = int.MaxValue;
	}
}
