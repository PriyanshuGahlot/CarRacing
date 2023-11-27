using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class xUp : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    car car;
    [HideInInspector]
    public bool pressed;
    float x;
    public xDown xDown;
    ingameUiController controller;

    Sprite original;
    public Image image;
    public Sprite pressedImage;


    private void Start()
    {
        car = FindObjectOfType<car>();
        pressed = false;
        controller = FindObjectOfType<ingameUiController>();
        original = image.sprite;
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
        image.sprite = pressedImage;
    }

    void IPointerUpHandler.OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
    {
        pressed = false;
        image.sprite = original;
    }
}