using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

public class MechaHarambe : MonoBehaviour
{
    [Header("References")]
    GameObject player;
    [SerializeField] GameObject boss;
    [SerializeField] BFBGManager b_Man;
    
    [Header("Movement")]
    [SerializeField] Transform[] phasePos;
    [SerializeField] float speed;
    [SerializeField] float distance;
    public int posNum;
    public bool start;

    [Header("Attack")]
    [SerializeField] Transform projectilePoint;
    [SerializeField] GameObject trackingLaser;
    [SerializeField] GameObject randomLaser;
    [SerializeField] float reloadTime;
    float reload;
    [SerializeField] float fireTime;
    float fire;
    [SerializeField] float waitTime;
    float wait;
    [SerializeField] GameObject risingLava;
    bool attackType;



    // Start is called before the first frame update
    void Start()
    {
        b_Man = GameObject.FindWithTag("BFBG").GetComponent<BFBGManager>();
        player = GameObject.FindWithTag("Player");
        reload = reloadTime;
        wait = waitTime;
        fire = fireTime;
        attackType = true;

        NextPhase(b_Man.phase);
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            distance = Vector3.Distance(player.transform.position, boss.transform.position);

            if (distance > 45)
            {
                if(b_Man.phase == 0 || b_Man.phase == 2)
                {
                    boss.transform.Translate(new Vector2(-1, 0) * (Time.deltaTime * speed));
                }
                else
                {
                    boss.transform.Translate(new Vector2(1, 0) * (Time.deltaTime * speed));
                }                      
            }
            else if(distance < 25)
            {
                if (b_Man.phase == 0 || b_Man.phase == 2)
                {
                    boss.transform.Translate(new Vector2(1, 0) * (Time.deltaTime * speed));
                }
                else
                {
                    boss.transform.Translate(new Vector2(-1, 0) * (Time.deltaTime * speed));
                }         
            }

                      
            if(fire >= 0)
            {
                wait = waitTime;
                reload -= Time.deltaTime;
                if (reload <= 0)
                {
                    if (attackType)
                    {
                        Instantiate(trackingLaser, projectilePoint.position, Quaternion.identity);
                        reload = reloadTime;
                    }
                    else
                    {
                        Instantiate(randomLaser, projectilePoint.position, Quaternion.identity);
                        reload = reloadTime / 2;
                    }

                    
                }

                fire -= Time.deltaTime;
            }
            else if(fire < 0)
            {
                wait -= Time.deltaTime;
                if(wait <= 0)
                {
                    attackType = !attackType;
                    fire = fireTime;
                }
            }
        }
    }

    public void NextPhase(int p)
    {
        boss.transform.position = phasePos[p].position;

        if (p == 0 || p == 2)
        {
            boss.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            boss.transform.localScale = new Vector3(1, 1, 1);
        }

        if(p == 2)
        {
            risingLava.SetActive(true);
            risingLava.GetComponent<HarmingLiquid>().Restart();
        }
            
    }
}
