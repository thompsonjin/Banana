using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = spawnTimeMax;
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckpointManager.checkpointNum == 1)
        {
            if (waveNum > 0 && canSpawn)
            {
                Wave();
            }

            if(waveNum == 4)
            {
                Destroy(door);
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
