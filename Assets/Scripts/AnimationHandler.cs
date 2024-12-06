using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField]
    public Animator lobsterAnimator;

    private bool hitDown = true;

    private GameObject scene1;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayAnimation();
        }
    }

    public void PlayAnimation()
    {
        if (CompareTag("Lobster2"))
        {
            lobsterAnimator.Play("Attack");
        }
        else
        {
            if (hitDown)
            {
                lobsterAnimator.Play("HitUp");
                hitDown = false;
            }
            else if (!hitDown){
                lobsterAnimator.Play("HitDown");
                hitDown = true;
            }
        }
    }

    public void IdleDown()
    {
        lobsterAnimator.Play("IdleDown");
    }

    public void IdleUp()
    {
        lobsterAnimator.Play("IdleUp");
    }

    public void Idle()
    {
        lobsterAnimator.Play("Idle");
    }

}
