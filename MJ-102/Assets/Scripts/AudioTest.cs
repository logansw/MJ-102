using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{

    AudioSource[] songs;

    // Start is called before the first frame update
    void Start()
    {
        songs = GetComponents<AudioSource>(); // Index 0 is alive theme
                                              // Index 1 is ghost theme
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("d")) // Dead
        {
            songs[0].mute = true;
            songs[1].mute = false;
        }

        if (Input.GetKeyDown("a")) // Alive
        {
            songs[0].mute = false;
            songs[1].mute = true;
        }
    }
}
