using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigHarambeGun : MonoBehaviour
{
    [Header("Laser Settings")]
    public GameObject[] laser;
    public Transform[] laserPos;
    [SerializeField] float laserRange;
    private Vector3 scaleChange;
    private Vector3 positionChange;

    // Start is called before the first frame update
    void Start()
    {      
        scaleChange = new Vector3(0.4f, 0, 0);
        positionChange = new Vector3(0.2f, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        FireLaser();
    }   

    void FireLaser()
    {
        foreach (GameObject g in laser)
        {
            if (g.transform.localScale.x >= -laserRange)
            {
                g.GetComponent<HarambeLaserBehaviour>().Fire(scaleChange, positionChange);
            } 
            
        }  
    }
}
