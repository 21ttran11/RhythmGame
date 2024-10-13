using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Fishile : MonoBehaviour
{
    private float speed;
    private Transform lobster;
    private float distance;

    [SerializeField]
    private float beatsPerMinute;

    private float beatsPerSecond;

    private float secondsBetweenBeats;

    // Start is called before the first frame update
    void Start()
    {
        lobster = GameObject.FindWithTag("Lobster").transform;
        distance = transform.position.x - lobster.position.x;

        beatsPerSecond = beatsPerMinute / 60;
        secondsBetweenBeats = 1 / beatsPerSecond;

        speed = distance / (4 * secondsBetweenBeats);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        // Move towards the claws with speed calculated earlier
        Vector2 direction = (lobster.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, lobster.position, speed * Time.deltaTime);
    }
}