using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RadialButtonController : MonoBehaviour
{
    /*
    * Back to Main Menu
    */
    public void handleBackToMainMenu(){
        SceneManager.LoadScene((int)SceneIndexes.MAIN_MENU);
    }
}
