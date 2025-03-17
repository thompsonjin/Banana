using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

public class MechaHarambe : MonoBehaviour
{
    [Header("References")]
    GameObject player;
    [SerializeField] GameObject boss;
    
    [Header("Movement")]
    [SerializeField] Transform[] movePos;
    [SerializeField] float speed;
    [SerializeField] float distance;
    public int posNum;
    public bool start;

    [Header("Attack")]
    [SerializeField] Transform projectilePoint;
    [SerializeField] GameObject projectile;
    [SerializeField] float reloadTime;
    float reload;
    [SerializeField] float fireTime;
    float fire;
    [SerializeField] float waitTime;
    float wait;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        reload = reloadTime;
        wait = waitTime;
        fire = fireTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            distance = Vector3.Distance(player.transform.position, boss.transform.position);

            if (distance > 40)
            {
                boss.transform.Translate(new Vector2(-1, 0) * (Time.deltaTime * speed));              
            }
            else if(distance < 23)
            {
                boss.transform.Translate(new Vector2(1, 0) * (Time.deltaTime * speed));
            }

            

            
            if(fire >= 0)
            {
                wait = waitTime;
                reload -= Time.deltaTime;
                if (reload <= 0)
                {
                    Instantiate(projectile, projectilePoint.position, Quaternion.identity);
                    reload = reloadTime;
                }

                fire -= Time.deltaTime;
            }
            else if(fire < 0)
            {
                wait -= Time.deltaTime;
                if(wait <= 0)
                {
                    fire = fireTime;
                }
            }
        }
    }
}
