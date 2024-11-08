using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField]
    public Animator lobsterAnimator;

    private bool hitDown = false;

    // Start is called before the first frame update
    void Start()
    {
        lobsterAnimator = gameObject.GetComponent<Animator>();
    }

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
        if (hitDown == false)
        {
            lobsterAnimator.Play("HitDown");
            hitDown = true;
        }
        else if (hitDown){
            lobsterAnimator.Play("HitUp");
            hitDown = false;
        }
    }

    public void ReturnIdle()
    {
        lobsterAnimator.Play("Idle");
    }
}
