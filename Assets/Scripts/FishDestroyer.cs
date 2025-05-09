using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FishDestroyer : MonoBehaviour
{
    [SerializeField]
    public Animator lobsterAnimator;
    private GameObject localFish;

    [SerializeField]
    private GameObject miss;

    public UnityEvent attacked;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(lobsterAnimator != null)
        {
            lobsterAnimator.Play("Attacked");
        }

        Instantiate(miss);
        if (collision.gameObject.CompareTag("Fish"))
        {
            localFish = collision.gameObject;
            attacked.Invoke();
        }
        Destroy(localFish);
    }
}
