using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {

	[SerializeField] private float jumpForce = 520f;
	[Range(0, 0.45f)] [SerializeField] private float movementSmoothing = 0.65f;
	[SerializeField] private bool airControl = false;

	[SerializeField] private LayerMask ground;
	[SerializeField] private Transform groundCheck;

    public Camera cam;

	const float groundedRadius = .3f;
	private bool isGrounded;
	private bool facingRight = true;

	private Vector3 velocity;

	private Rigidbody2D rb2D;

	public UnityEvent IsOnLandEvent;

	public Interactable focus;

	void Awake ()
	{
		rb2D = GetComponent<Rigidbody2D>();

		if (IsOnLandEvent == null)
		{
			IsOnLandEvent = new UnityEvent();
		}
	}

	void Update ()
	{
		if (EventSystem.current.IsPointerOverGameObject())
			return;

		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100))
			{
				RemoveFocus();
			}
		}

		if (Input.GetMouseButtonDown(1))
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100))
			{
				Interactable interactable = hit.collider.GetComponent<Interactable>();
				if (interactable != null)
				{
					SetFocus(interactable);
				}
			}
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

	void SetFocus (Interactable newFocus)
	{
		if (newFocus != focus)
		{
			if (focus != null)
				focus.OnDefocused();
			
			focus = newFocus;

			// Open info UI about the object. Maybe this should be outside the if loop!
		}

		newFocus.OnFocused(transform);
	}

	void RemoveFocus ()
	{
		if (focus != null)
			focus.OnDefocused();

		focus = null;

		// Close the info UI
	}

}