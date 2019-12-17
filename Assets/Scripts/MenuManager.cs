using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject canvasTitle;
    public List<GameObject> canvasList;

    //fade variables
    public RawImage cache;
    private Color tmpColor;
    public float speedFade = 1;
    public bool inFadeOut = false;
    public bool inFadeIn = false;

    private bool[] bValidPerso = new bool[3];

    private void Awake()
    {
        canvasTitle.SetActive(false);
        cache.gameObject.SetActive(true);
    }

    public void StartManager()
    {
        tmpColor.a = 1;
        canvasTitle.SetActive(true);
        inFadeOut = true;

        foreach (GameObject item in canvasList)
        {
            item.SetActive(false);
        }
        ActivateCanvas(canvasList[0]);
    }

    private void Update()
    {
        Faid();
    }

    public void ActivateCanvas(GameObject _canvas)
    {
        AudioManager.instance.Play("clicBouton");

        foreach (GameObject item in canvasList)
        {
            item.SetActive(false);
        }

        _canvas.SetActive(true);
    }

    public void UnactiveCanvas(GameObject _canvas)
    {
        _canvas.SetActive(false);
    }
    
    private void Faid()
    {
        if (inFadeIn)
        {
            FadeIn();
        }
        else if (inFadeOut)
        {
            FadeOut();
        }
    }

    private void FadeIn()
    {
        tmpColor.a += speedFade * Time.deltaTime;
        cache.color = tmpColor;

        if (tmpColor.a >= 1)
        {
            inFadeIn = false;
        }
    }

    private void FadeOut()
    {
        tmpColor.a -= speedFade * Time.deltaTime;
        cache.color = tmpColor;
        if (tmpColor.a <= 0)
        {
            inFadeOut = false;
        }
    }

    public void StartGame()
    {
        canvasList[3].SetActive(true);
        AudioManager.instance.Play("clicBouton");
        StreamVideo.instance.Go();

    }

    public void LoadScene(int idScene)
    {
        AudioManager.instance.Play("clicBouton");

        SceneManager.LoadScene(idScene);
    }

    public void SetResolution(float resol)
    {
        GameManager.instance.resolutionLight = resol;
    }

    public void ExitGame()
    {
        AudioManager.instance.Play("clicBouton");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}
