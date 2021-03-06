using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorButtonSript : MonoBehaviour
{
    GameObject colorController;

    void Start()
    {
        colorController = GameObject.Find("ColorPanelController");
        GetComponent<Button>().onClick.AddListener( () => ChangeColor() );
    }

    void ChangeColor()
    {
        ColorControllerScript cur = colorController.GetComponent<ColorControllerScript>();
        cur.UpdateColorChoiceSelection(gameObject.GetComponent<Image>().color);
    }
}
