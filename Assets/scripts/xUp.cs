using UnityEngine;
using UnityEngine.EventSystems;

public class xUp : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    car car;
    [HideInInspector]
    public bool pressed;
    float x;
    public xDown xDown;
    ingameUiController controller;

    private void Start()
    {
        car = FindObjectOfType<car>();
        pressed = false;
        controller = FindObjectOfType<ingameUiController>();
    }

    private void FixedUpdate()
    {
        if (controller.carControlable)
        {
            if (!xDown.pressed)
            {
                if (pressed) { x += Time.fixedDeltaTime; }
                else { x -= Time.fixedDeltaTime; }
                x = Mathf.Clamp01(x);
                car.x = x;
            }
        }
    }

    void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        pressed = true;
    }

    void IPointerUpHandler.OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
    {
        pressed = false;
    }
}