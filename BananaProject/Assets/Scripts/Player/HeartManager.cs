using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartManager : MonoBehaviour
{
    public static int MaxHearts { get; private set; } = 3;

    public static bool heartOne = false;
    public static bool heartTwo = false;

    public static void IncreaseMaxHearts()
    {
        MaxHearts++;
    }
}
