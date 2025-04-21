using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [TextArea(3, 10)]
    public string tooltipContent;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!string.IsNullOrEmpty(tooltipContent))
            ToolTipSystem.Instance.ShowTooltip(tooltipContent);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipSystem.Instance.HideTooltip();
    }

    private void OnDisable()
    {
        ToolTipSystem.Instance?.HideTooltip();
    }
}