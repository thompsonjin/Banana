using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CheckpointManager
{
    public static int checkpointNum;
    public static int lastLevel = 6;

    public static void NextCheckpoint()
    {
        checkpointNum++;
    }

    public static void CheckpointReset()
    {
        checkpointNum = 0;
    }
}
