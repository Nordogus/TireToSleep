using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
	[SerializeField] private Vector2 endPos;
	[SerializeField] private float maxTime;
	[SerializeField, Range(0,1)] private float delay;
	[Range(0,1)]  private float ratio;
	private bool addVelocityToPlayer;
	private bool initialDirection;
	private float time = 0.0f;
	private Vector3 startPos;

	private void Awake()
	{
		startPos = transform.position;
	}

	public void Move()
	{
		StartCoroutine(MovementAuto());
	}
	public void Stop()
	{
		StopAllCoroutines();
	}

	private void Update()
	{
		transform.position = Vector3.Lerp(startPos, endPos, ratio);
	}

	IEnumerator MovementAuto()
	{
		if (initialDirection)
		{
			while (time <= maxTime)
			{
				time += Time.deltaTime;
				ratio = 1 * (time / maxTime);
				yield return new WaitForEndOfFrame();
			}
		}
		else
		{
			while (time >= 0.0f)
			{
				time -= Time.deltaTime;
				ratio = 1 * (time / maxTime);
				yield return new WaitForEndOfFrame();
			}
		}

		initialDirection = !initialDirection;

		yield return new WaitForSeconds(delay);

		StartCoroutine(MovementAuto());
	}

	IEnumerator MovementSequencial()
	{
		if (initialDirection)
		{
			while (time <= maxTime)
			{
				time += Time.deltaTime;
				ratio = 1 * (time / maxTime);
				yield return new WaitForEndOfFrame();
			}
		}
		else
		{
			while (time >= 0.0f)
			{
				time -= Time.deltaTime;
				ratio = 1 * (time / maxTime);
				yield return new WaitForEndOfFrame();
			}
		}

		initialDirection = !initialDirection;
	}

	void OnDrawGizmosSelected()
	{
		// Draw a semitransparent blue cube at the transforms position
		Gizmos.color = new Color(1, 0, 0, 0.5f);
		Gizmos.DrawCube(endPos, new Vector3(1, 1, 1));
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Crate")
		{
			collision.transform.parent = transform;
		}
	}
	private void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Crate")
		{
			collision.transform.parent = null;
		}
	}
}
