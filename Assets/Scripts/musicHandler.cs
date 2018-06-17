using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicHandler : MonoBehaviour {

    public AudioSource audioSource;
    static public musicHandler music_handler;

    void Awake()
    {
        if(!music_handler)
        {
            audioSource = GetComponent<AudioSource>();
            music_handler = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void ToggleMusic()
    {
        music_handler.audioSource.mute = !music_handler.audioSource.mute;
        OptionsMenu.playMusic = !OptionsMenu.playMusic;
        Debug.Log(OptionsMenu.playMusic);
    }
}
