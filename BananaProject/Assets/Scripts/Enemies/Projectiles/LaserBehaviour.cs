using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LaserBehaviour : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] float speed;
    private Vector3 target;
    public bool tracking;
    public bool playerBullet;
    public bool random;

    float decay;
    public float maxDecayTime;

    public LayerMask enemyLayer;
    float closestDist;
    GameObject closest;
    bool posTaken;

    // Start is called before the first frame update
    void Start()
    {
        if (random)
        {
            SetBossTarget();
        }
        else
        {
            SetTargetPlayer();
        }

        decay = maxDecayTime;
    }

    // Update is called once per frame
    void Update()
    {
          
    }

    private void FixedUpdate()
    {
        if (tracking)
        {
            if (!playerBullet)
            {
                SetTargetPlayer();
                rb.velocity = new Vector3(-target.normalized.x * speed, -target.normalized.y * speed, 0);
                decay -= Time.deltaTime;
                if(decay <= 0)
                {
                    Destroy(this.gameObject);
                }
            }
            else
            {
                SetTargetNearestEnemy();
                decay -= Time.deltaTime;
                if (decay <= 0)
                {
                    Destroy(this.gameObject);
                }
            }
        }
        else
        {
            rb.velocity = new Vector3(-target.normalized.x * speed, -target.normalized.y * speed, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && !playerBullet)
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage();
            Destroy(this.gameObject);
        }
        
        if(collision.gameObject.tag == "Enemy" && playerBullet)
        {
            if (collision.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth component))
            {
                collision.gameObject.GetComponent<EnemyHealth>().Damage(4);
                Destroy(this.gameObject);
            }      
        }

        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Throwable")
        {
            Destroy(this.gameObject);
        }
        
    }

    public void SetBossTarget()
    {
        GameObject boss = GameObject.Find("Robot");

        target = this.transform.position - boss.transform.position;
        target = target.normalized;
        target = -target;
    }

    public void SetTargetPlayer()
    {
        GameObject player = GameObject.Find("Player");

        if (player != null)
        {
            target = this.transform.position - player.transform.position;
        }
    }

    public void SetTargetNearestEnemy()
    {
        if(HitRange().Length > 0)
        {
            foreach (Collider2D e in HitRange())
            {
                float dist = Vector3.Distance(e.gameObject.transform.position, transform.position);

                if (dist < closestDist || closestDist == 0)
                {
                    closestDist = dist;
                }

                if (closestDist == dist)
                {
                    target = this.transform.position - e.gameObject.transform.position;
                    target = target.normalized;
                }
                rb.velocity = new Vector3(-target.normalized.x * speed, -target.normalized.y * speed, 0);
            }
        }
        else
        {
            GameObject player = GameObject.Find("Player");

            if(player != null && posTaken)
            {
                target = -(this.transform.position - player.transform.position);
                target = target.normalized;
                posTaken = true;
            }   
            rb.velocity = new Vector3(target.normalized.x * speed, 0, 0);
        }      
    }

    private Collider2D[] HitRange()
    {
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(transform.position, 10, enemyLayer);

        return enemiesHit;
    }
}
