using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MirorController : MonoBehaviour
{
    public enum InstructMirror
    {
        Null,
        Right,
        Left
    }

    public float rotationSpeed = 25;
    public float rotation = 0;
    public float maxRotation = 60;

    public InstructMirror stateAction { get; set; }

    [SerializeField]
    private float maxViewRadius = 20;
    [SerializeField]
    private float mediumViewRadius = 10;
    [SerializeField]
    private float speedIncreaceRadiusMax = 1;
    [SerializeField]
    private float speedIncreaceRadiusMedium = 3;
    
    [SerializeField]
    private FieldOfView miror;

    [SerializeField]
    private float levelAddBattery = 3;

    public bool doAction = false;

    private bool levelBatryDemiPast = false;

    [SerializeField] private GameObject gameOverCanvas;

    // Start is called before the first frame update
    void Start()
    {
        if (!miror)
        {
            miror = FindObjectOfType<FieldOfView>();
        }
    }

    void Update()
    {
        if (stateAction == InstructMirror.Right || Input.GetKey(KeyCode.D))
        {
            rotation += rotationSpeed * Time.deltaTime;
            if (rotation > maxRotation)
            {
                rotation = maxRotation;
            }
        }
        else if (stateAction == InstructMirror.Left || Input.GetKey(KeyCode.A))
        {
            rotation -= rotationSpeed * Time.deltaTime;
            if (rotation < -maxRotation)
            {
               rotation = -maxRotation;
            }
        }

        Rotator();

        if (miror.viewRadius > mediumViewRadius)
        {
            miror.viewRadius -= speedIncreaceRadiusMax * Time.deltaTime;
            levelBatryDemiPast = true;
        }
        else if(miror.viewRadius > 0)
        {
            miror.viewRadius -= speedIncreaceRadiusMedium * Time.deltaTime;
            if (levelBatryDemiPast)
            {
                levelBatryDemiPast = false;
                AudioManager.instance.Play("boyHalf");
            }
        }
        else
        {
            AudioManager.instance.Play("boyNone");

            miror.viewRadius = 0;
            Debug.Log("view radius 0");
            gameOverCanvas.SetActive(true);
        }

        addBattery();

    }

    public void addBattery()
    {
        if (doAction)
        {
            AudioManager.instance.Play("batteryCharge");

            miror.viewRadius += levelAddBattery;
            if (miror.viewRadius > maxViewRadius)
            {
                miror.viewRadius = maxViewRadius;
            }
            doAction = false;
        }
    }

    private void Rotator()
    {
        transform.rotation = Quaternion.Euler(0, rotation, 0);
    }
}