using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartController : MonoBehaviour
{
    [Serializable]
    private struct Part
    {
        public string name;
        public Sprite down; // down key sprite image
        public Sprite left; // left key animation
    }

    private List<Part> parts;
    int index = 0;
    Part curPart;
    int partLimit = 2;
    SpriteRenderer sr;

    private void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        curPart = parts[index];
    }

    public void SetBodyPart(string n, Sprite d, Sprite l)
    {
        Part newPart;
        newPart.name = n;
        newPart.down = d;
        newPart.left = l;
    }    

    private void LeftFacing()
    {
        sr.sprite = curPart.left;
    }

    private void RightFacing()
    {
        sr.sprite = curPart.left;
        sr.flipX = true;
    }

    private void RightReleased()
    {
        sr.flipX = false;
    }

    private void BackFacing()
    {

    }

    public void UpdateSpriteColor(Color32 newColor)
    {
        sr.color = newColor;
    }

}
