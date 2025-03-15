using UnityEngine;
using UnityEngine.UI;

public class ButtonTester : MonoBehaviour
{
    [SerializeField] private Button buttonToTest;

    void Start()
    {
        if (buttonToTest != null)
        {
            buttonToTest.onClick.AddListener(OnButtonClicked);
            Debug.Log("ButtonTester initialized");
        }
        else
        {
            Debug.LogError("No button assigned to test!");
        }
    }

    void OnButtonClicked()
    {
        Debug.Log("BUTTON WAS CLICKED!");
    }
}
