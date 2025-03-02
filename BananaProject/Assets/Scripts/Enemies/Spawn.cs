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

    public bool enemySpawn;
    public GameObject[] enemies = new GameObject[2];
    public int enemiesLeft;

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
                if (enemySpawn)
                {
                    if(enemiesLeft > 0)
                    {
                        Instantiate(enemies[enemiesLeft], this.transform);
                        enemiesLeft--;
                    }
                    else
                    {
                      
                    }
                }
                else
                {
                    Instantiate(spawnItem, this.transform);       
                }

                spawnTimer = spawnTimeMax;
            }
        }

       
    }
}
