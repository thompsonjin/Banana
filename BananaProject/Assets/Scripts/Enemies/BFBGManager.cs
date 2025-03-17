using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class BFBGManager : MonoBehaviour
{
    public Transform[] spawnPos;
    [SerializeField] MechaHarambe m_Ham;

    [Header("Laser Attributes")]
    public GameObject laser;
    [SerializeField] float scaleRate;
    [SerializeField] float correctionRate;
    [SerializeField] float laserRange;
    float laserInterval;
    [SerializeField] float maxLaserInterval;
    private Vector3 scaleChange;
    private Vector3 correction;
    public bool endLaser;
    public GameObject warning;

    [Header("Banana Platform Attributes")]
    public GameObject bananaPlatform;
    int firedBananas;
    [SerializeField] int maxBananas;
    float fireInterval;
    [SerializeField]float maxInterval;
    public float speed;

    [Header("Phase Management")]
    public int phase;
    public Transform[] cannonLocations;
    public GameObject[] Generators;
    public bool bananaDir;

    bool spawned;
    public bool flipped;


    // Start is called before the first frame update
    void Start()
    {
        fireInterval = maxInterval;
        scaleChange.x = scaleRate;
        correction.x = correctionRate;

        if(CheckpointManager.checkpointNum != 0)
        {
            phase = CheckpointManager.checkpointNum - 1;

            for(int i = 0; i == phase - 1; i++)
            {
                Destroy(Generators[i]);
            }
        }       
    }

    // Update is called once per frame
    void Update()
    {
        //Location change
        switch (phase)
        {
            case 0:
                this.transform.position = cannonLocations[0].position;
                break;
            case 1:
                this.transform.position = cannonLocations[1].position;
                warning.SetActive(false);
                if (!flipped)
                {
                    if (!CheckLasers())
                    {
                        DestroyLasers();
                    }     
                    transform.localScale = -transform.localScale;
                    flipped = true;
                }
                break;
            case 2:
                this.transform.position = cannonLocations[2].position;
                if (flipped)
                {
                    transform.localScale = -transform.localScale;
                    flipped = false;
                }
                break;
            case 3:
                GameObject.Find("Game Manager").GetComponent<GameManager>().NextScene();
                break;
             
        }

            //Banana Cycle

            fireInterval -= Time.deltaTime;

        if (fireInterval <= 0)
        {
            FireBananaPlatforms();
        }

        //Laser Cycle
        if(phase == 0)
        {
            if (!spawned)
            {
                laserInterval -= Time.deltaTime;
                if (laserInterval <= 0)
                {
                    warning.SetActive(true);
                    Debug.Log("Spawn Laser");
                    SpawnLasers();
                    endLaser = false;
                    spawned = true;
                }

            }
            else
            {
                FireLasers(scaleChange, correction, laserRange);
                if (endLaser)
                {
                    warning.SetActive(false);
                    DestroyLasers();
                    laserInterval = maxLaserInterval;
                    spawned = false;
                }
            }
        }
    }

    void SpawnLasers()
    {
        foreach(Transform t in spawnPos)
        {
            Instantiate(laser, t);
        }
    }

    void FireLasers(Vector3 scale, Vector3 cor, float range)
    {
        foreach(Transform t in spawnPos)
        {
            t.GetChild(0).gameObject.GetComponent<BFBGLaserBehaviour>().Fire(scale, cor, range);
        }
    }

    void DestroyLasers()
    {
        foreach (Transform t in spawnPos)
        {
            Destroy(t.GetChild(0).gameObject);
        }
    }

    bool CheckLasers()
    {
        if(GameObject.FindWithTag("Laser") != null)
        {
            return false;
        }
        else
        {
            return true;
        }
        
    }

    void FireBananaPlatforms()
    {
        Instantiate(bananaPlatform, spawnPos[Random.Range(0, spawnPos.Length)].position, Quaternion.identity);
        fireInterval = maxInterval;
    }

    public void NextPhase()
    {
        phase++;
        m_Ham.NextPhase(phase);
    }
}
