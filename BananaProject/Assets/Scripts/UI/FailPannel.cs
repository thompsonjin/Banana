using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FailPannel : MonoBehaviour
{
   public void GoToBoss()
   {
        Time.timeScale = 1;
        CheckpointManager.CheckpointReset();
        SceneManager.LoadScene("Boss Fight");
   }
}
