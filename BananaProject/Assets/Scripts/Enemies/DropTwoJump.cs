using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTwoJump : MonoBehaviour
{
    public GameObject prefab;

    public void DropTwoJumpItem()
    {
        GameObject item = Instantiate(prefab, transform.parent.parent.position, Quaternion.identity);
    }
}
