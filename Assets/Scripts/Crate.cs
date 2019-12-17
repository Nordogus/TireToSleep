using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField] private float distanceHitGround = 1f;
    private RaycastHit hit;

    private bool onAir = false;

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out hit, distanceHitGround))
        {
            if (hit.collider.gameObject.tag != null)
            {
                if (hit.collider.gameObject.tag == "ground")
                {
                    if (onAir)
                    {
                        AudioManager.instance.Play("crateFall");
                    }
                    onAir = false;

                }
                else
                {
                    onAir = true;
                }
            }
		}
    }
}
