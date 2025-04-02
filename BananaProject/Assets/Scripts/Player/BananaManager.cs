using System.Collections;
using System.Collections.Generic;
//using UnityEditor.PackageManager.Requests;
using UnityEngine;

public static class BananaManager
{
    public static int startingBananas = 5;
    public static int MaxBananas { get; private set; } = startingBananas;

    static public bool one;
    static public bool two;
    static public bool three;
    static public bool four;
    static public bool five;

    public static void IncreaseMaxBananas()
    {
        MaxBananas++;

        switch (GameManager.Instance.GetScene())
        {
            case 5:
                one = true;
                break;
            case 6:
                two = true;
                break;
            case 7:
                three = true;
                break;
            case 8:
                four = true;
                break;
            case 9:
                five = true;
                break;
        }
    }
}
