using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Scripting.APIUpdating;

public class Fishile : MonoBehaviour
{
    private float speed;
    private Transform hitbox;
    private Transform claws;
    private float distance;
    private Rigidbody2D fish;
    private Light2D pulsarLight;
    private bool hit = false;

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
        if (gameObject.name == "FishilePerp(Clone)")
        {
            pulsarLight = GetComponent<Light2D>();
        }

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
        if (!hit)
        {
        Move();
        }
    }

    public void Move()
    {
        // Move towards the claws with speed calculated earlier
        Vector2 direction = (claws.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, claws.position, speed * Time.deltaTime);
    }

    public void fish_hit()
    {
        hit = true;
        fishCollider.enabled = false;
        int i = Random.Range(0, spin_outs.Count);
        spin_outs[i].Invoke();
        StartCoroutine(DestroyFish());
    }

    private void spin_effect(float torque) //torque = force to rotate/spin the fish
    {
        //increased force -> stronger upward lanuch
        //decreased the range -> none of the fish spin out too slow
        fish.AddForce(new Vector2(Random.Range(-4f, 4f), Random.Range(6f, 8f)), ForceMode2D.Impulse);
        fish.AddTorque(torque * 3f); //make the spin force stronger x3
    }
    private IEnumerator DestroyFish()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
    public IEnumerator PulseLight()
    {
        bool progress = false;
        while (true)
        {
            if (!progress)
            {
                pulsarLight.intensity += .2f;
                if (pulsarLight.intensity >= 1f)
                {
                    pulsarLight.intensity = 1f;
                    progress = true;
                }
            } else
            {
                pulsarLight.intensity -= .05f;
                if (pulsarLight.intensity <= 0f)
                {
                    pulsarLight.intensity = 0f;
                    break;
                }
            }

            yield return new WaitForSeconds(.01f);
        }
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Hitbox") || other.CompareTag("Claws")) //the lobster's claw/hitbox
    //    {
    //        fish_hit();
    //    }
    //}

}