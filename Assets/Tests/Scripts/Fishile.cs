using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Fishile : MonoBehaviour
{
    public float speed;
    private Transform lobster;

    // Start is called before the first frame update
    void Start()
    {
        lobster = GameObject.FindWithTag("Lobster").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        Vector2 direction = (lobster.position - transform.position).normalized;
        Vector2 position = (Vector2)lobster.position - direction;
        transform.position = Vector2.MoveTowards(transform.position, position, speed * Time.deltaTime);
    }
}
