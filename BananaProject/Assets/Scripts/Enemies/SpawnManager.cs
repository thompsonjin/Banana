using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] spawnPoints;
    private Spawn spawn;

    [SerializeField] int waveNum;
    public bool canSpawn;

    private float spawnTimer;
    public float spawnTimeMax;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = spawnTimeMax;
    }

    // Update is called once per frame
    void Update()
    {
        if(waveNum > 0 && canSpawn)
        {
            Wave();                 
        }

        if (GameObject.FindWithTag("Enemy") == null)
        {
            waveNum++;
            canSpawn = true;
        }
        else
        {
            canSpawn = false;
        }



    }

    void Wave()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            for(int i = 0; i <= 4; i++)
            {
                for (int v = 0; v <= spawnPoints.Length - 1; v++)
                {
                    spawn = spawnPoints[v].GetComponent<Spawn>();
                    spawn.SpawnImmediate(waveNum, Random.Range(0, 3));
                }
            }
           
            spawnTimer = spawnTimeMax;
        }
       
    }
}
