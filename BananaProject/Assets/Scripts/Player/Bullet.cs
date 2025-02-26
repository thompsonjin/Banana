using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float shotSpeed = 20f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int damage = 4;

    void Start()
    {
        rb.velocity = transform.right * shotSpeed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        RoboMonkeyAI e_Ai = hitInfo.GetComponent<RoboMonkeyAI>();
        EnemyHealth e_Health = hitInfo.GetComponent<EnemyHealth>();

        if (e_Health != null && e_Ai != null)
        {
            e_Ai.SetHit();
            e_Ai.SetPatrol(false);
            e_Health.Damage(damage);
            Destroy(gameObject);
        }
        
    }

}
