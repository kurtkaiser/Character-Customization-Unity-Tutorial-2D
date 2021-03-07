﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListScriptTest : MonoBehaviour
{

    [Serializable]
    public struct Sprites
    {
        public string name;
        public Sprite down; // down key sprite image
        public Sprite left; // left key animation
    }

    public List<Sprites> sprites;
    int index = 0;


    public void UpdateSprite(int newIndex)
    {
        index = newIndex;
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[index].down;
    }

    public int GetSpritesLength()
    {
        return sprites.Length;
    }

    public int GetSpriteIndex()
    {
        return index;
    }

    public void UpdateToNextSprite()
    {
        if (++index > sprites.Length - 1) index = 0;
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[index].down;
    }

    public void UpdateSpriteColor(Color32 newColor)
    {
        gameObject.GetComponent<SpriteRenderer>().color = newColor;
    }
}