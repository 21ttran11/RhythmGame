using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public bool isPaused;
    public UnityEvent paused;

    // Start is called before the first frame update
    void Start()
    {
        paused = new UnityEvent();
        paused.Invoke();
        pauseMenu.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        
    }
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;  //pause physics updates
        AudioListener.pause = true;

        //pause all animations
        Animator[] animators = FindObjectsOfType<Animator>();
        foreach (Animator animator in animators)
        {
            animator.speed = 0f;
        }

        StopAllCoroutines();
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f; //resume game physics

        AudioListener.pause = false;

        //sesume all animations
        Animator[] animators = FindObjectsOfType<Animator>();
        foreach (Animator animator in animators)
        {
            animator.speed = 1f;
        }
        
        isPaused = false;
    }
}
