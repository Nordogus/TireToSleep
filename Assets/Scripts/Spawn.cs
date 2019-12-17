using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
	[SerializeField] private GameObject pneu;
	[SerializeField] private float delay = 30.0f;
	private float t;

	private void Update()
	{
		t += Time.deltaTime;

		if (t >= delay)
		{
			SpawnObj();
			t = 0.0f;
		}
	}

	private void SpawnObj()
	{
		Instantiate(pneu, transform.position, Quaternion.identity); 
	}
}
