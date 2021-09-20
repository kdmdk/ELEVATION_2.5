using UnityEngine;
using UnityEngine.EventSystems;

public class FixedFloatingJoystick : Joystick
{
    Vector2 InitPos = Vector2.zero;
    protected override void Start()
    {
        InitPos = background.anchoredPosition;
        base.Start();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        background.anchoredPosition = InitPos;
        base.OnPointerUp(eventData);
    }
}