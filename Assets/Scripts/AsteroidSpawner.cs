using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject AsteroidGO;

    void SpawnAsteroid()
    {
        GameObject playerShip = GameObject.Find("PlayerGO");

        if (playerShip != null)
        { 
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        


            GameObject anAsteroid = (GameObject)Instantiate(AsteroidGO);



            anAsteroid.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
            //Vector2 direction = new Vector2(Random.Range(min.x, max.x), min.y);
            Vector2 direction = playerShip.transform.position - anAsteroid.transform.position;
            anAsteroid.GetComponent<AsteroidControl>().SetDirection(direction);

            ScheduleNextAsteroidSpawn();
        }
    }

    void ScheduleNextAsteroidSpawn()
    {
        float spawnInSeconds;
        float maxSpawnRateInSeconds = 3f;

        spawnInSeconds = Random.Range(1f, maxSpawnRateInSeconds);

        Invoke("SpawnAsteroid", spawnInSeconds);
    }

    // function to start enemy spawner
    public void ScheduleAsteroidSpawner()
    {
        float maxSpawnRateInSeconds = Random.Range(1f, 3f);

        Invoke("SpawnAsteroid", maxSpawnRateInSeconds);
    }

}
