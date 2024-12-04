using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class HitDetect : MonoBehaviour
{
    private bool fishHit = false;

    private GameObject localFish;
    private float score = 0.0f;
    private float tScore = 0.0f;
    public TMP_Text scoreText;

    [SerializeField]
    private GameObject perfect;

    [SerializeField]
    private GameObject okay;

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

                if (score >= 300f && score <= 400f)
                {
                    Instantiate(perfect);
                }
                else
                {
                    Instantiate(okay);
                }
                tScore += score;
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