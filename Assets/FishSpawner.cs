using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject fishPrefab;

    [SerializeField]
    //spawns a new fish every 3 seconds
    private float fishInterval = 3f;

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
            //creates the new fish at the same point
            GameObject newFish = Instantiate(fish, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}
