using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipUiControls : MonoBehaviour
{
    [Header("UI Elements")]
    
    [Header("Boat Controls")]
    public Sprite goSpriteOn;
    public Sprite goSpriteOff;
    public Sprite stopSpriteOn;
    public Sprite stopSpriteOff;
    public Button goButton;
    public Button stopButton;
    
    [Header("Wheel Controls")]
    public Sprite[] leverSprites;
    public Image leverSprite;
    public Slider slider;

    [HideInInspector]
    public event EventHandler<OnWheelChangeEventArgs> OnWheelChange;
    public event EventHandler OnGoPressed;
    public event EventHandler OnStopPressed;
    public class OnWheelChangeEventArgs : EventArgs
    {
        public int sliderValue;
    }
    

    public void PressedPowerButton()
    {
        if (this.name == "Button_Go")
        {
            OnGoPressed?.Invoke(this, EventArgs.Empty);
            goButton.image.sprite = goSpriteOn;
            stopButton.image.sprite = stopSpriteOff;
        }
        else if (this.name == "Button_Stop")
        {
            OnStopPressed?.Invoke(this, EventArgs.Empty);
            goButton.image.sprite = goSpriteOff;
            stopButton.image.sprite = stopSpriteOn;
        }
    }
    public void ChangeWheelControls()
    {
        OnWheelChange?.Invoke(this, new OnWheelChangeEventArgs{sliderValue = (int)slider.value});
        ChangeLeverSprite();
    }
    private void ChangeLeverSprite()
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