using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] GameObject spawnItem;
    [SerializeField] GameObject[] enemyPoolOne;
    [SerializeField] GameObject[] enemyPoolTwo;
    [SerializeField] GameObject[] enemyPoolThree;

    float spawnTimer;
    [SerializeField] float spawnTimeMax;

    float startTimer;
    [SerializeField] float initStartTime;

    public bool timed;

    private void Start()
    {
        startTimer = initStartTime;
        spawnTimer = spawnTimeMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (timed)
        {
            if (startTimer >= 0)
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

    public void SpawnImmediate(int wave, int enemy)
    {
        switch (wave)
        {
            case 1:
                Instantiate(enemyPoolOne[enemy], this.transform);
                break;

            case 2:
                Instantiate(enemyPoolTwo[enemy], this.transform);
                break;

            case 3:
                Instantiate(enemyPoolThree[enemy], this.transform);
                break;
        }
    }
}
