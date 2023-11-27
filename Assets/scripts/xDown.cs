using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class xDown : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    car car;
    [HideInInspector]
    public bool pressed;
    float x;
    public xUp xUp;
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
            if (!xUp.pressed)
            {
                if (pressed)
                {
                    if (Vector3.Angle(car.transform.forward, car.transform.GetComponent<Rigidbody>().velocity) < 90 && car.transform.GetComponent<Rigidbody>().velocity.magnitude > 1)
                    {
                        car.breakDown();
                    }
                    else
                    {
                        car.breakUp();
                        x -= Time.fixedDeltaTime;
                    }
                }
                else
                {
                    car.breakUp();
                    x += Time.fixedDeltaTime;
                }
                x = Mathf.Clamp(x, -1, 0);
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