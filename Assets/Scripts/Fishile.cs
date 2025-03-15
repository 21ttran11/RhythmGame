using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Fishile : MonoBehaviour
{
    private float speed;
    private Transform hitbox;
    private Transform claws;
    private float distance;
    private Rigidbody2D fish;

    private Collider2D fishCollider;

    [SerializeField]
    private float beatsPerMinute;

    private float beatsPerSecond;

    private List<System.Action> spin_outs = new List<System.Action>();

    // Start is called before the first frame update
    void Start()
    {
        fish = GetComponent<Rigidbody2D>();
        fishCollider = GetComponent<Collider2D>();

        hitbox = GameObject.FindWithTag("Hitbox").transform;
        claws = GameObject.FindWithTag("Claws").transform;
        distance = transform.position.x - (hitbox.position.x);

        beatsPerSecond = beatsPerMinute / 60;

        speed = distance / beatsPerSecond;

        fish.drag = 0.006f; //drag slows down the obj -> reduce drag value for faster spin out

        //add spin out animations to list
        spin_outs.Add(() => spin_effect(300f));
        spin_outs.Add(() => spin_effect(-300f));
        spin_outs.Add(() => spin_effect(Random.Range(-500f, 500f)));

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

    public void fish_hit()
    {
        fishCollider.enabled = false;
        int i = Random.Range(0, spin_outs.Count);
        spin_outs[i].Invoke();
    }

    private void spin_effect(float torque) //torque = force to rotate/spin the fish
    {
        //increased force -> stronger upward lanuch
        //decreased the range -> none of the fish spin out too slow
        fish.AddForce(new Vector2(Random.Range(-4f, 4f), Random.Range(6f, 8f)), ForceMode2D.Impulse);
        fish.AddTorque(torque * 3f); //make the spin force stronger x3
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Hitbox") || other.CompareTag("Claws")) //the lobster's claw/hitbox
    //    {
    //        fish_hit();
    //    }
    //}

}