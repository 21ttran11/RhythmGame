using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pelican : MonoBehaviour
{
    [SerializeField]
    public Animator animator;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayAnimation();
        }
    }

    private void PlayAnimation()
    {
        animator.Play("Catch");
    }
}


