using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarSortOrderScript : MonoBehaviour
{
    bool earsOnTop = false;

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.RightArrow)
            || Input.GetKey(KeyCode.LeftArrow))
            && earsOnTop == false) {
            gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Head";
            earsOnTop = true;
        } else if (earsOnTop == true)
        {
            gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Ears";
        }
    }
}
