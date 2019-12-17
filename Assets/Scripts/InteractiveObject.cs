using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    static protected GameObject player;
    private BoxCollider colliderObject;
    private bool isColliding;
    private bool isUsingObject;

    // Start is called before the first frame update
    void Awake()
    {
        colliderObject = GetComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isColliding = true;
            player = collision.gameObject;
        }
        Debug.Log(collision.gameObject.name);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isColliding = false;
            player = null;
        }
        Debug.Log(collision.gameObject.name);
    }

    
}
