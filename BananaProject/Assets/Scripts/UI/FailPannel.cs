using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FailPannel : MonoBehaviour
{
   public void GoToBossLose()
   {
        Time.timeScale = 1;
        CheckpointManager.CheckpointReset();
        SceneManager.LoadScene("Boss Fight");
   }

    public void GoToBossWin()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().IncreaseMaxHealth();
        Time.timeScale = 1;
        CheckpointManager.CheckpointReset();
        SceneManager.LoadScene("Boss Fight");
    }
}
