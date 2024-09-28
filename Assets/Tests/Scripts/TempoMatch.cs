using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TempoMatch : MonoBehaviour
{
    [SerializeField]
    private float bpm;
    // Enter bpm of song from inspector


    public AudioSource music;
    public AudioSource click;
    // reference to song & metronome 

    [SerializeField] private float secBetwBeat;
    [SerializeField] private float timePos;
    [SerializeField] private float beatPos;
    // serialized for bug testing

    public Note[] notes;

    /* creates an array (i keep calling them tables)
     * 
     * array contains the beat which an event occurs
     * also contains a bool to make the note a normal one or a longer one
     * 
     * Might be a bit tedious to work with, but it works
    */

    void Start()
    {
        music.Play();
        secBetwBeat = GetSecondsBetweenBeats(bpm);
        // calculates the amount of seconds between each beat
        // later used for getting the position of the song relative to the bpm
    }

    private float GetSecondsBetweenBeats(float bpm)
    {
        return 1 / (bpm / 60f);
    }

    private int i;
    void Update()
    {
        UpdateTimeData();
        // Gets the time postion of the song, then the position relative to the bpm

        if (beatPos >= (float)notes[i].beat - .05) 
        {
            click.Play();
            i++;
        }
        // plays audio when new beat occurs
        // >= and not == because the beat pos can skip over the timed marker
        // subtracted by .05 to account for the the delay, which hopefully makes it more accurate

    }

    private void UpdateTimeData()
    {
        timePos = (float)music.timeSamples / (float)music.clip.frequency;
        beatPos = timePos / secBetwBeat;
    }
}

[System.Serializable]
public class Note
{
    public float beat;
    public bool hold;
}
// definition for the table