using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEventHandler : MonoBehaviour
{
    //root object
    private GameObject obj; 

    private MultiSelectStore multiSelectStore;
    private IsolationController isolationController;
    private ViewController viewController;

    private GameObject viewPopupLockBtn;
    private GameObject viewPopupUnlockBtn;
    private GameObject functionPopupLockBtn;
    private GameObject functionPopupUnlockBtn;

    private void Start()
    {
        obj = GameObject.Find("root_object");
        multiSelectStore = MultiSelectStore.Instance;
        viewController = ViewController.Instance;
        isolationController = new IsolationController();
        viewPopupLockBtn   = GameObject.Find("View Modal/Top Bar/Lock Btn");
        viewPopupUnlockBtn = GameObject.Find("View Modal/Top Bar/Unlock Btn");
        functionPopupLockBtn   = GameObject.Find("Functions Modal/Top Bar/Lock Btn");
        functionPopupUnlockBtn = GameObject.Find("Functions Modal/Top Bar/Unlock Btn");
    }
    public void onIsolateBtnClick()
    {
        isolationController.IsolateObjects(multiSelectStore.getSelectedObjects());
    }

    public void onUndoBtnClick()
    {
        isolationController.undo();
    }

    public void onRedoBtnClick()
    {
        isolationController.redo();
    }


    public void onCenterClicked(){
        viewController.centerPos(obj);
    }

    public void onAnteriorClicked(){
        viewController.antRotation(obj);
    }

    public void onPosteriorClicked(){
        viewController.posRotation(obj);
    }

    public void onLateralClicked(){
        viewController.latRotation(obj);
    }

    public void onMedialClicked(){
        viewController.medRotation(obj);
    }

    public void onSuperiorClicked(){
        viewController.supRotation(obj);
    }

    public void onInferiorClicked(){
        viewController.infRotation(obj);
    }

    public void doExitApplication() {
        Application.Quit();
        Debug.Log("Game is exiting");
    }

    public void onViewPopupLockRotation(){
        PlayerPrefs.SetInt("View Modal Container RotationLock",1);
        viewPopupLockBtn.SetActive(false);
        viewPopupUnlockBtn.SetActive(true);
    }

    public void onViewPopupUnlockRotation(){
        PlayerPrefs.SetInt("View Modal Container RotationLock",0);
        viewPopupLockBtn.SetActive(true);
        viewPopupUnlockBtn.SetActive(false);
    }

    public void onFunctionPopupLockRotation(){
        PlayerPrefs.SetInt("Functions Modal Container RotationLock",1);
        functionPopupLockBtn.SetActive(false);
        functionPopupUnlockBtn.SetActive(true);
    }

    public void onFunctionPopupUnlockRotation(){
        PlayerPrefs.SetInt("Functions Modal Container RotationLock",0);
        functionPopupLockBtn.SetActive(true);
        functionPopupUnlockBtn.SetActive(false);
    }

}
