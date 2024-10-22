using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Using tutorial: https://youtu.be/Xm9_rcmv3UU?si=ZywbhLRViElcfcFj
public class SwipeDetection : MonoBehaviour
{
    public static SwipeDetection instance;

    public delegate void Swipe(Vector2 direction);
    public event Swipe swipePerformed;

    [SerializeField] private InputAction position, press;
    [SerializeField] private float swipeResistance = 100; // threshold for touch to be considered a swipe, measured in pixels. //TODO: make reqd swiping legth dependent on screen size
    private Vector2 initialPos;
    private Vector2 currentPos => position.ReadValue<Vector2>();


    void Awake()
    {
        position.Enable();
        press.Enable();
        //press.performed += _ => { initialPos = currentPos; };
        press.performed += _ => { initialPos = currentPos; Debug.Log("[SWIPE DETECTION] Swipe started"); };
        press.canceled += _ => DetectSwipe();

        instance = this;
    }

    void DetectSwipe()
    {
        Vector2 delta = currentPos - initialPos;
        Vector2 direction = Vector2.zero;

        // check if touch was long enough to be considered a swipe
        if(Mathf.Abs(delta.x) > swipeResistance)
        {
            direction.x = Mathf.Clamp(delta.x, -1, 1);
        }
        if (Mathf.Abs(delta.y) > swipeResistance)
        {
            direction.y = Mathf.Clamp(delta.y, -1, 1);
        }

        // check if swipe was detected
        if(direction != Vector2.zero && swipePerformed != null)
        {
            Debug.Log("[SWIPE DETECTION] Swipe detected: " + direction);
            swipePerformed(direction);
        }
    }
}
