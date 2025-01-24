using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeDetection : MonoBehaviour
{

	public static SwipeDetection instance;
	public delegate void Swipe(Vector2 direction);
	public event Swipe swipePerformed;


	[SerializeField] private InputAction position, press;
	[SerializeField] private float swipeResistance = 10;

	private Vector2 initialPos;
	private Vector2 currentPos => position.ReadValue<Vector2>();


	private void Awake()
	{
		position.Enable();
		press.Enable();

		press.performed += _ => { initialPos = currentPos; };
		press.canceled += _ => DetectSwipe();
		instance = this;
	}

	private void DetectSwipe()
	{
		Vector2 delta = currentPos - initialPos;
		//Vector2 direction = Vector2.zero;

		if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
		{
			// Horizontal swipe
			if (delta.x > 0)
				TriggerSwipe(Vector2.right); // Right swipe
			else
				TriggerSwipe(Vector2.left); // Left swipe
		}
		else
		{
			// Vertical swipe
			if (delta.y > 0)
				TriggerSwipe(Vector2.up); // Up swipe
			else
				TriggerSwipe(Vector2.down); // Down swipe
		}
	}

	private void TriggerSwipe(Vector2 direction)
	{
		if (swipePerformed != null)
		{
			swipePerformed(direction);
		}

		Debug.Log($"Swipe Detected: {direction}");
	}

	private void OnDisable()
	{
		position.Disable();
		press.Disable();
	}
}
