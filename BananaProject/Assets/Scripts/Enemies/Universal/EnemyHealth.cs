using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private PlayerController p_Con;
    private BaseEnemy b_En;
    private ScoreDisplay score;
    [SerializeField] ParticleSystem spark;

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
        if (GameObject.FindWithTag("Score"))
        {
            score = GameObject.FindWithTag("Score").GetComponent<ScoreDisplay>();
        }
        health = maxHealth;
    }

    public void Damage(int d)
    {
        health -= d;

        spark.Play();

        if(health <= 0)
        {
            switch (type)
            {
                case 1:
                    //Baboon
                    if (score != null)
                    {
                        score.IncreaceCurrentScore(100);
                    }
                    break;
                case 2:
                    //Proboscis
                    if (score != null)
                    {
                        score.IncreaceCurrentScore(150);
                    }

                    break;
                case 3:
                    if (score != null)
                    {
                        score.IncreaceCurrentScore(50);
                    }
                    //Tiny monkey
                    break;
                case 4:
                    if (score != null)
                    {
                        score.IncreaceCurrentScore(200);
                    }
                    //Chimp
                    break;
                case 5:
                    //Saki
                    p_Con.sakiBoost = true;
                    p_Con.sakiBoostIndicator.SetActive(true);
                    if (score != null)
                    {
                        score.IncreaceCurrentScore(200);
                    }
                    break;
            }
            p_Con.GiveBanana(1);
            Destroy(this.gameObject);
        }
    }

}
