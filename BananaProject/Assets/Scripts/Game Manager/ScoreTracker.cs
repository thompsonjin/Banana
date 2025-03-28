using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    float score;
    float lvlTimer;

    public Text scoreText;
    public Text timerText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lvlTimer += Time.deltaTime;
        timerText.text = Mathf.RoundToInt(lvlTimer).ToString();
    }

    public void IncreaceScore(int s)
    {
        score += s;
        scoreText.text = score.ToString();
    }
}
