using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleLayersSelectModal : MonoBehaviour
{
    private GameObject accessibilityBtn;
    private GameObject radialMenu;
    private GameObject viewModal;
    private GameObject functionsModal;
    public GameObject infoModal;
    public GameObject drawModal;
    public GameObject colorModal;
    public GameObject layersModal;
    public GameObject layersSelectModal;

    void Start()
    {
        accessibilityBtn = GameObject.Find("Accessibility Btn");
        radialMenu = GameObject.Find("Radial Menu");
        viewModal = GameObject.Find("View Modal");
        functionsModal = GameObject.Find("Functions Modal");
        // infoModal = GameObject.Find("Info Modal");
        drawModal = GameObject.Find("Draw Modal");
        colorModal = GameObject.Find("Color Modal");
        layersModal = GameObject.Find("Layers Modal");
        layersSelectModal = GameObject.Find("Layers Select Modal");

        // if(accessibilityBtn == null) Debug.Log("accessibilityBtn not found");
        // if(radialMenu == null) Debug.Log("radialMenu not found");
        // if(viewModal == null) Debug.Log("viewModal not found");
        // if(functionsModal == null) Debug.Log("functionsModal not found");
        // if(infoModal == null) Debug.Log("infoModal not found");
        // if(drawModal == null) Debug.Log("drawModal not found");
        // if(colorModal == null) Debug.Log("colorModal not found");
        // if(layersModal == null) Debug.Log("layersModal not found");
        // if(layersSelectModal == null) Debug.Log("layersSelectModal not found");
    }

    public void handleModalOpen()
    {
        Debug.Log("handleModalOpen called");

        accessibilityBtn.SetActive(false);
        radialMenu.SetActive(false);
        viewModal.SetActive(false);
        functionsModal.SetActive(false);
        // infoModal.SetActive(false);
        layersModal.SetActive(false);
        drawModal.SetActive(false);
        colorModal.SetActive(false);
    }

    public void handleModalClose()
    {
        radialMenu.SetActive(true);

        layersSelectModal.SetActive(false);
    }
}
