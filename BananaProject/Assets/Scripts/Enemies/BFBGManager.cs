using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UIElements;

public class BFBGManager : MonoBehaviour
{
    public Transform[] spawnPos;

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

    [Header("Banana Platform Attributes")]
    public GameObject bananaPlatform;
    int firedBananas;
    [SerializeField] int maxBananas;
    float fireInterval;
    [SerializeField]float maxInterval;
    public float speed;

    [Header("Phase Management")]
    private int phase;
    public Transform[] cannonLocations;
    public GameObject[] Generators;
    public bool bananaDir;

    bool fireLaser = true;
    bool fireBanana;
    bool spawned;
    public bool flipped;

    // Start is called before the first frame update
    void Start()
    {
        phase = 1;
        fireInterval = maxInterval;
        scaleChange.x = scaleRate;
        correction.x = correctionRate;
    }

    // Update is called once per frame
    void Update()
    {
        //Location change
        switch (phase)
        {
            case 1:
                this.transform.position = cannonLocations[0].position;
                break;
            case 2:
                this.transform.position = cannonLocations[1].position;
                if (!flipped)
                {
                    DestroyLasers();
                    transform.localScale = -transform.localScale;
                    flipped = true;
                }
                break;
            case 3:
                this.transform.position = cannonLocations[2].position;
                if (flipped)
                {
                    transform.localScale = -transform.localScale;
                    flipped = false;
                }
                break;
        }
      

        //Banana Cycle

        fireInterval -= Time.deltaTime;

        if (fireInterval <= 0)
        {
            FireBananaPlatforms();
        }

        //Laser Cycle
        if(phase == 1)
        {
            if (!spawned)
            {
                laserInterval -= Time.deltaTime;
                if (laserInterval <= 0)
                {
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
                    DestroyLasers();
                    laserInterval = maxLaserInterval;
                    spawned = false;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            phase++;
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

    void FireBananaPlatforms()
    {
        Instantiate(bananaPlatform, spawnPos[Random.Range(0, spawnPos.Length)].position, Quaternion.identity);
        fireInterval = maxInterval;
    }
}
