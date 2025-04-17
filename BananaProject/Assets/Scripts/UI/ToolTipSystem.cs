using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToolTipSystem : MonoBehaviour
{
    public static ToolTipSystem Instance;

    [SerializeField] private GameObject tooltipContainer;
    [SerializeField] private TextMeshProUGUI tooltipText;

    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 padding = new Vector2(20, 10);

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        // Hide tooltip initially
        HideTooltip();
    }

    public void ShowTooltip(string content)
    {
        tooltipText.text = content;
        tooltipContainer.SetActive(true);

        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);

        UpdatePosition();
    }

    public void HideTooltip()
    {
        tooltipContainer.SetActive(false);
    }

    private void Update()
    {
        if (tooltipContainer.activeSelf)
        {
            UpdatePosition();
        }
    }

    private void UpdatePosition()
    {
        Vector2 mousePosition = Input.mousePosition;

        if (canvas.renderMode == RenderMode.ScreenSpaceCamera || canvas.renderMode == RenderMode.WorldSpace)
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.GetComponent<RectTransform>(), mousePosition, canvas.worldCamera, out localPoint);
            mousePosition = localPoint;
        }

        float pivotX = mousePosition.x / canvas.GetComponent<RectTransform>().sizeDelta.x;
        float pivotY = mousePosition.y / canvas.GetComponent<RectTransform>().sizeDelta.y;

        pivotX = Mathf.Clamp(pivotX, 0.1f, 0.9f);
        pivotY = Mathf.Clamp(pivotY, 0.1f, 0.9f);

        rectTransform.pivot = new Vector2(pivotX, pivotY);
        rectTransform.position = mousePosition + new Vector2(padding.x, padding.y);
    }
}
