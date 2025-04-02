using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCompleteScreen : MonoBehaviour
{
    public float score;
    public float time;

    public bool boss;

    public Text displayScore;
    public Text displayTime;

    public GameObject winner;
    public GameObject level;

    private void Start()
    {
        boss = CompleteScoreDisplay.boss;
        score = CompleteScoreDisplay.score;
        time = CompleteScoreDisplay.time;


        if (!boss)
        {
            Destroy(winner);
        }
        else
        {
            Destroy(level);
        }  

        displayScore.text = score.ToString();
        displayTime.text = Mathf.RoundToInt(time).ToString();
    }

    public void Elevator()
    {
        int num = CheckpointManager.lastLevel;
        CheckpointManager.lastLevel += 2;
        SceneManager.LoadScene(num);
    }
}
