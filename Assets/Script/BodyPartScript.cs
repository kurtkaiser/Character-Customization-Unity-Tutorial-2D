using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartScript : MonoBehaviour
{
    // be able to have universal applications to body parts
    // access to a variety directions of similar sprites
    // access to different kinds of action/types/designs

    [Serializable]
    public struct Sprites
    {
        public string name;
        public Sprite down; // down key sprite image
        public Sprite left; // left key animation
        public Sprite up; // up key sprite image
    }

    public Sprites[] sprites;
    int index = 0;

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[index].down;
    }

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

    public void ChangeSpriteDirection(string dir)
    {
        if(dir == "WalkDownKey")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[index].down;
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (dir == "WalkLeftKey")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[index].left;
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (dir == "WalkRightKey")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[index].left;
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (dir == "WalkUpKey")
        {
            if(sprites[index].up != null)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[index].up;
            } else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[index].down;
            }            
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
