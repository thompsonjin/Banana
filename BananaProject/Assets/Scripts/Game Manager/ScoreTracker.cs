using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{

    public Text scoreText;
    public Text timerText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = CompleteScoreDisplay.score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        CompleteScoreDisplay.time += Time.deltaTime;
        timerText.text = Mathf.RoundToInt(CompleteScoreDisplay.time).ToString();
    }

    public void IncreaceScore(int s)
    {
        CompleteScoreDisplay.score += s;
        scoreText.text = CompleteScoreDisplay.score.ToString();
    }

    public void ResetScore()
    {
        CompleteScoreDisplay.score = 0;
        CompleteScoreDisplay.time = 0;
    }

    public void SetBossWin()
    {
        CompleteScoreDisplay.boss = true;
    }
}
