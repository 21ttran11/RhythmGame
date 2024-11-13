using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/* public class Health : MonoBehaviour
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
        if(i  == 3){
            alive = false;
            Debug.Log("game over");
            Time.timeScale = 0f;
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
 */