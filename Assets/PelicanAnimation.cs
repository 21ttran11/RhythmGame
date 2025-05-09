using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelicanAnimation : MonoBehaviour
{   
    public Animator animator;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.Play("Catch", -1, 0f);
        }
    }
}
