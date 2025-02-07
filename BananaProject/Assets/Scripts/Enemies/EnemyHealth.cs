using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void Damage(int d)
    {
        health -= d;

        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
