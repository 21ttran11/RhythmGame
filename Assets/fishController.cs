using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEditor.Tilemaps;
using UnityEngine;

public class fishController : MonoBehaviour
{
    [SerializeField]
    float fishSpeed;

    [SerializeField]
    Vector2 fishSpawnLocation;

    //list for the fish animations which we'll access thru the trigger names (left, right, up, down)
    [SerializeField]
    List<string> fishAnimations;

    //added reference to animator
    Animator animate;

    // Start is called before the first frame update
    void Start()
    {
        //transform.position = fishSpawnLocation;
        
        //add animator component to the fish
        animate = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //destroys the fish if it goes out of bounds
        if (transform.position.x < -10 || transform.position.x > 10 || transform.position.y < -4 || transform.position.y > 4)
        {
            //plays the spinning out animation
            Destroy(gameObject);
        }

       transform.Translate(Vector2.left * fishSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayRandomAnimation();
            //Destroy(gameObject);
        }
    }

    private void PlayRandomAnimation()
    {
        if (fishAnimations.Count > 0)
        {
            //choose a random spin out animation from the list based on the trigger names
            int randomAnimation = Random.Range(0, fishAnimations.Count);
            string animation = fishAnimations[randomAnimation];

            //play the animation based on the random trigger name

            animate.Play(animation);
        }
    }
}