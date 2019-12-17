using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    [SerializeField] private float distanceHitGround = 1f;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(transform.position, Vector3.down, out hit, distanceHitGround);

        if (hit.collider.gameObject.tag == "ground")
        {
            AudioManager.instance.Play("batterySound");
            Destroy(this);
        }
    }


}
