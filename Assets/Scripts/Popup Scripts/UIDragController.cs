using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragController : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    private UIRotateController uIRotateController;

    private float startPosX;

    private float startPosY;


    public void dragHandler(BaseEventData data){
        if(Input.touchCount == 1){
            uIRotateController = UIRotateController.Instance;
            PointerEventData pointerData = (PointerEventData)data;

            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)canvas.transform,
                pointerData.position,
                canvas.worldCamera,
                out position
            );

            transform.position = canvas.transform.TransformPoint(position.x ,position.y,0);
        }
        
        //uIRotateController.Rotate(transform);
    }

}
