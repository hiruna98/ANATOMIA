using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerContorller : MonoBehaviour
{

    public List<GameObject> layers;

    private LayersStore layersStore;

    // Start is called before the first frame update
    void Start()
    {
        layersStore = LayersStore.Instance;
        layersStore.initializeLayers(layers);
    }

    public void addLayer(){
        layersStore.addLayer();
    }

    public void removeLayer(){
        layersStore.removeLayer();
    }

}
