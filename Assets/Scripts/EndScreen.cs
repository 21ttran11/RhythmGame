using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [SerializeField] 
    private GameObject winScreen;

    [SerializeField] 
    private GameObject loseScreen;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        if (StaticData.won)
        {
            loseScreen.SetActive(false);
            scoreText.text += StaticData.score;
        } else
        {
            winScreen.SetActive(false);
        }

    }
    public void Restart()
    {
        SceneManager.LoadScene("DEMO");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

}
