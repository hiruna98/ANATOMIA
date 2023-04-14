using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RadialMenuDragController : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    private UIRotateController uIRotateController;

    private bool rotationEnable = true;

    private DataStore dataStore;
    

    public void dragHandler(BaseEventData data){
        uIRotateController = UIRotateController.Instance;
        dataStore = DataStore.Instance;
        PointerEventData pointerData = (PointerEventData)data;

        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            pointerData.position,
            canvas.worldCamera,
            out position
        );

        transform.position = canvas.transform.TransformPoint(position.x,position.y,0);
        uIRotateController.Rotate(transform,transform);
        RotateUI();

        // Debug.Log(transform.position.y);
        if(transform.position.y < 0 + dataStore.getDividingLineY())
        {
            dataStore.setIsRadialTop(false);
        }else
        {
            dataStore.setIsRadialTop(true);
        }
    }

    public void RotateUI()
    {
        GameObject[] popupList = GameObject.FindGameObjectsWithTag("popup");
        foreach(GameObject popup in popupList)
        {
            // popup.transform.rotation = Quaternion.Euler(0,0,transform.rotation.z);
            string key = popup.name + " RotationLock";
            int rotationLock = PlayerPrefs.GetInt(key);
            if(rotationLock == 0){
                uIRotateController.Rotate(transform,popup.transform);
            }
        }
    }

}
