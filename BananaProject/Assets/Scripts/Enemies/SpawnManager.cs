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

    float waveWaitTime;
    float maxWaitTime = 3;

    public GameObject door;

    public Text count;
    public Text waveTitle;
    private int enemiesLeft;
    public LayerMask enemyLayer;
    float textVis;
    float textTime;
    float maxTextTime = 5;

    public AudioSource first;
    public AudioSource second;
    public AudioSource third;


    // Start is called before the first frame update
    void Start()
    {
        first.clip.LoadAudioData();
        second.clip.LoadAudioData();
        third.clip.LoadAudioData();
        spawnTimer = spawnTimeMax;
        waveWaitTime = maxWaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(waveNum);
        if(CheckpointManager.checkpointNum == 1)
        {
            if (!first.isPlaying && waveNum == 1)
            {
                textTime = maxTextTime;
                textVis = 1;
                waveTitle.color = new Color(1,1,1,textVis);
                waveTitle.text = "";
                waveTitle.text = waveTitle.text + "Wave One";
                first.Play();
            }

            if (waveNum > 0 && canSpawn)
            {
                Wave();
            }

            if (GameObject.FindWithTag("Enemy") == null && waveNum > 0)
            {               
                waveWaitTime -= Time.deltaTime;
                if(waveWaitTime <= 0)
                {
                    waveNum++;
                    canSpawn = true;
                }
            }
            else
            {
                waveWaitTime = maxWaitTime;
                canSpawn = false;         
            }
        }

        if(textVis > 0)
        {
            textTime -= Time.deltaTime;

            if(textTime <= 0)
            {
                textVis -= .01f;
                waveTitle.color = new Color(1, 1, 1, textVis);
            }      
        }

        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(transform.position, 100, enemyLayer);
        enemiesLeft = enemiesHit.Length;

        count.text = enemiesLeft.ToString();

        switch (waveNum)
        {
            case 2:

                if(waveWaitTime <= 0)
                {
                    if (!second.isPlaying)
                    {
                        textTime = maxTextTime;
                        textVis = 1;
                        waveTitle.color = new Color(1, 1, 1, textVis);
                        waveTitle.text = "";
                        waveTitle.text = "Wave Two";
                        first.Stop();
                        second.Play();
                    }
                }                   
                break;
            case 3:

                if (waveWaitTime <= 0)
                {
                    if (!third.isPlaying)
                    {
                        textTime = maxTextTime;
                        textVis = 1;
                        waveTitle.color = new Color(1, 1, 1, textVis);
                        waveTitle.text = "";
                        waveTitle.text = "Wave Three";
                        second.Stop();
                        third.Play();
                    }
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
            for(int i = 0; i <= 1; i++)
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 100);
    }
}
