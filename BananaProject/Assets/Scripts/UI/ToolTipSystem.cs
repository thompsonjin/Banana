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

        Vector2 tooltipPosition = mousePosition + new Vector2(padding.x, padding.y);

        tooltipPosition.y += rectTransform.rect.height / 2;

        rectTransform.pivot = new Vector2(0, 1);

        rectTransform.position = tooltipPosition;

        Vector2 screenBounds = new Vector2(Screen.width, Screen.height);
        Vector2 tooltipSize = rectTransform.rect.size;

        if (tooltipPosition.x + tooltipSize.x > screenBounds.x)
        {
            tooltipPosition.x = mousePosition.x - tooltipSize.x - padding.x;
        }

        if (tooltipPosition.y + tooltipSize.y > screenBounds.y)
        {
            tooltipPosition.y = mousePosition.y - tooltipSize.y - padding.y;
        }

        rectTransform.position = tooltipPosition;
    }
}
