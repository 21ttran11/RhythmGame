using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Fishile : MonoBehaviour
{
    private float speed;
    private Transform claws;
    private float distance;

    [SerializeField]
    private float beatsPerMinute;

    private float beatsPerSecond;

    private float secondsBetweenBeats;

    // Start is called before the first frame update
    void Start()
    {
        claws = GameObject.FindWithTag("Claws").transform;
        distance = transform.position.x - claws.position.x;

        beatsPerSecond = beatsPerMinute / 60;
        secondsBetweenBeats = 1 / (4 * beatsPerSecond);

        speed = distance / secondsBetweenBeats;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        // Move towards the claws with speed calculated earlier
        Vector2 direction = (claws.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, claws.position, speed * Time.deltaTime);
    }
}