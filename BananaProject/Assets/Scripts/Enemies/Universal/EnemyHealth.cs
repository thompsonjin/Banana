using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private PlayerController p_Con;
    private BaseEnemy b_En;

    public bool saki;

    [SerializeField] private int health;
    [SerializeField] private int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        p_Con = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        b_En = this.gameObject.GetComponent<BaseEnemy>();
        health = maxHealth;
    }

    public void Damage(int d)
    {
        health -= d;

        if(health <= 0)
        {
            if (saki)
            {
                p_Con.sakiBoost = true;
            }
            p_Con.GiveBanana(1);
            Destroy(this.gameObject);
        }
    }

    public void Hit()
    {

    }
}
