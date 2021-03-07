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
    }

    void MatchSprites(string tagName)
    {
        GameObject[] sameItem = GameObject.FindGameObjectsWithTag(tagName);
        int index = sameItem[0].GetComponent<BodyPartScript>().GetSpriteIndex();
        sameItem[1].GetComponent<BodyPartScript>().UpdateSprite(index);
    }

    void BodyButtonClicked()
    {
        Debug.Log(playerScript.bodyParts[0].gameObject.name);
    }

    void ArrowClicked(int num)
    {
        partIndex += num;
        if (partIndex > bodyPartNames.Length - 1) {
            partIndex = 0; 
        } else if (partIndex < 0){
            partIndex = bodyPartNames.Length - 1;
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

}
