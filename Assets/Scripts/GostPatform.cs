using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GostPatform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GostPatformManager.instance.AddGostPlatform(gameObject);
    }
}
