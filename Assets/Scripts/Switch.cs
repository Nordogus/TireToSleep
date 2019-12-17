using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public static Switch instance;

    [SerializeField] private bool lightOn = false;
    [SerializeField] private GameObject winScreen;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void TurnOnLight()
    {
        lightOn = true;

        ScreenControler.instance.OpenCanvas("WinCanvas");
    }

    private void Update()
    {
        if (lightOn)
        {
            Time.timeScale = 0;
            winScreen.SetActive(true);
        }
    }
}
