/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InputConfigName
{
    public string horizontalAxis;
    public string verticalAxis;
    public string aButton;
}

public class Ulysse_Player_Controller : MonoBehaviour
{
	[SerializeField] private float speed = 1f;
	[SerializeField] private float jumpForce = 1f;
    [SerializeField] private InputConfigName inputConfig = new InputConfigName();
    private float moveInput = 1f;
    private Rigidbody rb;

    
	// Jump
	[SerializeField] private bool isUsingJump;
	private bool canJump;
	[SerializeField] private LayerMask whatIsGround;
	[SerializeField] private float raycastDistance;
	[SerializeField] private List<Transform> raycastsOrigins;

	// action
	private bool isInterractingWithLever;
	private bool isInterractingWithMirror;

	// Start is called before the first frame update
	void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //control mod
        if (Input.GetButtonDown(inputConfig.aButton))
        {
            rb.velocity = new Vector3(rb.velocity.y, jumpForce, 0);
            Debug.Log("Jump");
        }

		else
		{
			// move horizontaly
			moveInput = Input.GetAxisRaw(inputConfig.horizontalAxis);
			rb.velocity = new Vector3(moveInput * speed, rb.velocity.y, 0);
		}


		if (isUsingJump)
		{
			// Check if we can Jump
			canJump = false;
			foreach (Transform originTransform in raycastsOrigins)
			{
				if (Physics.Raycast(originTransform.position, Vector3.down, raycastDistance, whatIsGround))
				{
					canJump = true;
					break;
				}
			}

			// Jump
			if (Input.GetButtonDown(inputConfig.aButton) && canJump)
			{
				rb.velocity = new Vector3(rb.velocity.y, jumpForce, rb.velocity.z);
				Debug.Log("Jump");
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Lever")
		{
			isInterractingWithLever = true;
		}
		if (other.gameObject.tag == "Mirror")
		{
			isInterractingWithMirror = true;
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Lever")
		{
			isInterractingWithLever = false;
		}
		if (other.gameObject.tag == "Mirror")
		{
			isInterractingWithMirror = false;
		}
	}
}
*/
