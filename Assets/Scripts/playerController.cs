using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class playerController : MonoBehaviour {

    [SerializeField]
    private int maxSpeed;

    [SerializeField]
    private ParticleSystem p;

    private float acceleration = 5;
    public static float currentSpeed = 0;

    private int buttonCount = 0;
    private float buttonCooler = 0.5f;

    private float InitialTouch;

    // Use this for initialization
    void Start () {
        p.Pause();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(0))
        {
            currentSpeed += acceleration * Time.deltaTime;

            if(currentSpeed > maxSpeed)
            {
                currentSpeed = maxSpeed;
            }

            transform.position = transform.position + Camera.main.transform.forward * currentSpeed * Time.deltaTime;

            p.Play();
            p.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 20;
            p.transform.LookAt(Camera.main.transform);
        }
        else
        {
            p.Pause();
            p.Clear();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (Time.time < InitialTouch + 0.5f)
            {
                Debug.Log("DoubleTouch");
                SceneManager.LoadScene("mainMenu");
            }

            InitialTouch = Time.time;
        }

        if(buttonCooler > 0)
        {
            buttonCooler -= 1 * Time.deltaTime;
        }
        else
        {
            buttonCount = 0;
        }

    }
}
