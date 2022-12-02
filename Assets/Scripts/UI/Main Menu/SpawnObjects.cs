using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnObjects : MonoBehaviour
{
    public GameObject[] prefabs;

    // Start is called before the first frame update
    void Start()
    {
        string name = MainMenuController.returnClickedCard();
        spawnPrefab(name);
    }

    void spawnPrefab(string name)
    {
        
        switch (name) 
        {
            case "skull":
                Debug.Log("***SKULL***");
                Instantiate(prefabs[0], new Vector3(0, 0, 0), Quaternion.identity);
                break;
            case "heart":
                Debug.Log("***HEART***");
                Instantiate(prefabs[1], new Vector3(0, 0, 0), Quaternion.identity);
                break;
            default:
                Debug.Log("No prefabs found.");
                break;
        }
        
        // Instantiate(obj, new Vector3(0, 0, 0), Quaternion.identity);

    }
}
