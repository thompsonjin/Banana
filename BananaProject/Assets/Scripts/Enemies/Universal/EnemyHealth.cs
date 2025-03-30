using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private PlayerController p_Con;
    private BaseEnemy b_En;
    private ScoreTracker score;

    public int type;
    //1 Baboon
    //2 Proboscis
    //3 Tiny monkey
    //4 Chimp
    //5 Saki

    [SerializeField] private int health;
    [SerializeField] private int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        p_Con = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        b_En = this.gameObject.GetComponent<BaseEnemy>();
        score = GameObject.FindWithTag("Score").GetComponent<ScoreTracker>();
        health = maxHealth;
    }

    public void Damage(int d)
    {
        health -= d;

        if(health <= 0)
        {
            switch (type)
            {
                case 1:
                    //Baboon
                    score.IncreaceScore(100);
                    break;
                case 2:
                    //Proboscis
                    score.IncreaceScore(150);
                    break;
                case 3:
                    score.IncreaceScore(50);
                    //Tiny monkey
                    break;
                case 4:
                    score.IncreaceScore(200);
                    //Chimp
                    break;
                case 5:
                    //Saki
                    p_Con.sakiBoost = true;
                    p_Con.sakiBoostIndicator.SetActive(true);
                    score.IncreaceScore(200);
                    break;
            }
            p_Con.GiveBanana(1);
            Destroy(this.gameObject);
        }
    }

}
