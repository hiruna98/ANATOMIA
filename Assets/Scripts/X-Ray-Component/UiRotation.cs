using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiRotation : MonoBehaviour
{
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
                    // Calculate the rotation angle using the DeltaPosition vectors
                    float angle = Mathf.Atan2(touch.deltaPosition.y, touch.deltaPosition.x) * Mathf.Rad2Deg;

                    // Scale down the rotation angle by a factor
                    angle *= 0.01f;

                    // Rotate the image by the scaled down angle
                    transform.Rotate(Vector3.forward, angle);
                }
            }
        }
    }
}


}
