using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    public bool playAllSound = false;
    public bool stopAllSound = false;

    public List<string> listeSoundToPlay = new List<string>();

    // Update is called once per frame
    void Update()
    {
        if (playAllSound)
        {
            playAllSound = false;
            foreach (string sound in listeSoundToPlay)
            {
                AudioManager.instance.Play(sound);
            }
        }

        if (stopAllSound)
        {
            stopAllSound = false;
            foreach (string sound in listeSoundToPlay)
            {
                AudioManager.instance.Stop(sound);
            }
        }
    }
}
