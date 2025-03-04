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
    private Vector3 scaleChange;
    private Vector3 correction;
    public bool endLaser;

    [Header("Banana Platform Attributes")]
    public GameObject bananaPlatform;
    [SerializeField] int firedBananas;
    float fireInterval;
    [SerializeField]float maxInterval;

    bool fireLaser = true;
    bool fireBanana;
    bool spawned;


    // Start is called before the first frame update
    void Start()
    {
        fireInterval = maxInterval;
        scaleChange.x = scaleRate;
        correction.x = correctionRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (fireLaser)
        {
            if (!spawned)
            {
                SpawnLasers();
                spawned = true;
            }
            else
            {
                FireLasers(scaleChange, correction, laserRange);
                if (endLaser)
                {
                    DestroyLasers();
                    fireBanana = true;
                    fireLaser = false;
                }
            }
        }

        if (fireBanana)
        {
            FireBananaPlatforms();
            spawned = false;
            endLaser = false;
            fireLaser = true;
            fireBanana = false;
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
        for(int i = 0; i < firedBananas; i++)
        {
            fireInterval -= Time.deltaTime;

            if(fireInterval <= 0)
            {
                Instantiate(bananaPlatform, spawnPos[Random.Range(0, spawnPos.Length)]);
                fireInterval = maxInterval;
            }            
        }
    }
}
