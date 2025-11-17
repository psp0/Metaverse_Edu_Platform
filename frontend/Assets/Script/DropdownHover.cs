using UnityEngine;
using UnityEngine.EventSystems;

public class DropdownHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject dropdownPanel;
    private static bool isPointerOverGroup = false; // 버튼+패널 전체에 적용할 그룹 상태
    public float closeDelay = 0.2f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOverGroup = true;
        dropdownPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOverGroup = false;
        Invoke(nameof(CheckClose), closeDelay);
    }

    private void CheckClose()
    {
        if (!isPointerOverGroup)
            dropdownPanel.SetActive(false);
    }
}
