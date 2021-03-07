using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChSceneControllerScript : MonoBehaviour
{

    // Scene UI
    public ColorControllerScript colorController;
    public Button randomBtn;
    public GameObject bodyPartBtn;
    public Button leftArrowBtn;
    public Button rightArrowBtn;
    public Toggle sleevesToggle;
    public Toggle hatToggle;

    // Script variables
    public PlayerScript playerScript;
    private string[] bodyPartNames;
    private int partIndex = 0;
    private GameObject[] skinBodyParts;



    void Start()
    {
        bodyPartNames = GetPlayerBodyPartNames();
        skinBodyParts = GameObject.FindGameObjectsWithTag("Skin");

        randomBtn.onClick.AddListener(() => RandomPlayerSprites());
        leftArrowBtn.onClick.AddListener(() => ArrowClicked(1));
        rightArrowBtn.onClick.AddListener(() => ArrowClicked(-1));
        bodyPartBtn.GetComponent<Button>().onClick.AddListener(() => ChangeBodyPartClicked());
        sleevesToggle.onValueChanged.AddListener(delegate {
            ToggleSleeves(sleevesToggle);
        });
        hatToggle.onValueChanged.AddListener(delegate {
            ToggleHat(hatToggle);
        });
        ArrowClicked(0);
    }

    void RandomPlayerSprites()
    {
        int len = playerScript.bodyParts.Length - 1;
        int rndSpriteIndex;
        for (int i = 0; i < len; i++)
        {
            rndSpriteIndex = UnityEngine.Random.Range(0, playerScript.bodyParts[i].GetSpritesLength());
            playerScript.bodyParts[i].UpdateSprite(rndSpriteIndex);
        }
        MatchSprites("Leg");
        MatchSprites("Foot");
        String strSprit = GameObject.Find("Shirt").GetComponent<SpriteRenderer>().sprite.name;
        bool sleevesOn = UnityEngine.Random.Range(0, 4) > 0;
        if (sleevesOn && strSprit == "Abs" || strSprit == "ChestFemale") sleevesOn = false;
        ShowSleeves(sleevesOn);
        sleevesToggle.isOn = sleevesOn;
    }

    void MatchSprites(string tagName)
    {
        GameObject[] sameItem = GameObject.FindGameObjectsWithTag(tagName);
        int index = sameItem[0].GetComponent<BodyPartScript>().GetSpriteIndex();
        sameItem[1].GetComponent<BodyPartScript>().UpdateSprite(index);
    }

    void ArrowClicked(int num)
    {
        partIndex += num;
        if (partIndex > bodyPartNames.Length - 1) {
            partIndex = 0; 
        } else if (partIndex < 0){
            partIndex = bodyPartNames.Length - 1;
        }
        if (!sleevesToggle.isOn) {
            if (bodyPartNames[partIndex] == "Left Sleeve")
            {
                partIndex += 2;
            }
            else if (bodyPartNames[partIndex] == "Right Sleeve")
            {
                partIndex -= 2;
            }

        }
        bodyPartBtn.transform.GetChild(0).gameObject.GetComponent<Text>().text = bodyPartNames[partIndex];
        if (playerScript.bodyParts[partIndex].CompareTag("Skin"))
        {
            colorController.ChangeButtonColors(colorController.GetHexSkinColors());
        } else if (playerScript.bodyParts[partIndex].CompareTag("Hair"))
        {
            colorController.ChangeButtonColors(colorController.GetHexHairColors());
        } else
        {
            colorController.ChangeButtonColors(colorController.GetHexDefaultColors());
        }
    }

    private string[] GetPlayerBodyPartNames()
    {
        int i = 0;
        string[] allNames = new string[playerScript.bodyParts.Length];
        foreach(BodyPartScript partScript in playerScript.bodyParts)
        {
            allNames[i] = partScript.gameObject.name;
            i++;
        }
        return allNames;
    }

    private void ChangeBodyPartClicked()
    {
        playerScript.bodyParts[partIndex].UpdateToNextSprite();
        string partTag = playerScript.gameObject.tag;
        if (partTag == "Leg" || partTag == "Foot")
        {
            MatchSprites("Leg");
            MatchSprites("Foot");
        } 
    }

    public void ChangeCurrentPartColor(Color32 newColor)
    {

        if (playerScript.bodyParts[partIndex].CompareTag("Skin"))
        {
            foreach (GameObject part in skinBodyParts)
            {
                part.GetComponent<BodyPartScript>().UpdateSpriteColor(newColor);
            }
        }
        else
        {
            playerScript.bodyParts[partIndex].UpdateSpriteColor(newColor);
        }
    }

    private void ToggleSleeves(Toggle change)
    {

        ShowSleeves(change.isOn);

    }

    private void ToggleHat(Toggle change)
    {
        GameObject.Find("Hat").GetComponent<SpriteRenderer>().enabled = change.isOn;
    }

    private void ShowSleeves(bool showSleeve)
    {
        GameObject[] sleeves = GameObject.FindGameObjectsWithTag("Sleeve");
        Color newColor = Color.white;
        if (showSleeve)
        {
            newColor = GameObject.Find("Shirt").GetComponent<SpriteRenderer>().color;

        }
        else
        {
            newColor = GameObject.Find("Head").GetComponent<SpriteRenderer>().color;
        }
        sleeves[0].GetComponent<SpriteRenderer>().color = newColor;
        sleeves[1].GetComponent<SpriteRenderer>().color = newColor;
    }

}
