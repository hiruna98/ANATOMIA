using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AccessibilityBtnController : MonoBehaviour
{
    private GameObject accessibilityBtn;
    private GameObject radialMenu;
    private GameObject viewModal;
    private GameObject functionsModal;
    private GameObject infoModal;
    private GameObject layersModal;
    private GameObject layersSelectModal;

    private Button viewBtn;
    private Button functionBtn;
    private Button layersBtn;

    private MultiSelectStore multiSelectStore;
    private MaterialController materialController;
    
    private GameObject layersPopupLockBtn;
    private GameObject layersPopupUnlockBtn;
    

    void Awake()
    {
        multiSelectStore = MultiSelectStore.Instance;
        materialController = MaterialController.Instance;
        
        accessibilityBtn = GameObject.Find("Accessibility Btn");
        radialMenu = GameObject.Find("Radial Menu");
        viewModal = GameObject.Find("View Modal");
        functionsModal = GameObject.Find("Functions Modal");
        infoModal = GameObject.Find("Info Modal");
        layersModal = GameObject.Find("Layers Modal");
        layersSelectModal = GameObject.Find("Layers Select Modal");

        viewBtn = GameObject.Find("Radial Menu/Background/Elements/View/Button").GetComponent<Button>();
        functionBtn = GameObject.Find("Radial Menu/Background/Elements/Functions/Button").GetComponent<Button>();
        layersBtn = GameObject.Find("Radial Menu/Background/Elements/Layers/Button").GetComponent<Button>();
        layersPopupLockBtn = layersModal.transform.Find("Top Bar/Lock Btn").gameObject;
        layersPopupUnlockBtn = layersModal.transform.Find("Top Bar/Unlock Btn").gameObject;

        if(!accessibilityBtn) Debug.Log("accessibilityBtn not found");
        if(!radialMenu) Debug.Log("radialMenu not found");
        if(!viewModal) Debug.Log("viewModal not found");
        if(!functionsModal) Debug.Log("functionsModal not found");
        if(!infoModal) Debug.Log("infoModal not found");
        if(!layersModal) Debug.Log("layersModal not found");
        if(!layersSelectModal) Debug.Log("layersSelectModal not found");

    }

    void Start()
    {
        radialMenu.SetActive(false);
        // viewModal.SetActive(false);
        // functionsModal.SetActive(false);
        // layersModal.SetActive(false);
        // layersSelectModal.SetActive(false);
    }


    /*
    * Radial Menu
    */

    public void handleRadialMenuOpen()
    {
        accessibilityBtn.SetActive(false);
        radialMenu.SetActive(true);
    }

    public void handleRadialMenuClose()
    {
        accessibilityBtn.SetActive(true);
        radialMenu.SetActive(false);
    }

    /*
    * Functions Menu
    */
    public void handleFunctionsMenuOpen()
    {
        functionsModal.SetActive(true);
        // functionBtn.interactable = false;
    }

    public void handleFunctionsMenuClose()
    {
        functionsModal.SetActive(false); 
        // functionBtn.interactable = true;
    }

    public void handleFunctionsMenuLock()
    {
        Debug.Log("Functions menu lock");   
    }

    /*
    * View Menu
    */
    public void handleViewMenuOpen()
    {
        viewModal.SetActive(true);
        // viewBtn.interactable = false;
    }

    public void handleViewMenuClose()
    {
        viewModal.SetActive(false);
        // viewBtn.interactable = true;
    }

    public void handleViewMenuLock()
    {
        Debug.Log("View menu lock");
    }

    /*
    * Draw Scene
    */
    public void handleDrawVisibility()
    {
        Debug.Log("Draw btn clicked");
    }

    /*
    * Back to Main Menu
    */
    public void handleBackToMainMenu(){
        SceneManager.LoadScene((int)SceneIndexes.MAIN_MENU);
    }

    /*
    * Info modal
    */
    public void handleInfoModelOpen()
    {
        infoModal.SetActive(true);
    }

    public void handleInfoModelClose()
    {
        materialController.removeMaterialOfAllObjects();
        infoModal.SetActive(false);
        multiSelectStore.removeAllObject();
    }

    /*
    * Layers model
    */
    public void handleLayersModalOpen()
    {
        layersModal.SetActive(true);
        // layersBtn.interactable = false;
    }

    public void handleLayersModalClose()
    {
        layersModal.SetActive(false);
        // layersBtn.interactable = true;
    }

    /*
    * Layers Select model
    */
    public void handleLayersSelectModelOpen()
    {
        // accessibilityBtn.SetActive(false);
        // radialMenu.SetActive(false);
        // viewModal.SetActive(false);
        // functionsModal.SetActive(false);
        // layersModal.SetActive(false);
    }

    public void handleLayersSelectModelClose()
    {
        radialMenu.SetActive(true);
        layersSelectModal.SetActive(false);
        // layersBtn.interactable = true;
        // viewBtn.interactable = true;
        // functionBtn.interactable = true;
    }

    public void onLayersPopupLockRotation(){
        PlayerPrefs.SetInt("Layers Model Container RotationLock",1);
        layersPopupLockBtn.SetActive(false);
        layersPopupUnlockBtn.SetActive(true);
    }

    public void onLayersPopupUnlockRotation(){
        PlayerPrefs.SetInt("Layers Model Container RotationLock",0);
        layersPopupLockBtn.SetActive(true);
        layersPopupUnlockBtn.SetActive(false);
    }

}
