using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformPoint : MonoBehaviour
{
    public void Delete()
    {
        DestroyImmediate(this.gameObject, true);
    }
}
