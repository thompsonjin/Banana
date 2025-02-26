using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    protected float hitTimer;
    protected bool hit;
    protected bool patrol;

    public void SetHit()
    {
        hit = !hit;
        hitTimer = .3f;
    }

    public void SetPatrol(bool b)
    {
        patrol = b;
    }
}
