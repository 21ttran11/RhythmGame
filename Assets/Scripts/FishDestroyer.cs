using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FishDestroyer : MonoBehaviour
{
    [SerializeField]
    public Animator lobsterAnimator;
    private GameObject localFish;

    public UnityEvent attacked;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        lobsterAnimator.Play("Attacked");
        if (collision.gameObject.CompareTag("Fish"))
        {
            localFish = collision.gameObject;
            attacked.Invoke();
        }
        Destroy(localFish);
    }
}
