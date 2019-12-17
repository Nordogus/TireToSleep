using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Option
{
    public string language = "en";
    public int volume = 100;
    public float resolutionLight = .1f;
}

public class GameManager : MonoBehaviour
{
    public MenuManager menuManager;

    public string language = "en";
    public int volume = 100;
    public float resolutionLight = .1f;

    public Option option;

    public static GameManager instance;

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
        Time.timeScale = 1;

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        option.language = language;
        option.volume = 100;
        resolutionLight = .1f;

        LoadSave();

        if (!menuManager)
        {
           menuManager = FindObjectOfType<MenuManager>();
        }

        menuManager.StartManager();
    }

    private void LoadSave()
    {
        if (SaveLoad.SaveExists("Option"))
        {
            option = SaveLoad.Load<Option>("Option");
        }
        else
        {
            SaveLoad.Save<Option>(option, "Option");
        }
    }
}
