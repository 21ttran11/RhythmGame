using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resume : MonoBehaviour
{
    [SerializeField]
    private PauseMenu pauseMenu;

    public void ResumeButton()
    {
        pauseMenu.ResumeGame();
    }
}

