using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDestroyer : MonoBehaviour
{
    [SerializeField]
    public Animator lobsterAnimator;
    private GameObject localFish;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        lobsterAnimator.Play("Attacked");
        if (collision.gameObject.CompareTag("Fish"))
        {
            localFish = collision.gameObject;
        }
        Destroy(localFish);
    }
}
