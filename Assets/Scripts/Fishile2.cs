using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishile2 : MonoBehaviour
{
    private float speed;
    private Transform hitbox;
    private Transform body;
    private Rigidbody2D fish;
    private Collider2D fishCollider;

    private bool hit = false;

    [SerializeField]
    private float beatsPerMinute;

    private float beatsPerSecond;

    private Vector2 startPosition;
    private Vector2 targetPosition;
    private float totalTravelDistance;
    private float elapsedTime = 0f;

    [SerializeField]
    private float arcHeight = 2f;

    void Start()
    {
        fish = GetComponent<Rigidbody2D>();
        fishCollider = GetComponent<Collider2D>();

        hitbox = GameObject.FindWithTag("Hitbox").transform;
        body = GameObject.FindWithTag("Body").transform;

        startPosition = transform.position;
        targetPosition = body.position;

        beatsPerSecond = beatsPerMinute / 60f;

        float horizontalDistance = Mathf.Abs(transform.position.x - hitbox.position.x);
        speed = horizontalDistance / beatsPerSecond;

        totalTravelDistance = Vector2.Distance(startPosition, targetPosition);
    }

    void Update()
    {
        if (!hit)
        {
            MoveInArc();
        }
    }

    void MoveInArc()
    {
        elapsedTime += Time.deltaTime;

        float traveledDistance = speed * elapsedTime;
        float t = Mathf.Clamp01(traveledDistance / totalTravelDistance);

        Vector2 flatPos = Vector2.Lerp(startPosition, targetPosition, t);
        float heightOffset = arcHeight * Mathf.Sin(Mathf.PI * t);

        transform.position = new Vector2(flatPos.x, flatPos.y + heightOffset);

        if (t >= 1f)
        {
            hit = true;
        }
    }

    public void FishHit()
    {
        hit = true;
        fishCollider.enabled = false;
        Destroy(gameObject);
    }
}
