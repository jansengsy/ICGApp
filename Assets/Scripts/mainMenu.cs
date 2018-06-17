using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class mainMenu : MonoBehaviour {

    [SerializeField]
    private Sprite notSelected;

    [SerializeField]
    private Sprite selected;

    [SerializeField]
    private GameObject about;

    [SerializeField]
    private GameObject exit;

    [SerializeField]
    private GameObject start;

    [SerializeField]
    private GameObject options;

    [SerializeField]
    private AudioSource audio_src;

    private Image A, E, S, O;

    private bool clicked = false;

    private void Start()
    {
        A = about.GetComponent<Image>();
        E = exit.GetComponent<Image>();
        S = start.GetComponent<Image>();
        O = options.GetComponent<Image>();

        StreamReader reader = new StreamReader(new MemoryStream((Resources.Load("first") as TextAsset).bytes));

        while (!reader.EndOfStream)
        {
            string input = reader.ReadLine();

            if (input == "0")
            {
                System.IO.File.WriteAllText(Application.dataPath + "/Resources/first.txt", "1");
                SceneManager.LoadScene("about");
            }
        }
    }

    void Update()
    { 
        
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        RaycastHit hit;

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            GameObject btn = hit.transform.gameObject;

            if (btn.name == "Start")
            {
                clickPlayer();
                S.sprite = selected;

                if (Input.GetMouseButton(0))
                {
                    SceneManager.LoadScene("main");
                }
            } 
            else if (btn.name == "About")
            {
                clickPlayer();
                A.sprite = selected;

                if (Input.GetMouseButton(0))
                {
                    SceneManager.LoadScene("about");
                }
            }
            else if (btn.name == "Options")
            {
                clickPlayer();
                O.sprite = selected;

                if (Input.GetMouseButton(0))
                {
                    SceneManager.LoadScene("options");
                }
            }
            else if (btn.name == "Exit")
            {
                clickPlayer();
                E.sprite = selected;

                if (Input.GetMouseButton(0))
                {
                    Application.Quit(); 
                }
            }
        } 
        else
        {
            clicked = false;
            A.sprite = notSelected;
            E.sprite = notSelected;
            S.sprite = notSelected;
            O.sprite = notSelected;
        }
    }

    void clickPlayer()
    {
        if (OptionsMenu.playSound && !clicked)
        {
            audio_src.Play();
            clicked = true;
        }
    }

    IEnumerator LateCall(GameObject o)
    {
        yield return new WaitForSeconds(0.5f);
        o.GetComponent<BoxCollider>().isTrigger = true;
    }
}
