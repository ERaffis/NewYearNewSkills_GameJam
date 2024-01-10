using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionPort : MonoBehaviour
{
    public bool isConnected;
    public bool isPowered;
    public GameObject cableExtension;
    public List<PowerCable> connectedCables = new List<PowerCable>();

    public MeshRenderer redLight;
    public MeshRenderer greenLight;

    public Material lightOff;
    public Material lightOn;
    
    
    private void Start()
    {
        
        if (isPowered)
        {
            greenLight.material = lightOn;
            redLight.material = lightOff;
        }
        else
        {
            greenLight.material = lightOff;
            redLight.material = lightOn;
        }
    }

    private void LateUpdate()
    {
        CheckForPower();
    }

    public void Connect(PowerCable cable)
    {
        isConnected = true;
        if(cableExtension != null) cableExtension.SetActive(true);
        connectedCables.Add(cable);
    }

    public void CheckForPower(PowerCable pwCable)
    {
        if (!pwCable.isPowered) return;
        
        if (this.isPowered != false) return;
        this.isPowered = true;
        greenLight.material = lightOn;
        redLight.material = lightOff;
    }
    
    private void CheckForPower()
    {
        if (isPowered) return;
    
        foreach (var cable in connectedCables)
        {
            if (cable.isPowered)
            {
                isPowered = true;
                greenLight.material = lightOn;
                redLight.material = lightOff;
                break;
            }
        }
    }
   
    
}
