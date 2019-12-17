using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSound : MonoBehaviour
{
    
    public string[] doggoSounds = new string[4];

    public float timeBeforeNewDoggoMin = 1f;
    public float timeBeforeNewDoggoMax = 10f;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.Play("musicBG");
        AudioManager.instance.Play("rainBG");
        StartCoroutine("Doggo");
    }

    IEnumerator Doggo()
    {
        AudioManager.instance.Play(doggoSounds[UnityEngine.Random.Range(0, doggoSounds.Length)]);

        yield return new WaitForSeconds(UnityEngine.Random.Range(timeBeforeNewDoggoMin, timeBeforeNewDoggoMax));

        StartCoroutine("Doggo");
    }
}
