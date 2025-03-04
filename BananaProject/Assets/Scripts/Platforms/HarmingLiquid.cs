using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HarmingLiquid : MonoBehaviour
{
    public float riseSpeed;
    public float fallSpeed;
    public GameObject liquid;

    [Header("Water Type")]
    public bool rising;
    public bool ebbing;


    [Header("Rising Positions")]
    public Transform startPos;
    public Transform endPos;

    bool up = true;


    private void Update()
    {
        if (rising)
        {
            RisingWater();
        }
        else if (ebbing)
        {
            EbbingWater();
        }
    }

    void RisingWater()
    {
        liquid.transform.position = Vector3.MoveTowards(this.transform.position, endPos.position, riseSpeed * Time.deltaTime);
    }

    void EbbingWater()
    {
        if (up)
        {
            liquid.transform.position = Vector3.MoveTowards(liquid.transform.position, endPos.position, riseSpeed * Time.deltaTime);

            if(liquid.transform.position.y >= endPos.position.y)
            {
                up = false;
            }
           
        }
        else
        {
            liquid.transform.position = Vector3.MoveTowards(liquid.transform.position, startPos.position, fallSpeed * Time.deltaTime);

            if (liquid.transform.position.y <= startPos.position.y)
            {
                up = true;
            }
        }
        
    }
}
