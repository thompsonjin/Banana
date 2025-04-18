using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreDisplay : MonoBehaviour
{
    [Header("Display")]
    [SerializeField] Text scoreText;
    [SerializeField] Text timeText;
    [SerializeField] Text deathText;

    [Header("Scoreboard Menu")]
    [SerializeField] bool scoreboard;
    bool win;

    [SerializeField] GameObject gameWinText;
    [SerializeField] GameObject lvlCompleteText;


    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 5)
        {
            ResetTotals();
        }

        if(SceneManager.GetActiveScene().buildIndex == 15)
        {
            deathText.text = ScoreTracker.deaths.ToString();
        }

        if(ScoreTracker.lastScene == 15)
        {
            win = true;
        }

        if (scoreboard)
        {
            HandleScoreboard();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!scoreboard)
        {
            ScoreTracker.currentTime += Time.deltaTime;
            timeText.text = Mathf.RoundToInt(ScoreTracker.currentTime).ToString();
        }    
    }

    //SCORE MANAGEMENT
    public void IncreaceCurrentScore(int s)
    {
        ScoreTracker.currentScore += s;
        scoreText.text = ScoreTracker.currentScore.ToString();
    }

    public void IncreaceDeaths(int d)
    {
        ScoreTracker.deaths += d;
    }

    public void ResetCurrentScore()
    {
        ScoreTracker.currentScore = 0;
    }

    public void ResetCurrentTime()
    {
        ScoreTracker.currentTime = 0;
    }

    public void Cache()
    {
        ScoreTracker.totalScore += ScoreTracker.currentScore;
        ScoreTracker.totalTime += ScoreTracker.currentTime;

        ScoreTracker.currentScore = 0;
        ScoreTracker.currentTime = 0;
    }

    public void ResetTotals()
    {
        ScoreTracker.totalScore = 0;
        ScoreTracker.totalTime = 0;
    }

    //SCOREBOARD MANAGEMENT

    void HandleScoreboard()
    {
        if (win)
        {
            lvlCompleteText.SetActive(false);
        }
        else
        {
            gameWinText.SetActive(false);
        }

        scoreText.text = ScoreTracker.totalScore.ToString();
        timeText.text = Mathf.RoundToInt(ScoreTracker.totalTime).ToString(); ;
    }

    public void NextElevator()
    {
        SceneManager.LoadScene(ScoreTracker.lastScene + 1);
    }
}
