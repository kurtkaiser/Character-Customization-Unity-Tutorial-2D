using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorControllerScript : MonoBehaviour
{
    public ChSceneControllerScript charSceneScript;

    [Serializable]
    public struct ColorControl
    {
        public Slider slider;
        public Image fill;
        public Image handle;
        public float rgb;
    }


    public ColorControl red;
    public ColorControl green;
    public ColorControl blue;

    public Button[] colorButtons;

    public Image choiceColorDisplay;

    private String[] HexHairColors =
       { "#fbe7a1", "#d9b380", "#CC9966", "#75250a", "#652A0E",
         "#634933", "#5A3825",  "#3D2314", "#500a09", "#201123",
        "#23120B", "#010203", "#8d827a", "#505050", "#F3EBE1"
    };

    private String[] HexSkinColors =
        { "#fbe7a1", "#f1c27d", "#CAA667", "#d9b380", "#CC9966",
        "#AF6E51", "#652A0E", "#c68642", "#634933",  "#5A3825",
        "#8d5524", "#3D2314", "#23120B" , "#010203", "#656d43"
    };

    private String[] HexCrayonColors =
       { "#fbe7a1", "#f1c27d", "#CAA667", "#d9b380", "#CC9966",
        "#AF6E51", "#652A0E", "#c68642", "#634933",  "#5A3825",
        "#8d5524", "#3D2314", "#23120B" , "#010203", "#656d43"
    };



    private void Start()
    {
        red.slider.value = (float)UnityEngine.Random.Range(0, 255);
        green.slider.value = (float)UnityEngine.Random.Range(0, 255);
        blue.slider.value = (float)UnityEngine.Random.Range(0, 255);
        RedChange();
        GreenChange();
        BlueChange();

        red.slider.onValueChanged.AddListener(delegate { RedChange(); });
        green.slider.onValueChanged.AddListener(delegate { GreenChange(); });
        blue.slider.onValueChanged.AddListener(delegate { BlueChange(); });

        ChangeButtonColors(HexHairColors);
    }

    void RedChange()
    {
        red.rgb = red.slider.value;
        red.fill.color = new Color32((byte)(red.rgb), 0, 0, (byte)255);
        UpdateHandleAndColorDisplay();
    }

    void GreenChange()
    {
        green.rgb = green.slider.value;
        green.fill.color = new Color32(0, (byte)(green.rgb), 0, (byte)255);
        UpdateHandleAndColorDisplay();
    }

    void BlueChange()
    {
        blue.rgb = blue.slider.value;
        blue.fill.color = new Color32(0, 0, (byte)(blue.rgb), (byte)255);
        UpdateHandleAndColorDisplay();
    }

    private void UpdateHandleAndColorDisplay()
    {
        Color32 newColor =new Color32(
            (byte)(red.rgb),
            (byte)(green.rgb),
            (byte)(blue.rgb),
            (byte)255
        );

        red.handle.color = newColor;
        green.handle.color = newColor;
        blue.handle.color = newColor;
        UpdateColorChoiceSelection(newColor);
    }

    public void UpdateColorChoiceSelection(Color newColor)
    {
        choiceColorDisplay.color = newColor;
        charSceneScript.ChangeCurrentPartColor(newColor);
    }

    private void ChangeButtonColors(String[] colors)
    {
        int i = 0;
        foreach(Button btn in colorButtons)
        {
            ColorUtility.TryParseHtmlString(colors[i++], out Color color);
            btn.image.color = color;
        }
    }
}
