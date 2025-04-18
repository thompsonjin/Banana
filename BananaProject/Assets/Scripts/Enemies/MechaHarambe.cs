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
    public AudioSource audio;
    
    [Header("Movement")]
    [SerializeField] Transform[] phasePos;
    [SerializeField] float speed;
    [SerializeField] float distance;
    public int posNum;
    public bool start;

    [Header("Guns")]
    [SerializeField] Transform projectilePointOne;
    [SerializeField] Transform projectilePointTwo;
    [SerializeField] GameObject gunOne;
    [SerializeField] GameObject gunTwo;
    [SerializeField] GameObject gunPivot;
    private Quaternion rotationTargetOne;
    public float gunSpeed;
    bool ready;
    [SerializeField] Animator animOne;
    [SerializeField] Animator animTwo;

    [Header("Laser")]
    [SerializeField] GameObject normalLaser;
    [SerializeField] GameObject randomLaser;
    [SerializeField] GameObject trackingLaser;
    [SerializeField] float reloadTime;
    float reload;
    [SerializeField] float fireTime;
    float fire;
    [SerializeField] float waitTime;
    float wait;
    [SerializeField] GameObject risingLava;
    int attackType;





    // Start is called before the first frame update
    void Start()
    {
        b_Man = GameObject.FindWithTag("BFBG").GetComponent<BFBGManager>();
        player = GameObject.FindWithTag("Player");
        reload = reloadTime;
        wait = waitTime;
        fire = fireTime;
        attackType = 0;
        rotationTargetOne = new Quaternion(90, 0, 0, 0);

        NextPhase(b_Man.phase);

        Debug.Log(gunPivot.transform.rotation.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            if(player != null)
            {
                distance = Vector3.Distance(player.transform.position, boss.transform.position);
            }

            if (distance > 50)
            {
                if(b_Man.phase == 0 || b_Man.phase == 1 || b_Man.phase == 2)
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
                if (b_Man.phase == 0 || b_Man.phase == 1 || b_Man.phase == 2)
                {
                    boss.transform.Translate(new Vector2(1, 0) * (Time.deltaTime * speed));
                }
                else
                {
                    boss.transform.Translate(new Vector2(-1, 0) * (Time.deltaTime * speed));
                }         
            }

            

            if (attackType == 1)
            {
                Debug.Log(gunPivot.transform.eulerAngles);
                SpinGun();
            }
            else if(attackType == 2)
            {
                
                ResetGun();
            }


            if (fire >= 0)
            {
                wait = waitTime;
                reload -= Time.deltaTime;
                if (reload <= 0)
                {
                    animOne.SetBool("Fire", true);
                    animTwo.SetBool("Fire", true);
                    if (attackType == 0)
                    {               
                        Instantiate(normalLaser, projectilePointOne.position, Quaternion.identity);
                        Instantiate(normalLaser, projectilePointTwo.position, Quaternion.identity);
                        reload = reloadTime;
                    }
                    else if (attackType == 1)
                    {
                        if(b_Man.phase == 1)
                        {
                            Instantiate(normalLaser, projectilePointOne.position, Quaternion.identity);
                            Instantiate(normalLaser, projectilePointTwo.position, Quaternion.identity);
                            reload = reloadTime;
                        }
                        else
                        {
                            Instantiate(randomLaser, projectilePointOne.position, Quaternion.identity);
                            Instantiate(randomLaser, projectilePointTwo.position, Quaternion.identity);
                            reload = reloadTime / 2.5f;
                        }
                            
                    }
                    else if (attackType == 2)
                    {
                        Instantiate(trackingLaser, projectilePointOne.position, Quaternion.identity);
                        Instantiate(trackingLaser, projectilePointTwo.position, Quaternion.identity);
                        reload = reloadTime * 3;
                    }


                }

                fire -= Time.deltaTime;
            }
            else if (fire < 0)
            {
                animOne.SetBool("Fire", false);
                animTwo.SetBool("Fire", false);
                wait -= Time.deltaTime;
                if (wait <= 0)
                {
                    attackType++;
                    if (attackType > 2)
                    {
                        attackType = 0;
                    }
                    fire = fireTime;
                }
            }
        }
    }

    public void NextPhase(int p)
    {
        boss.transform.position = phasePos[p].position;

        if (p == 0 || p == 1 || p == 4)
        {
            boss.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            boss.transform.localScale = new Vector3(1, 1, 1);
        }

        if(p == 4)
        {
            risingLava.SetActive(true);
            risingLava.GetComponent<HarmingLiquid>().Restart();
        }          
    }

    void SpinGun()
    {
        if (!ready)
        {
            if (gunOne.transform.eulerAngles.z > 270 || gunOne.transform.eulerAngles.z == 0)
            {              
                gunOne.transform.Rotate(0, 0, 200 * Time.deltaTime);
                gunTwo.transform.Rotate(0, 0, -200 * Time.deltaTime);
            }
            else
            {
                ready = true;
            }
        }
        else
        {
            if(gunPivot.transform.eulerAngles.z > 1 || gunPivot.transform.eulerAngles.z == 0)
            {
                gunPivot.transform.Rotate(0, 0, 200 * Time.deltaTime);
            }         
        }
    }

    void ResetGun()
    {
        if (gunOne.transform.eulerAngles.z < 359 || gunOne.transform.eulerAngles.z == 270)
        {
            gunOne.transform.Rotate(0, 0, -200 * Time.deltaTime);
            gunTwo.transform.Rotate(0, 0, 200 * Time.deltaTime);
        }
        else
        {
            ready = false;
        }

        if (gunPivot.transform.eulerAngles.z < 1 || gunPivot.transform.eulerAngles.z > 0)
        {
            gunPivot.transform.Rotate(0, 0, 0.5f * Time.deltaTime);
        }
    }
}
