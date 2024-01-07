using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeverController : MonoBehaviour
{
    public Sprite[] leverSprites;
    public Image leverSprite;
    public Slider slider;


    public void ChangeSprite()
    {
        var leverValue = slider.value;

        switch (leverValue)
        {
            case -2:
                leverSprite.sprite = leverSprites[4];
                break;
            case -1:
                leverSprite.sprite = leverSprites[3];
                break;
            case 0:
                leverSprite.sprite = leverSprites[2];
                break;
            case 1:
                leverSprite.sprite = leverSprites[1];
                break;
            case 2:
                leverSprite.sprite = leverSprites[0];
                break;
            default:
                break;
        }
    }
}