using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    Animator playerAnimator;

    [SerializeField]
    public int sequencePosition = 1; 

    void Start()
    {
        playerAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(sequencePosition >= 3)
        {
            sequencePosition = 1;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Animation(sequencePosition);
            sequencePosition++;
        }
    }

    public void Animation(int sequencePostion)
    {
        playerAnimator.SetInteger("Sequence", sequencePosition);
        StartCoroutine(Idle(sequencePosition));
    }

    IEnumerator Idle(int sequencePosition)
    {
        yield return new WaitForSeconds(playerAnimator.GetCurrentAnimatorStateInfo(0).length);
        playerAnimator.SetInteger("Sequence", 0);
    }

}
