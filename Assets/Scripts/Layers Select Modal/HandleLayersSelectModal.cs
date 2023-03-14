using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleLayersSelectModal : MonoBehaviour
{
    private GameObject radialMenuContainer;
    private GameObject viewModalContainer;
    private GameObject functionsModalContainer;
    private GameObject infoModalContainer ;
    private GameObject layersModalContainer;
    private GameObject drawModalContainer;
    private GameObject colorModalContainer;
    private GameObject layersSelectModalContainer;

    void Start()
    {
        radialMenuContainer = GameObject.Find("Radial Menu Container");
        viewModalContainer = GameObject.Find("View Modal Container");
        functionsModalContainer = GameObject.Find("Functions Modal Container");
        infoModalContainer = GameObject.Find("Info Modal Container");
        layersModalContainer = GameObject.Find("Layers Modal Container");
        drawModalContainer = GameObject.Find("Draw Modal Container");
        colorModalContainer = GameObject.Find("Draw Modal Container/Color Modal Container");
        layersSelectModalContainer = GameObject.Find("Layers Select Modal Container");
    }

    public void handleModalOpen()
    {
        radialMenuContainer.SetActive(false);
        viewModalContainer.SetActive(false);
        functionsModalContainer.SetActive(false);
        infoModalContainer.SetActive(false);
        layersModalContainer.SetActive(false);
        drawModalContainer.SetActive(false);
        colorModalContainer.SetActive(false);

        layersSelectModalContainer.SetActive(true);
    }

    public void handleModalClose()
    {
        radialMenuContainer.SetActive(true);

        layersSelectModalContainer.SetActive(false);
    }
}
