using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerContorller : MonoBehaviour
{

    public List<GameObject> layers;

    public GameObject clippingRoot;

    private LayersStore layersStore;

    // Start is called before the first frame update
    void Start()
    {
        layersStore = LayersStore.Instance;
        layersStore.initializeLayers(layers,clippingRoot);
    }

    public void addLayer(){
        layersStore.addLayer();
        Debug.Log("add");
    }

    public void removeLayer(){
        layersStore.removeLayer();
        Debug.Log("hide");
    }

}
