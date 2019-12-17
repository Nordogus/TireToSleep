using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
	[SerializeField] private Platform platform;
	private bool isAcitve;

	public void On()
	{
		if (!isAcitve)
		{
			platform.Move();
			isAcitve = true;
		}
	}
	public void Off()
	{
		if (isAcitve)
		{
			platform.Stop();
			isAcitve = false;
		}
	}
}
