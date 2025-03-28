using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] spawnPoints;
    private Spawn spawn;

    [SerializeField] int waveNum;
    public bool canSpawn;

    private float spawnTimer;
    public float spawnTimeMax;

    public GameObject door;

    public Text count;
    private int enemiesLeft;
    public LayerMask enemyLayer;

    public AudioSource first;
    public AudioSource second;
    public AudioSource third;


    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = spawnTimeMax;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(waveNum);
        if(CheckpointManager.checkpointNum == 1)
        {
            if (!first.isPlaying && waveNum == 1)
            {
                first.Play();
            }

            if (waveNum > 0 && canSpawn)
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

        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(transform.position, 100, enemyLayer);
        enemiesLeft = enemiesHit.Length;

        count.text = enemiesLeft.ToString();

        switch (waveNum)
        {
            case 2:
                if (!second.isPlaying)
                {
                    first.Stop();
                    second.Play();
                }         
                break;
            case 3:
                if (!third.isPlaying)
                {
                    second.Stop();
                    third.Play();
                }                    
                break;
            case 4:
                if (!first.isPlaying)
                {
                    third.Stop();
                    first.Play();
                    Destroy(door);
                }               
                break;
        }
    }

    void Wave()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            for (int v = 0; v <= spawnPoints.Length - 1; v++)
            {
                spawn = spawnPoints[v].GetComponent<Spawn>();
                spawn.SpawnImmediate(waveNum, Random.Range(0, 3));
            }

            spawnTimer = spawnTimeMax;
        }
       
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 100);
    }
}
