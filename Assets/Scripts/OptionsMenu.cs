using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu: MonoBehaviour
{
    [SerializeField]
    private Sprite btnOff;

    [SerializeField]
    private Sprite btnOn;

    [SerializeField]
    private Sprite btnHover;

    [SerializeField]
    private Sprite btnOnHover;

    [SerializeField]
    private Sprite btnOffHover;

    [SerializeField]
    private Sprite normal;

    [SerializeField]
    private Sprite normalHover;

    [SerializeField]
    private GameObject music;

    [SerializeField]
    private GameObject sound;

    [SerializeField]
    private GameObject lowC;

    [SerializeField]
    private GameObject medC;

    [SerializeField]
    private GameObject highC;

    [SerializeField]
    private GameObject back;

    [SerializeField]
    private GameObject exit;

    [SerializeField]
    private AudioSource audioSrc;

    private Image M, Snd, lC, mC, hC, B, E;

    public static bool low = false;
    public static bool medium = false;
    public static bool high = true;
    public static bool playMusic = true;
    public static bool playSound = true;

    private bool clicked = false;

    private void Start()
    {
        M = music.GetComponent<Image>();
        Snd = sound.GetComponent<Image>();
        lC = lowC.GetComponent<Image>();
        mC = medC.GetComponent<Image>();
        hC = highC.GetComponent<Image>();
        B = back.GetComponent<Image>();
        E = exit.GetComponent<Image>();
    }

    void Update()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        RaycastHit hit;

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            GameObject btn = hit.transform.gameObject;
            
            if (btn.name == "Music")
            {
                clickPlayer();

                if (playMusic == true)
                {
                    M.sprite = btnOnHover;
                }
                else
                {
                    M.sprite = btnOffHover;
                }

                if (Input.GetMouseButton(0))
                {
                    if (btn.GetComponent<BoxCollider>().isTrigger)
                    {
                        musicHandler.ToggleMusic();
                        btn.GetComponent<BoxCollider>().isTrigger = false;
                        StartCoroutine(LateCall(btn));
                    }
                }
            }
            else if(btn.name == "Sound")
            {
                if(playSound == true)
                {
                    clickPlayer();
                    Snd.sprite = btnOnHover;
                }
                else
                {
                    clickPlayer();
                    Snd.sprite = btnOffHover;
                }
                if (Input.GetMouseButton(0))
                {
                    if(btn.GetComponent<BoxCollider>().isTrigger)
                    {
                        playSound = !playSound;
                        btn.GetComponent<BoxCollider>().isTrigger = false;
                        StartCoroutine(LateCall(btn));
                    }
                }
            }
            else if (btn.name == "Return")
            {
                clickPlayer();
                B.sprite = normalHover;

                if(Input.GetMouseButton(0))
                {
                    SceneManager.LoadScene("mainMenu");
                }
            }
            else if(btn.name == "Exit")
            {
                clickPlayer();
                E.sprite = normalHover;

                if (Input.GetMouseButton(0))
                {
                    Application.Quit();
                }
            }
            else if (btn.name == "lowCoverage")
            {
                clickPlayer();

                if (low == true)
                {
                    lC.sprite = btnOnHover;
                    hC.sprite = btnOff;
                    mC.sprite = btnOff;
                }
                else
                {
                    lC.sprite = btnOffHover;

                    if (medium)
                    {
                        hC.sprite = btnOff;
                        mC.sprite = btnOn;
                    }
                    else if (high)
                    {
                        hC.sprite = btnOn;
                        mC.sprite = btnOff;
                    }
                }
                if (Input.GetMouseButton(0))
                {
                    low = true;
                    medium = false;
                    high = false;
                    UniverseManager.updateCoverage(150);
                }
            }
            else if (btn.name == "mediumCoverage")
            {
                clickPlayer();

                if ( medium == true)
                {
                    mC.sprite = btnOnHover;
                    hC.sprite = btnOff;
                    lC.sprite = btnOff;
                }
                else
                {
                    mC.sprite = btnOffHover;

                    if (low)
                    {
                        hC.sprite = btnOff;
                        lC.sprite = btnOn;
                    }
                    else if (high)
                    {
                        hC.sprite = btnOn;
                        lC.sprite = btnOff;
                    }
                }
                if (Input.GetMouseButton(0))
                {
                    low = false;
                    medium = true;
                    high = false;
                    UniverseManager.updateCoverage(300);
                }
            }
            else if (btn.name == "highCoverage")
            {
                clickPlayer();

                if (high == true)
                {
                    hC.sprite = btnOnHover;
                    lC.sprite = btnOff;
                    mC.sprite = btnOff;
                }
                else
                {
                    hC.sprite = btnOffHover;

                    if(low)
                    {
                        mC.sprite = btnOff;
                        lC.sprite = btnOn;
                    }
                    else if(medium)
                    {
                        mC.sprite = btnOn;
                        lC.sprite = btnOff;
                    }
                }
                if (Input.GetMouseButton(0))
                {
                    low = false;
                    medium = false;
                    high = true;
                    UniverseManager.updateCoverage(600);
                }
            }

            if(!low && !medium && !high)
            {
                low = true;
            }

        }
        else
        {
            clicked = false;

            if(playMusic)
            {
                M.sprite = btnOn;
            }
            else
            {
                M.sprite = btnOff;
            }
            
            if(playSound)
            {
                Snd.sprite = btnOn;
            }
            else
            {
                Snd.sprite = btnOff;
            }

            if(low)
            {
                lC.sprite = btnOn;
            }
            else
            {
                lC.sprite = btnOff;
            }

            if (medium)
            {
                mC.sprite = btnOn;
            }
            else
            {
                mC.sprite = btnOff;
            }

            if (high)
            {
                hC.sprite = btnOn;
            }
            else
            {
                hC.sprite = btnOff;
            }

            E.sprite = normal;
            B.sprite = normal;
        }
    }

    void clickPlayer()
    {
        if (playSound && !clicked)
        {
            audioSrc.Play();
            clicked = true;
        }
    }

    IEnumerator LateCall(GameObject o)
    {
        yield return new WaitForSeconds(0.5f);
        o.GetComponent<BoxCollider>().isTrigger = true;
    }
}
