using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopup : MonoBehaviour
{
    private GameObject UIWrapper;
    private Transform UIWrapperTransform;


    void Start()
    {
        // UIWrapperTransform = UIWrapper.transform;
        transform.localScale = new Vector2(0f, 0f);
    }

    public void Open()
    {
        Debug.Log("called" + transform.localScale + transform.position);
        // Passses an Vector2 of X,Y values set to 1 and length of the animation
        // UIWrapperTransform.LeanScale(new Vector2(1f, 1f), 0.8f);
        transform.localScale = new Vector2(1f, 1f);
        Debug.Log("called2" + transform.localScale + transform.position);


    }

    public void Close()
    {
        // UIWrapperTransform.LeanScale(new Vector2(0f, 0f), 1f).setEaseInBack();
        transform.localScale = new Vector2(0f, 0f);

    }
}
