using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSpawner : MonoBehaviour
{
    public Transform position;
    public GameObject beat;

    public Animator animator; 
    public GameObject perpBeat;

    public FMODUnity.EventReference EventName;
    FMOD.Studio.EventInstance sfx;

#if UNITY_EDITOR
    void Reset()
    {
        EventName = FMODUnity.EventReference.Find("event:/SpawnSFX");
    }
#endif
    public void Spawn()
    {
        sfx = FMODUnity.RuntimeManager.CreateInstance(EventName);
        if (animator != null)
        {
            animator.Play("Throw", -1, 0f);
        }
        Instantiate(beat, position);
        sfx.start();
    }

    public void Pulse()
    {
        foreach (Transform child in transform)
        {
            Debug.Log(child.name);
            if (child.name == "FishilePerp(Clone)")
            {
                if (child.gameObject.TryGetComponent<Fishile>(out var fishileClass))
                {
                StartCoroutine(fishileClass.PulseLight());
                }
            }
        }
    }

    public void SwitchPrefab()
    {
        beat = perpBeat;
    }
}
