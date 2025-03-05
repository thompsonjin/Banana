using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public static class BananaManager
{
    public static int startingBananas = 5;
    public static int MaxBananas { get; private set; } = startingBananas;


    public static void IncreaseMaxBananas()
    {
        MaxBananas++;
    }
}
