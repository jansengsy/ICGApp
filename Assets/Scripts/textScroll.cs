using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class textScroll : MonoBehaviour {

    [SerializeField]
    private Text aboutText;

    private float timeLeft = 22;
    // Use this for initialization
    void Start ()
    {
        //Wait(5.0f);
        aboutText.transform.position = new Vector3(0.0f, -120.0f, 150.0f);
    }
	
	// Update is called once per frame
	void Update ()
    {
        aboutText.transform.position += new Vector3(0, 0.15f, 0);

        timeLeft -= Time.deltaTime;

        if (timeLeft < 0)
        {
            keepStraight.about = false;
            SceneManager.LoadScene("mainMenu");
        }
    }

    IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }
}
