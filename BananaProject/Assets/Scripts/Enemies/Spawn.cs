using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] GameObject spawnItem;

    float spawnTimer;
    [SerializeField] float spawnTimeMax;

    float startTimer;
    [SerializeField] float initStartTime;

    private void Start()
    {
        startTimer = initStartTime;
        spawnTimer = spawnTimeMax;
    }

    // Update is called once per frame
    void Update()
    {
        if(startTimer >= 0)
        {
            startTimer -= Time.deltaTime;
        }
        else
        {
            spawnTimer -= Time.deltaTime;

            if (spawnTimer <= 0)
            {
                Instantiate(spawnItem, this.transform);
                spawnTimer = spawnTimeMax;
            }
        }

       
    }
}
