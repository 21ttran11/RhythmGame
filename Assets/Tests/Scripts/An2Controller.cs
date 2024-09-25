using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class An2Controller : MonoBehaviour
{
    [SerializeField]
    public Animator lobsterAnimator;

    [SerializeField]
    public TextMeshProUGUI timeText;

    [SerializeField]
    float time = 0f;

    [SerializeField]
    int seconds = 0;

    // Start is called before the first frame update
    void Start()
    {
        lobsterAnimator = gameObject.GetComponent<Animator>();
        timeText.text = time.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayAnimation();
        }

        timeText.text = seconds.ToString();
    }

    private void FixedUpdate()
    {
        time += Time.fixedDeltaTime;

        if (time >= 1f)
        {
            seconds++;
            time -= 1f;
        }

        if(time >= 4f)
        {
            time = 0f;
            seconds = 0;
        }
    }

    public void PlayAnimation()
    {
        if(seconds%2 == 0)
        {
            lobsterAnimator.Play("Attack");
        }
        else
        {
            lobsterAnimator.Play("Attacked");
        }
    }

    public void ReturnIdle()
    {
        lobsterAnimator.Play("Idle");
    }
}
