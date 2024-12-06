using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> hearts;

    private SpriteRenderer heartSprite;
    // Start is called before the first frame update

    int i = 0;

    private bool alive = true;

    [SerializeField]
    private Sprite deadHeart;

    private void Update()
    {
        if(i  == hearts.Count){
            alive = false;
            Time.timeScale = 0f;
            FMOD.Studio.Bus masterBus = RuntimeManager.GetBus("bus:/");
            masterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);            
            StaticData.won = false;
            SceneManager.LoadScene("End Scene");
        }
    }
    public void updateHeart()
    {
        GameObject heart = hearts[i];
        heartSprite = heart.GetComponent<SpriteRenderer>();
        heartSprite.sprite = deadHeart;
        i++;
    }
}