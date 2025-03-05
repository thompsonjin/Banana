using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditPannel : MonoBehaviour
{
    public  Button  Return;
    // Start is called before the first frame update
    void Start()
    {
        Return.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            
            
            
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
