using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenControler : MonoBehaviour
{
    [SerializeField] private List<GameObject> canvas = new List<GameObject>();

    public static ScreenControler instance;
    
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

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject canva in canvas)
        {
            canva.SetActive(false);
        }
    }

    public void BackMenu()
    {
        AudioManager.instance.Play("clicBouton");

        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void LoadScene(int idScene)
    {
        AudioManager.instance.Play("clicBouton");
        Time.timeScale = 1;

        if (idScene < 0)
        {
            SceneManager.LoadScene(0);
        }
        SceneManager.LoadScene(idScene);
    }

    public void OpenCanvas(string _name)
    {

        foreach (GameObject item in canvas)
        {
            if (item.name == _name)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
