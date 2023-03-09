using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiRotation : MonoBehaviour
{
    public Image image;

    private bool _isRotating;
    private Vector2 _initialTouchPosition;
    private float _initialRotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchSupported)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    if (Input.touches.Length == 2)
                    {
                        Touch touch1 = Input.GetTouch(0);
                        Touch touch2 = Input.GetTouch(1);

                        if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
                        {
                            // Check which image is being touched by either touch
                            if (RectTransformUtility.RectangleContainsScreenPoint(image.rectTransform, touch1.position) || RectTransformUtility.RectangleContainsScreenPoint(image.rectTransform, touch2.position))
                            {
                                _isRotating = true;
                                _initialTouchPosition = touch1.position - touch2.position;
                                _initialRotation = image.rectTransform.localEulerAngles.z;
                            }
                        }
                        if(touch1.phase == TouchPhase.Stationary && touch2.phase == TouchPhase.Moved){

                            Vector2 currentTouchPosition = touch1.position - touch2.position;
                            float angle = Vector2.SignedAngle(_initialTouchPosition, currentTouchPosition);

                            // Apply the initial rotation to get the current rotation
                            angle += _initialRotation;

                            // Rotate the image by the scaled down angle
                            transform.Rotate(Vector3.forward, angle);
                        }
                        else if (touch1.phase == TouchPhase.Ended || touch1.phase == TouchPhase.Canceled || touch2.phase == TouchPhase.Ended || touch2.phase == TouchPhase.Canceled)
                        {
                            _isRotating = false;
                        }
                    }
                }
            }
        }
    }
}
