using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float shotSpeed = 20f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int damage = 4;


    private void Update()
    {
        StartCoroutine(Countdown(3));
    }

    void Start()
    {
        rb.velocity = transform.right * shotSpeed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        BaseEnemy e_Ai = hitInfo.GetComponent<BaseEnemy>();
        EnemyHealth e_Health = hitInfo.GetComponent<EnemyHealth>();

        if (e_Health != null && e_Ai != null)
        {
            e_Ai.SetHit();
            e_Ai.SetPatrol(false);
            e_Health.Damage(damage);
            Destroy(gameObject);
        }
    }
    
    private IEnumerator Countdown(int seconds)
    {
        int counter = seconds;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
        }
        Destroy(gameObject);
    }
}

