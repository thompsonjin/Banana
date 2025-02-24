using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : MonoBehaviour
{
    [SerializeField] private float damage = 1f;
    [SerializeField] private float knockbackForce = 5f;
    [SerializeField] private float upwardForce = 5f;
    private bool isThrown = false;
    private Rigidbody2D rb;
    private Collider2D col;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    public void OnPickup()
    {
        rb.isKinematic = true;
        col.enabled = false;
    }

    public void Throw(Vector2 direction, float throwForce)
    {
        rb.isKinematic = false;
        col.enabled = true;
        isThrown = true;

        //Force applied when thrown
        Vector2 throwVector = (direction * throwForce) + (Vector2.up * upwardForce);
        rb.AddForce(throwVector, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isThrown) return;

        //Checks for enemy collision
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.TryGetComponent<RoboMonkeyAI>(out RoboMonkeyAI e_Ai))
            {
                e_Ai.SetHit();
                e_Ai.SetPatrol(false);
                collision.gameObject.GetComponent<EnemyHealth>().Damage((int)damage);

                Vector2 knockbackDir = (collision.transform.position - transform.position).normalized;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
            }
            Destroy(gameObject);
        }
        
        else if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    
    }
}
