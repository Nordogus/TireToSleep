using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class InputConfigName
{
    public string horizontalAxis;
    public string verticalAxis;
    public string aButton;
    public string xButton;
}

public class PlayerController : MonoBehaviour
{
	private enum ControllerMode
	{
		Normal,
		Mirror,
		Lever,
        Ladder
	}

	private ControllerMode controllerMode = ControllerMode.Normal;
    private Rigidbody rb;
	private bool hasABattery;
    private bool isUsingObject;
    private float moveInputX;
    private float moveInputY;
    private Animator animator;
    private SpriteRenderer sprite;

    [SerializeField] private InputConfigName inputConfig = new InputConfigName();

    [Header("Ref")]
	[SerializeField] private MirorController mirrorController;
	private Lever lever;

	[Header("Movement")]
	[SerializeField] private float speed;
	[SerializeField] private float climbingSpeed;

	[Header("Push")]
	[SerializeField] private float raycastDistance;
	[SerializeField] private LayerMask whatIsGround;
	[SerializeField] private float maxDisV;
	private Transform CrateT;
	private bool canRotateSprite = true;

	[Header("Climbing")]
	[SerializeField] private GameObject topPlayer;
	[SerializeField] private GameObject bottomPlayer;
	private RaycastHit crate;
	private float? dis;
	private bool canPush;

	// Start is called before the first frame update
	void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
	{
        moveInputX = Input.GetAxisRaw(inputConfig.horizontalAxis);
        moveInputY = Input.GetAxisRaw(inputConfig.verticalAxis);
        animator.SetFloat("SpeedX", Mathf.Abs(moveInputX));

        switch (controllerMode)
		{
			case ControllerMode.Normal:
				animator.SetBool("isClimbing", false);
				rb.velocity = new Vector3(moveInputX * speed * Time.deltaTime, rb.velocity.y, 0);
				if (canRotateSprite)
				{
					if (moveInputX < -0.2f)
						sprite.flipX = true;
					else if (moveInputX > 0.2f)
						sprite.flipX = false;
				}
                break;

			case ControllerMode.Mirror:
				if (moveInputX > 0)
				{
					mirrorController.stateAction = MirorController.InstructMirror.Right;
				}
				else if (moveInputX < 0)
				{
					mirrorController.stateAction = MirorController.InstructMirror.Left;
				}
				else
				{
					mirrorController.stateAction = MirorController.InstructMirror.Null;
				}
				break;

			case ControllerMode.Lever:
				if (moveInputY > 0.7f)
				{
					lever.On();
				}
				else if (moveInputY < -0.7f)
				{
					lever.Off();
				}
				break;

            case ControllerMode.Ladder:
				animator.SetBool("isClimbing", true);
				rb.velocity = new Vector3(0, moveInputY * (-climbingSpeed) * Time.deltaTime, 0);
                animator.SetFloat("SpeedY", Mathf.Abs(moveInputY));
                rb.useGravity = false;

				//anim
				animator.SetBool("isClimbing", true);

                break;

            default:
				throw new System.InvalidOperationException($"{controllerMode} isn't suppoeted in switch");
		}

		// Check if we can push a crate
		
		if (CrateT != null && isUsingObject)
		{
			Vector3 disV = CrateT.position - transform.position;

			if (disV.sqrMagnitude > maxDisV * maxDisV)
			{
				CrateT = null;
				canPush = false;
			}

		}

		if (CrateT == null || !isUsingObject)
		{
			canPush = false;
			if (Physics.Raycast(transform.position, Vector3.left, out crate, raycastDistance, whatIsGround))
			{
				if (crate.collider != null)
				{
					if (crate.collider.gameObject.tag == "Crate")
					{
						CrateT = crate.collider.gameObject.transform;
						canPush = true;
					}
				}
			}
			else if (Physics.Raycast(transform.position, Vector3.right, out crate, raycastDistance, whatIsGround))
			{
				if (crate.collider != null)
				{
					if (crate.collider.gameObject.tag == "Crate")
					{
						CrateT = crate.collider.gameObject.transform;
						canPush = true;
					}
				}
			}
		}

		// Push
		if (Input.GetButton(inputConfig.xButton) &&  canPush)
		{
			isUsingObject = true;
			canRotateSprite = false;

			AudioManager.instance.Play("wheelBox");
			dis = dis ?? CrateT.position.x - transform.position.x;
			CrateT.position = new Vector3(transform.position.x + (float)dis, CrateT.position.y, 0);

			animator.SetBool("isInterracting", true);
			if (sprite.flipX) // facing left
			{
				if (rb.velocity.x <= 0)
				{
					animator.SetBool("isPushing", true);
					animator.SetBool("isDragging", false);
				}
				else
				{
					animator.SetBool("isPushing", false);
					animator.SetBool("isDragging", true);
				}
			}
			else // facing right
			{
				if (rb.velocity.x >= 0)
				{
					animator.SetBool("isPushing", true);
					animator.SetBool("isDragging", false);
				}
				else
				{
					animator.SetBool("isPushing", false);
					animator.SetBool("isDragging", true);
				}
			}
		}
		else
		{
			isUsingObject = false;
			canRotateSprite = true;
			animator.SetBool("isPushing", false);
			animator.SetBool("isDragging", false);
			animator.SetBool("isInterracting", false);
			AudioManager.instance.Stop("wheelBox");
            dis = null;
			CrateT = null;
		}

        if (Input.GetButtonDown(inputConfig.xButton) && controllerMode == ControllerMode.Ladder)
        {
            Physics.IgnoreLayerCollision(11, 12, false);
            controllerMode = ControllerMode.Normal;
            rb.useGravity = true;
            Debug.Log("cancel ladder");
        }
    }

	private void UseBattery()
	{
		hasABattery = false;
		mirrorController.doAction = true;
	}

	private void PickUp(GameObject go)
	{
		Destroy(go);
        AudioManager.instance.Play("batteryPick");
		hasABattery = true;
	}

	private void OnTriggerStay(Collider other)
	{
		// when player presse on A button
		if (Input.GetButtonDown(inputConfig.xButton))
		{
			if (isUsingObject == false)
            {
                if (other.gameObject.tag == "Switch")
                {
					isUsingObject = true;
                    AudioManager.instance.Play("clickBouton");

					Debug.Log("turn on light to win");
                    Switch.instance.TurnOnLight();
                }
                else if (other.gameObject.tag == "Battery")
				{
					Rigidbody orb = other.gameObject.GetComponent<Rigidbody>();

                    if (orb.isKinematic)
                    {
                        orb.isKinematic = false;
                        AudioManager.instance.Play("batterySound");
                    }
                    else
                    {
                        PickUp(orb.gameObject);
                    }
                }
				else if (other.gameObject.tag == "Lever")
				{
					isUsingObject = true;
					lever = other.gameObject.GetComponent<Lever>();
					controllerMode = ControllerMode.Lever;
				}
				else if (other.gameObject.tag == "Mirror")
				{
					isUsingObject = true;
					controllerMode = ControllerMode.Mirror;
				}
			}
		}
		else if (Input.GetButtonDown(inputConfig.aButton))
		{
			if (!isUsingObject)
			{
				 if ((other.gameObject.tag == "Ladder" || other.gameObject.tag == "CratePosition") && other.gameObject.name != "TopCrate")
				{

					if (other.gameObject.name == "BottomLadder")
						ApplyDiffPosition(CheckDiffPosition(other.gameObject.transform.position, bottomPlayer), other.gameObject.tag);
					else if (other.gameObject.name == "TopLadder")
						ApplyDiffPosition(CheckDiffPosition(other.gameObject.transform.position, topPlayer), other.gameObject.tag);
					controllerMode = ControllerMode.Ladder;
					Physics.IgnoreLayerCollision(11, 12, true);
					Debug.Log("apply ladder");
				}
			}
		}

		// when player release A button
		else if (Input.GetButtonUp(inputConfig.xButton))
		{
			//reset variables
			lever = null;
			mirrorController.stateAction = MirorController.InstructMirror.Null;

			isUsingObject = false;
			controllerMode = ControllerMode.Normal;
		}
    }


    private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Mirror")
		{
			if (hasABattery)
			{
				UseBattery();
			}
		}

        if ((other.gameObject.tag == "Ladder" || other.gameObject.tag == "CratePosition") && controllerMode == ControllerMode.Ladder)
        {
            if (other.gameObject.name == "TopLadder" || other.gameObject.name == "TopCrate")
            {
                ApplyDiffPosition(CheckDiffPosition(other.gameObject.transform.position, bottomPlayer), other.gameObject.name);
            }
            Physics.IgnoreLayerCollision(11, 12, false);
            controllerMode = ControllerMode.Normal;
            rb.useGravity = true;
        }
    }

	private Vector3 CheckDiffPosition(Vector3 ladderPosition, GameObject PlayerPointPosition)
    {
        float diffX = ladderPosition.x - PlayerPointPosition.transform.position.x;
        float diffY = ladderPosition.y - PlayerPointPosition.transform.position.y;
        float diffZ = ladderPosition.z - PlayerPointPosition.transform.position.z;
        return (new Vector3(diffX, diffY, diffZ));
    }

	private void ApplyDiffPosition(Vector3 diff, string tag)
    {
        if (tag == "CratePosition")
            transform.position = new Vector3(transform.position.x + diff.x, transform.position.y + diff.y, transform.position.z + diff.z);
        else // ladder
            transform.position = new Vector3(transform.position.x + diff.x, transform.position.y + diff.y, 0);
    }
}
