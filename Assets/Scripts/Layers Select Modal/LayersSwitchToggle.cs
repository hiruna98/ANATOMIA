using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayersSwitchToggle : MonoBehaviour
{
    [SerializeField] RectTransform uiHandleRectTransform;
    [SerializeField] Color backgroundActiveColor;
    [SerializeField] Color handleActiveColor;

    [SerializeField] List<GameObject> layers;
    [SerializeField] GameObject clippingRoot;

    private LayersStore layersStore;

    private Toggle toggleButton;

    Image backgroundImage, handleImage;

    Color backgroundDefaultColor, handleDefaultColor;

    Toggle toggle;

    Vector2 handlePosition;

    void Awake ( ) {
      toggle = GetComponent <Toggle>( );

      handlePosition = uiHandleRectTransform.anchoredPosition;

      backgroundImage = uiHandleRectTransform.parent.GetComponent <Image>( );
      handleImage = uiHandleRectTransform.GetComponent <Image>( );

      backgroundDefaultColor = backgroundImage.color;
      handleDefaultColor = handleImage.color;
      

      toggle.onValueChanged.AddListener (OnSwitch);

      if (toggle.isOn)
         OnSwitch (true);
    }

    void Start() {
      layersStore = LayersStore.Instance;
      // layersStore.initializeLayers(layers,clippingRoot);
      
      toggleButton = GetComponent<Toggle>();
      toggleButton.isOn = true;
    }

    void OnSwitch (bool on) {
      uiHandleRectTransform.anchoredPosition = on ? handlePosition * -1 : handlePosition; // no anim
      // uiHandleRectTransform.DOAnchorPos (on ? handlePosition * -1 : handlePosition, .4f).SetEase (Ease.InOutBack);

      backgroundImage.color = on ? backgroundActiveColor : backgroundDefaultColor; // no anim
      // backgroundImage.DOColor (on ? backgroundActiveColor : backgroundDefaultColor, .6f);

      handleImage.color = on ? handleActiveColor : handleDefaultColor; // no anim
      // handleImage.DOColor (on ? handleActiveColor : handleDefaultColor, .4f);

      GameObject myGameObject = gameObject;
      Transform parentTransform = myGameObject.transform.parent;
      Debug.Log("The name of the parent is: " + parentTransform.name + on);
      layersStore.setEnable(on, parentTransform.name);

   }

   void OnDestroy( ) {
      toggle.onValueChanged.RemoveListener (OnSwitch);
   }
}
