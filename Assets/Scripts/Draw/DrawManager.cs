using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DrawManager : MonoBehaviour
{
    public GameObject drawPrefab;
    public Material drawMaterial;
    private Color materialColor;

    GameObject theTrail;
    Plane planeObj;
    Vector3 startPos;
    Camera mainCamera;
    Camera uiCamera;

    private string drawObjectTag = "drawInstance";
    private bool isDrawActive = false;

    private GameObject radialMenuContainer;
    private GameObject viewModalContainer;
    private GameObject functionsModalContainer;
    private GameObject infoModalContainer;
    private GameObject layersModalContainer;
    private GameObject drawModalContainer;
    private GameObject colorModalContainer;

    private DataStore dataStore;

    private Button viewBtn;
    private Button functionBtn;
    private Button layersBtn;

    private TrailRenderer trailRenderer;

    void Start()
    {
        radialMenuContainer = GameObject.Find("Radial Menu Container");
        viewModalContainer = GameObject.Find("View Modal Container");
        functionsModalContainer = GameObject.Find("Functions Modal Container");
        infoModalContainer = GameObject.Find("Info Modal Container");
        layersModalContainer = GameObject.Find("Layers Modal Container");
        drawModalContainer = GameObject.Find("Draw Modal Container");
        colorModalContainer = GameObject.Find("Draw Modal Container/Color Modal Container");

        dataStore = DataStore.Instance;

        viewBtn = GameObject
            .Find("Radial Menu/Background/Elements/View/Button")
            .GetComponent<Button>();
        functionBtn = GameObject
            .Find("Radial Menu/Background/Elements/Functions/Button")
            .GetComponent<Button>();
        layersBtn = GameObject
            .Find("Radial Menu/Background/Elements/Layers/Button")
            .GetComponent<Button>();

        trailRenderer = drawPrefab.GetComponent<TrailRenderer>();

        if (drawPrefab == null)
        {
            Debug.Log("drawPrefab not found.");
        }
        if (drawMaterial == null)
        {
            Debug.Log("drawMaterial not found.");
        }

        if (radialMenuContainer == null)
        {
            Debug.Log("radialMenuContainer not found.");
        }
        if (viewModalContainer == null)
        {
            Debug.Log("viewModalContainer not found.");
        }
        if (functionsModalContainer == null)
        {
            Debug.Log("functionsModalContainer not found.");
        }
        if (infoModalContainer == null)
        {
            Debug.Log("infoModalContainer not found.");
        }
        if (layersModalContainer == null)
        {
            Debug.Log("layersModalContainer not found.");
        }
        if (drawModalContainer == null)
        {
            Debug.Log("drawModalContainer not found.");
        }
        if (colorModalContainer == null)
        {
            Debug.Log("colorModalContainer not found.");
        }

        if (viewBtn == null)
        {
            Debug.Log("viewBtn not found.");
        }
        if (functionBtn == null)
        {
            Debug.Log("functionBtn not found.");
        }
        if (layersBtn == null)
        {
            Debug.Log("layersBtn not found.");
        }
        if (trailRenderer == null)
        {
            Debug.Log("trailRenderer not found.");
        }

        /* Default Color: White */
        ColorUtility.TryParseHtmlString("#FFFFFF", out materialColor);

        colorModalContainer.SetActive(false);

        mainCamera = Camera.main;
        uiCamera = GameObject.FindWithTag("UI Camera").GetComponent<Camera>();

        planeObj = new Plane(mainCamera.transform.forward * -1, this.transform.position);
    }

    void Update()
    {
        if (isDrawActive)
        {
            if (
                Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began
                || Input.GetMouseButtonDown(0)
            )
            {
                // Check if the touch is within the specified UI element
                Vector2 inputPosition = Input.mousePosition;
                // bool isTouchOverUI = IsTouchOverUIElement(inputPosition);
                if (IsTouchOverUIObject())
                {
                    return;
                }

                // Set the UI color to white if its the first time
                Material newMaterial = new Material(drawMaterial);
                if (materialColor != null)
                {
                    newMaterial.color = materialColor;
                }
                else
                {
                    ColorUtility.TryParseHtmlString("#FFFFFF", out materialColor);
                    newMaterial.color = materialColor;
                }

                theTrail = (GameObject)Instantiate(
                    drawPrefab,
                    this.transform.position,
                    Quaternion.identity
                );
                theTrail.GetComponent<TrailRenderer>().material = newMaterial;
                theTrail.name = drawObjectTag;
                theTrail.tag = drawObjectTag;

                Ray mouseRay = mainCamera.ScreenPointToRay(inputPosition);
                float dist;
                if (planeObj.Raycast(mouseRay, out dist))
                {
                    startPos = mouseRay.GetPoint(dist);
                }
            }
            else if (
                Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved
                || Input.GetMouseButton(0)
            )
            {
                // Check if the touch is within the specified UI element
                Vector2 inputPosition = Input.mousePosition;
                // bool isTouchOverUI = IsTouchOverUIElement(inputPosition);
                if (IsTouchOverUIObject())
                {
                    return;
                }

                Ray mouseRay = mainCamera.ScreenPointToRay(inputPosition);
                float dist;
                if (planeObj.Raycast(mouseRay, out dist))
                {
                    theTrail.transform.position = mouseRay.GetPoint(dist);
                }
            }
        }
    }

    private bool IsTouchOverUIElement(Vector2 touchPosition)
    {
        GameObject uiElement = GameObject.Find("Draw Modal");
        if (uiElement == null)
        {
            Debug.Log("uiElement not found");
            return false;
        }

        // Convert touch position from screen space to local space within the UI element's RectTransform
        RectTransform rectTransform = uiElement.GetComponent<RectTransform>();
        Vector2 localPoint;
        bool isInside = RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform,
            touchPosition,
            null,
            out localPoint
        );

        // Check if local position is inside the element's bounds
        if (isInside && rectTransform.rect.Contains(localPoint))
        {
            Debug.Log("Touch is inside the element");
            return true; // Touch is inside the element
        }
        else
        {
            Debug.Log("Touch is not inside the element");
            return false; // Touch is not inside the element
        }
    }

    private bool IsTouchOverUIObject()
    {
        // Check if the touch/click is over the UI object
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(
            Input.mousePosition.x,
            Input.mousePosition.y
        );
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.name == "Draw Modal" || result.gameObject.name == "Color Modal")
            {
                return true;
            }
        }
        return false;
    }

    public void handleDrawActivation()
    {
        radialMenuContainer.SetActive(false);
        viewModalContainer.SetActive(false);
        functionsModalContainer.SetActive(false);
        infoModalContainer.SetActive(false);
        layersModalContainer.SetActive(false);

        drawModalContainer.SetActive(true);

        dataStore.setIsDrawing(true);

        isDrawActive = true;
    }

    public void handleDrawDeactivation()
    {
        radialMenuContainer.SetActive(true);
        viewModalContainer.SetActive(true);
        functionsModalContainer.SetActive(true);
        infoModalContainer.SetActive(true);
        layersModalContainer.SetActive(true);

        functionBtn.interactable = true;
        viewBtn.interactable = true;
        layersBtn.interactable = true;

        // dont deactivate drawModalContainer since its done in the animation
        colorModalContainer.SetActive(false);

        isDrawActive = false;

        /* Clears everything drawn */
        deleteAllDrawObjects();

        dataStore.setIsDrawing(false);
    }

    public void eraseEverythingDrawn()
    {
        Debug.Log("eraseEverythingDrawn() called");
        deleteAllDrawObjects();
    }

    private void deleteAllDrawObjects()
    {
        GameObject[] drawObjects = GameObject.FindGameObjectsWithTag(drawObjectTag);
        foreach (GameObject obj in drawObjects)
        {
            Destroy(obj);
        }
    }

    public void activateColorModal()
    {
        colorModalContainer.SetActive(!colorModalContainer.activeSelf);
    }

    public void changeColor(string color)
    {
        switch (color)
        {
            case "red":
                ColorUtility.TryParseHtmlString("#f04369", out materialColor);
                break;
            case "blue":
                ColorUtility.TryParseHtmlString("#47a3ff", out materialColor);
                break;
            case "green":
                ColorUtility.TryParseHtmlString("#1de051", out materialColor);
                break;
            case "pink":
                ColorUtility.TryParseHtmlString("#cf57ff", out materialColor);
                break;
            default:
                ColorUtility.TryParseHtmlString("#ffffff", out materialColor);
                break;
        }
    }
}
