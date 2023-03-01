using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine;
using UnityEngine.UI;

public class UiRotation : MonoBehaviour
{
    private bool _isRotating;
    private Vector2 _initialTouchPosition;
    private float _initialRotation;

    public RawImage image1;
    public RawImage image2;

    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                // Check which image is being touched by either touch
                if (RectTransformUtility.RectangleContainsScreenPoint(image1.rectTransform, touch1.position) || RectTransformUtility.RectangleContainsScreenPoint(image1.rectTransform, touch2.position))
                {
                    _isRotating = true;
                    _initialTouchPosition = touch1.position - touch2.position;
                    _initialRotation = image1.rectTransform.localEulerAngles.z;
                }
                else if (RectTransformUtility.RectangleContainsScreenPoint(image2.rectTransform, touch1.position) || RectTransformUtility.RectangleContainsScreenPoint(image2.rectTransform, touch2.position))
                {
                    _isRotating = true;
                    _initialTouchPosition = touch1.position - touch2.position;
                    _initialRotation = image2.rectTransform.localEulerAngles.z;
                }
            }
            else if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                // Calculate the rotation angle using the DeltaPosition vectors
                Vector2 currentTouchPosition = touch1.position - touch2.position;
                float angle = Vector2.SignedAngle(_initialTouchPosition, currentTouchPosition);

                // Apply the initial rotation to get the current rotation
                angle += _initialRotation;

                // Rotate the image by the calculated angle
                if (RectTransformUtility.RectangleContainsScreenPoint(image1.rectTransform, touch1.position) || RectTransformUtility.RectangleContainsScreenPoint(image1.rectTransform, touch2.position))
                {
                    image1.rectTransform.localEulerAngles = new Vector3(0, 0, angle);
                }
                else if (RectTransformUtility.RectangleContainsScreenPoint(image2.rectTransform, touch1.position) || RectTransformUtility.RectangleContainsScreenPoint(image2.rectTransform, touch2.position))
                {
                    image2.rectTransform.localEulerAngles = new Vector3(0, 0, angle);
                }
            }
            else if (touch1.phase == TouchPhase.Ended || touch1.phase == TouchPhase.Canceled || touch2.phase == TouchPhase.Ended || touch2.phase == TouchPhase.Canceled)
            {
                _isRotating = false;
            }
        }
    }
}
