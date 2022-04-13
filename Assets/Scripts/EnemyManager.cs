using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject carPrefab;
    // spawn options
    private bool isStopped = false;
    private readonly float[] zSpawns = {
        -8, -6, -4, -2, 0, 2, 4, 6, 8
    };
    private readonly float maxX = 22f;
    public float initialSpawnDelay = 0f;
    public float spawnRepeatRateLower = 1f;
    public float spawnRepeatRateHigher = 4f;

    // Start is called before the first frame update
    void Start()
    {
        setupCarSpawning();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
     * Setup first car instantiation.
     */
    private void setupCarSpawning()
    {
        Invoke("SpawnCar", initialSpawnDelay);
    }

    /**
     * Spawn a car in a random location, and then set a timer to spawn another one.
     */
    private void SpawnCar()
    {
        if (isStopped) return;
        int zIx = Random.Range(0, zSpawns.Length);
        // get car attributes
        Vector3 carPos;
        CarInfo.Direction direction;
        if (zIx % 2 == 0)
        {
            carPos = new Vector3(maxX, carPrefab.transform.position.y, zSpawns[zIx]);
            direction = CarInfo.Direction.LEFT;
        }
        else
        {
            carPos = new Vector3(-maxX, carPrefab.transform.position.y, zSpawns[zIx]);
            direction = CarInfo.Direction.RIGHT;
        }
        // instantiate car
        GameObject car = Instantiate(carPrefab, carPos, carPrefab.transform.rotation, gameObject.transform);
        car.GetComponent<Car>().direction = direction;
        // recurse
        float repeatRate = Random.Range(spawnRepeatRateLower, spawnRepeatRateHigher);
        Invoke("SpawnCar", repeatRate);
    }

    private void Stop()
    {
        isStopped = true;
    }
}
