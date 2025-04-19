using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ComicClickThrough : MonoBehaviour
{
    [SerializeField] private int mainMenuSceneIndex = 1;

    [SerializeField] Image panelOne;
    [SerializeField] Image panelTwo;
    [SerializeField] Image panelThree;

    float alphaOne;
    float alphaTwo;
    float alphaThree;
    bool change = true;

    [SerializeField] int clicks;

    private void Update()
    {
        //Check for any key press or mouse click
        if ((Input.anyKeyDown || Input.GetMouseButtonDown(0)))
        {
            switch (clicks)
            {
                case 0:
                    change = false;
                    alphaOne = 1;
                    panelOne.color = new Color(1, 1, 1, alphaOne);
                    clicks++;
                    change = true;
                    break;
                case 1:
                    change = false;
                    alphaTwo = 1;
                    panelTwo.color = new Color(1, 1, 1, alphaTwo);
                    clicks++;
                    change = true;
                    break;
                case 2:
                    change = false;
                    alphaThree = 1;
                    panelThree.color = new Color(1, 1, 1, alphaThree);
                    clicks++;
                    break;
                case 3:
                    LoadNextScene();
                    break;
            }
        }
    }
    private void FixedUpdate()
    {
        if (change)
        {
            if (alphaOne < 1)
            {
                alphaOne += Time.fixedDeltaTime;
                panelOne.color = new Color(1, 1, 1, alphaOne);
            }
            else
            {
                clicks = 1;
                if (alphaTwo < 1)
                {
                    alphaTwo += Time.fixedDeltaTime;
                    panelTwo.color = new Color(1, 1, 1, alphaTwo);
                }
                else
                {
                    clicks = 2;
                    if (alphaThree < 1)
                    {
                        alphaThree += Time.fixedDeltaTime;
                        panelThree.color = new Color(1, 1, 1, alphaThree);
                    }
                    else
                    {
                        clicks = 3;
                    }
                }
            }
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(mainMenuSceneIndex);
    }
}
