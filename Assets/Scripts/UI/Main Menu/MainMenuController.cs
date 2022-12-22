using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

public class MainMenuController : MonoBehaviour
{
    // public static MainMenuController instance;

    private static string clickedCard;

    public GameObject MainMenu;
    public GameObject LoadingScreen;
    // public ProgressBar bar;

    void Awake()
    {
        // instance = this;
    }

    public static string returnClickedCard()
    {
        return clickedCard;
    }

    public void test()
    {
        Debug.Log("wewewew");
    }

    public void handleCardClick(string name)
    {
        Debug.Log("card click");
        clickedCard = name;
        LoadingScreen.gameObject.SetActive(true);
        MainMenu.gameObject.SetActive(false);
        
        // StartCoroutine(GetSceneLoadProgress());
        // SceneManager.LoadScene((int)SceneIndexes.TEMP);

        if(name == "heart"){
            SceneManager.LoadScene((int)SceneIndexes.MODEL_SCENE);
        }else if(name == "skull"){
            SceneManager.LoadScene((int)SceneIndexes.CROSS_SECTION_SCENE);
        } else if(name == "head"){
            SceneManager.LoadScene((int)SceneIndexes.HEAD_SCENE);
        }
 
        LoadingScreen.gameObject.SetActive(false);
    }

    // float totalSceneProgress;
    public IEnumerator GetSceneLoadProgress()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync((int)SceneIndexes.TEMP);

        while (!asyncLoad.isDone)
        {
            // totalSceneProgress = 0;
            yield return null;
        }
    }

    public void ExitApplication()
    {
        Application.Quit();
        Debug.Log("Application Closed");
    }
}


