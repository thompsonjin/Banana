using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SavePannel : MonoBehaviour
{
    public GameObject pausePannel;
    public Button  retrurn;  // Start is called before the first frame update
    void Start()
    {
        
        retrurn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);

            if (SceneManager.GetActiveScene().buildIndex==1)
            {
                pausePannel.SetActive(true);
            }
         
            
            
            
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
