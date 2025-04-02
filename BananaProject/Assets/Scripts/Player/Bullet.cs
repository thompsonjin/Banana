using System.Collections;
using System.Collections.Generic;
//using UnityEditor.PackageManager;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float shotSpeed = 20f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int damage = 6;


    private void Update()
    {
        StartCoroutine(Countdown(3));
    }

    void Start()
    {
        rb.velocity = transform.right * shotSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BaseEnemy e_Ai = collision.gameObject.GetComponent<BaseEnemy>();
        EnemyHealth e_Health = collision.gameObject.GetComponent<EnemyHealth>();

        if (e_Health != null && e_Ai != null)
        {
            e_Ai.SetHit();
            e_Ai.SetPatrol(false);
            e_Health.Damage(damage);
        }

        Destroy(gameObject);
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

