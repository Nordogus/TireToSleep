using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    [SerializeField] private GameObject thunderLight;

    public bool activeThunder = false;
    [SerializeField] private float distanceEnemyPlayer = 1f;
    [SerializeField] private float distanceOfDeath = .1f;
    private List<GameObject> enemys = new List<GameObject>();

    [SerializeField] private float rangeOfThunderMax = 0f;
    [SerializeField] private float rangeOfThunderMin = 1f;
    [SerializeField] private float timeOfThunder = .5f;
    [SerializeField] private float difTimeBetwinThunderAndLightMin = .5f;
    [SerializeField] private float difTimeBetwinThunderAndLightMax = 1;

    private bool thunderAgaine = true;

    public static Thunder instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject enemy in enemys)
        {
            if (Vector3.Distance(enemy.transform.position, gameObject.transform.position) < distanceOfDeath)
            {
                Debug.Log("end game");
            }
            if (Vector3.Distance(enemy.transform.position, gameObject.transform.position) < distanceEnemyPlayer)
            {
                StartCoroutine(DoThunder(Random.Range(rangeOfThunderMin, rangeOfThunderMax)));
            }
        }

        if (activeThunder && thunderAgaine)
        {
            activeThunder = false;
            thunderAgaine = false;

            StartCoroutine(DoThunder(Random.Range(rangeOfThunderMin, rangeOfThunderMax)));
        }
    }

    public void AddInEnemies(GameObject _gameObject)
    {
        enemys.Add(_gameObject);
    }

    IEnumerator DoThunder(float delai)
    {
        yield return new WaitForSeconds(delai);
        AudioManager.instance.Play("Thunder");

        yield return new WaitForSeconds(Random.Range(difTimeBetwinThunderAndLightMin, difTimeBetwinThunderAndLightMax));


        thunderLight.transform.position = new Vector3(0, 0, -9);


        yield return new WaitForSeconds(timeOfThunder);

        thunderLight.transform.position = new Vector3(0, 0, -12);
        thunderAgaine = true;
    }
}
