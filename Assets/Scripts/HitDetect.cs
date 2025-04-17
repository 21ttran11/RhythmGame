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
    public TMP_Text scoreText2;
    public TMP_Text scoreText3;

    [SerializeField]
    private GameObject perfect;

    [SerializeField]
    private GameObject okay;

    // Start is called before the first frame update
    void Start()
    {
        // Parse the value from scoreText if it exists
        if (float.TryParse(scoreText.text, out float initialScore))
        {
            tScore = initialScore;
        }
        else
        {
            Debug.LogWarning("Score text is not a valid number! Defaulting to 0.");
            tScore = 0.0f;
        }
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
                Fishile fish = localFish.GetComponent<Fishile>();
                fish.fish_hit();
                // Object.Destroy(fish); // destroy after calling the spin out
                Debug.Log("bye fish");

                // Destroy(localFish); //instead of destroying, call spinout function
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

        // Update both TMP_Text objects with the latest score
        scoreText.text = tScore.ToString();
        scoreText2.text = tScore.ToString();
        scoreText3.text = tScore.ToString();
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
