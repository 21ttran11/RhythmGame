using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject fishPrefab;

    [SerializeField]
    //spawns a new fish every 3.5 seconds
    private float fishInterval = 3.5f;

    void Start()
    {
        //makes the spawning of the fish endless
        StartCoroutine(spawnFish(fishInterval, fishPrefab));
    }

    private IEnumerator spawnFish(float interval, GameObject fish)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            //creates the new fish at random values/positions using the vector
            GameObject newFish = Instantiate(fish, new Vector3(Random.Range(-5f, 5), Random.Range(-6f, 6f), 0), Quaternion.identity);
        }
    }
}
