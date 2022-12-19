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
    public GameObject infoModel;

    private Button viewBtn;
    private Button functionBtn;

    void Awake()
    {

        accessibilityBtn = GameObject.Find("Accessibility Btn");
        radialMenu = GameObject.Find("Radial Menu");
        viewModal = GameObject.Find("View Modal");
        functionsModal = GameObject.Find("Functions Modal");
        // infoModel = GameObject.Find("Info Model");

        viewBtn = GameObject.Find("Radial Menu/Background/Elements/View/Button").GetComponent<Button>();
        functionBtn = GameObject.Find("Radial Menu/Background/Elements/Functions/Button").GetComponent<Button>();
    }
    void Start()
    {
        radialMenu.SetActive(false);
        viewModal.SetActive(false);
        functionsModal.SetActive(false);
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
        functionBtn.interactable = false;
    }

    public void handleFunctionsMenuClose()
    {
        functionsModal.SetActive(false); 
        functionBtn.interactable = true;
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
        viewBtn.interactable = false;
    }

    public void handleViewMenuClose()
    {
        viewModal.SetActive(false);
        viewBtn.interactable = true;
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
    * Info model
    */
    public void handleInfoModelOpen()
    {
        infoModel.SetActive(true);
    }

    public void handleInfoModelClose()
    {
        infoModel.SetActive(false);
    }
}
