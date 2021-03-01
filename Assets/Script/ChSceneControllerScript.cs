using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChSceneControllerScript : MonoBehaviour
{

    public Button randomBtn;
    public GameObject bodyPartBtn;
    public Button leftArrowBtn;
    public Button rightArrowBtn;
    public PlayerScript playerScript;

    void Start()
    {
        randomBtn.onClick.AddListener(() => RandomPlayerSprites());
    }

    
    void RandomPlayerSprites()
    {
        int len = playerScript.bodyParts.Length - 1;
        int rndSpriteIndex;
        for (int i = 0; i < len; i++)
        {
            rndSpriteIndex = Random.Range(0, playerScript.bodyParts[i].GetSpritesLength());
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

    void bodyButtonClicked()
    {

    }

    void ArrowClicked()
    {

    }
}
