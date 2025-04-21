using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScrectLVLPannel : MonoBehaviour
{
    public Text bananaText;
    public Text timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bananaText.text = BananaScore.instance.bananaScore.ToString();
        timer.text = BananaScore.instance.timer.ToString("0");
    }
}
