using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour {

	[SerializeField] private float jumpForce = 520f;
	[Range(0, 0.45f)] [SerializeField] private float movementSmoothing = 0.65f;
	[SerializeField] private bool airControl = false;

	[SerializeField] private LayerMask ground;
	[SerializeField] private Transform groundCheck;

	const float groundedRadius = .3f;
	private bool isGrounded;
	private bool facingRight = true;

	private Vector3 velocity;

	private Rigidbody2D rb2D;

	public UnityEvent IsOnLandEvent;

	void Awake ()
	{
		rb2D = GetComponent<Rigidbody2D>();

		if (IsOnLandEvent == null)
		{
			IsOnLandEvent = new UnityEvent();
		}
	}

	void FixedUpdate ()
	{
		bool wasGrounded = isGrounded;
		isGrounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, ground);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				isGrounded = true;

				if (!wasGrounded)
					IsOnLandEvent.Invoke();
			}
		}
	}


	public void Move (float direction, bool isJumping)
	{
		if (isGrounded || airControl)
		{
			Vector3 targetVel = new Vector2(direction * 10f, rb2D.velocity.y);
			rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, targetVel, ref velocity, movementSmoothing);

			if (direction > 0 && !facingRight)
			{
				FlipFacing();
			}
			else if (direction < 0 && facingRight)
			{
				FlipFacing();
			}
		}

		if (isGrounded && isJumping)
		{
			isGrounded = false;
			rb2D.AddForce(new Vector2(0f, jumpForce));
		}
	}

	private void FlipFacing ()
	{
		facingRight = !facingRight;

		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

}