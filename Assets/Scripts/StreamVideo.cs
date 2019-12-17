using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour
{
    public RawImage rawImage;
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;

    public MenuManager menu;

    public static StreamVideo instance;

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
        if (!menu)
        {
            menu = FindObjectOfType<MenuManager>();
        }
    }

    public void Go()
    {
        StartCoroutine("PlayVideo");
        StartCoroutine("VideoEnd");
    }

    public IEnumerator PlayVideo()
    {
        videoPlayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        while (!videoPlayer.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }
        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
        rawImage.color = Color.white;
    }
    public IEnumerator VideoEnd()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        while (!((ulong)videoPlayer.frame == videoPlayer.frameCount - 1))
        {
            yield return waitForSeconds;
        }
        rawImage.color = Color.black;
        rawImage.texture = null;
        menu.LoadScene(1);
    }
}
