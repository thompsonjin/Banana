using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private PlayerController p_Con;

    [SerializeField] private int health;
    [SerializeField] private int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        p_Con = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        health = maxHealth;
    }

    public void Damage(int d)
    {
        health -= d;

        if(health <= 0)
        {
            p_Con.GiveBanana(1);
            Destroy(this.gameObject);
        }
    }
}
