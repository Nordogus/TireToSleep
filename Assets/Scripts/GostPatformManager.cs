using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GostPatformManager : MonoBehaviour
{
    [SerializeField] private GameObject _light;
    private FieldOfView _lightFiedOfView;
    [SerializeField] private List<GameObject> gosts = new List<GameObject>();

    public static GostPatformManager instance;

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
        if (_light == null)
        {
            _light = FindObjectOfType<FieldOfView>().gameObject;
        }

        _lightFiedOfView = _light.GetComponent<FieldOfView>();
        StartCoroutine(resetVisibiity(.1f));
    }

    public void AddGostPlatform(GameObject _object)
    {
        gosts.Add(_object);
    }

    IEnumerator resetVisibiity(float delai)
    {
        yield return new WaitForSeconds(delai);

        foreach (GameObject item in gosts)
        {
            SetVisible(item, false);
        }
        StartCoroutine(resetVisibiity(.1f));
    }

    public void SetVisible(GameObject gost, bool isActive)
    {
        gost.gameObject.GetComponent<BoxCollider>().isTrigger = !isActive;
        gost.gameObject.GetComponent<MeshRenderer>().enabled = isActive;
    }
}
