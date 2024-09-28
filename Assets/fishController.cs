using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class fishController : MonoBehaviour
{
    [SerializeField]
    float fishSpeed;

    [SerializeField]
    Vector2 fishSpawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = fishSpawnLocation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * -fishSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
