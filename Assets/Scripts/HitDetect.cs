using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HitDetect : MonoBehaviour
{
    private bool fishHit = false;

    private GameObject localFish;
    private float score = 0.0f;
    private float tScore = 0.0f;
    public TMP_Text scoreText;

    public 


    // Start is called before the first frame update
    void Start()
    {
        localFish = GetComponent<GameObject>();
    }

    // Update is called once per frame

    private int timer = 0;
    void Update()
    {
        if (fishHit)
        {
            timer++;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                fishHit = false;
                Destroy(localFish);
                score = (400f - (0.1f * Mathf.Pow(((float)timer - 60f), 2f)));
                tScore += score;
                Debug.Log("Points Earned: " + score);
                Debug.Log("Total score is " + tScore);
                timer = 0;
            }
            if (timer > 120)
            {
                fishHit = false;
                timer = 0;
            }
        }

        scoreText.text =  tScore.ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {
            fishHit = true;
            localFish = collision.gameObject;
        }
    }
}