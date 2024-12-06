using UnityEngine;
using UnityEngine.Playables;

public class TimelineControl : MonoBehaviour
{
    public PlayableDirector playableDirector;

    public bool isPlayingBackward = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) // Trigger backward play with 'B'
        {
            isPlayingBackward = !isPlayingBackward; // Toggle direction
        }

        if (isPlayingBackward)
        {
            // Play backward by reducing the time
            playableDirector.time -= Time.deltaTime;
            if (playableDirector.time <= 0)
            {
                playableDirector.time = 0; // Prevent going before the start
                isPlayingBackward = false; // Stop if at the beginning
            }

            playableDirector.Evaluate(); // Update the Timeline manually
        }
    }
}
