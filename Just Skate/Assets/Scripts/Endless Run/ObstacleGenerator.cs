using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    int i = 0;
    public GameObject gydrant;
    public GameObject typ;
    public float timeBetweenSpawn;
    private float spawnTime;

    void Start()
    {
        
    }

    void Update()
    {
        if(Time.time > spawnTime)
        {
            if (i % 2 == 0)
            {
                Spawn(gydrant);
            }
            else
            {
                Spawn(typ);
            }
            spawnTime = Time.time + timeBetweenSpawn;
            i++;
        }
    }

    public void Spawn(GameObject obstacle)
    {
        float randomX = 10;
        float randomY = -3;
        Instantiate(obstacle, transform.position + new Vector3(randomX, randomY, 0), transform.rotation);
    }
}
