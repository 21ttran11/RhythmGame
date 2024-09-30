using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSpawner : MonoBehaviour
{
    public Transform position;
    public GameObject beat;
    // Update is called once per frame
    public void Spawn()
    {
        Instantiate(beat, position);
    }
}
