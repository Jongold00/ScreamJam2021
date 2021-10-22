using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSpawner : MonoBehaviour
{

    public float SpawnInterval;
    public float spawnRadius;
    public GameObject wolfPrefab;

    float spawnTimer;


    public void Update()
    {
        if (spawnTimer <= 0)
        {
            spawnTimer = SpawnInterval;
            SpawnWolf(ChooseSpawnLocation());
        }
        spawnTimer -= Time.deltaTime;
    }

    Vector3 ChooseSpawnLocation()
    {
        Vector3 ret = Random.onUnitSphere * spawnRadius;
        ret.y = 0;
        return ret;
    }

    void SpawnWolf(Vector3 location)
    {
        Instantiate(wolfPrefab, location, new Quaternion(0,0,0,0));
    }
}
