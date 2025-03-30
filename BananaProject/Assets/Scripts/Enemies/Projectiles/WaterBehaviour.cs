using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WaterBehaviour : MonoBehaviour
{
    private Vector3 scaleChange;
    private Vector3 correction;
    private Vector3 frontCorrection;
    private RoboChimp r_Chi;

    public float scaleSpeed;
    public float corSpeed;
    public float frontCorSpeed;

    [SerializeField] GameObject front;
    [SerializeField] GameObject rear;

    // Start is called before the first frame update
    void Start()
    {
        r_Chi = this.transform.parent.parent.gameObject.GetComponent<RoboChimp>();
        scaleChange = new Vector3(scaleSpeed, 0, 0);

        if (r_Chi.isFacingRight)
        {
            correction = new Vector3(corSpeed, 0, 0);
            frontCorrection = new Vector3(frontCorSpeed, 0, 0);
        }
        else
        {
            correction = new Vector3(-corSpeed, 0, 0);
            frontCorrection = new Vector3(-frontCorSpeed, 0, 0);
        }           
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.localScale.x <= 20)
        {
            rear.transform.localScale += scaleChange * 3;
            rear.transform.position += correction * 3;
            front.transform.position += frontCorrection * 3;
        }  
    }

    
}
