using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    public GameEvent deathEvent;

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
        if (Input.GetKeyDown(KeyCode.P)) {
            deathEvent.Raise();
        }
    }

    public void PlayHumanTheme() {
        songs[0].mute = false;
        songs[1].mute = true;
    }
    public void PlayGhostTheme() {
        songs[0].mute = true;
        songs[1].mute = false;
    }
}
