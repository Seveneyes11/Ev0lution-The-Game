using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	// Getting a reference of the player controller
	public PlayerController controller;

	public float speed = 60f;

	float horiMove = 0f;
	bool isJumping = false;

	// This here is called every frame
	void Update ()
	{
		// Getting the input for the A & D keys
		horiMove = Input.GetAxisRaw("Horizontal") * speed;

		// Checking if the player have pressed the space key
		if (Input.GetButtonDown("Jump") || Input.GetKey("w"))
		{
			// Jumping
			isJumping = true;
		}
	}

	// This functiom 
	void FixedUpdate ()
	{
		// Moving
		controller.Move(horiMove * Time.fixedDeltaTime, false);
		isJumping = false;
	}
	
}