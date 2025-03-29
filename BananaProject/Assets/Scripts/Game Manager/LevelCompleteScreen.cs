using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteScreen : MonoBehaviour
{

    public void Elevator()
    {
        int num = CheckpointManager.lastLevel;
        CheckpointManager.lastLevel += 2;
        SceneManager.LoadScene(num);
    }
}
