using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VHierarchy.Libs;
using VRuler.Libs;
using VTabs.Libs;

public class BoatController : MonoBehaviour
{
    [Header("Boat Power Status")] 
    public bool isBoatPowered;

    [Header("Movement Variables")] 
    [Range(-2,2)] public int leftWheelSpeed;
    [Range(-2,2)] public int rightWheelSpeed;
    [Range(0,0.1f)] [Tooltip("Speed modifier, the lower the number the slower the boat")]public float speedModifier;
    [Range(0,0.1f)] [Tooltip("Rotation modifier, the lower the number the slower the boat")]public float rotationModifier;
    private float _boatSpeed;
    private float _desiredBoatSpeed;
    private float _rotationSpeed;
    private float _desiredRotationSpeed;

    [Header("Visuals")] 
    public GameObject leftWheelModel;
    public GameObject rightWheelModel;
    
    [SerializeField] private ShipUiControls leftWheelUI;
    [SerializeField] private ShipUiControls rightWheelUI;
    [SerializeField] private ShipUiControls goButtonUI;
    [SerializeField] private ShipUiControls stopButtonUI;

    private void Start()
    {
        leftWheelUI.OnWheelChange += ChangeLeftInput;
        rightWheelUI.OnWheelChange += ChangeRightInput;
        goButtonUI.OnGoPressed += PowerBoatOn;
        stopButtonUI.OnStopPressed += PowerBoatOff;
    }

    private void Update()
    {
        if (isBoatPowered)
        {
            CalculateSpeed();
            CalculateDirection();
            RotateWheels();
        }
        else
        {
            _boatSpeed = Mathf.Lerp(_boatSpeed, 0, Time.deltaTime * 0.5f);
            _rotationSpeed = Mathf.Lerp(_rotationSpeed, 0, Time.deltaTime * 0.5f);
            CalculateDirection();
        }
    }
    
    //Rotates the wheels visually
    private void RotateWheels()
    {
        var lwRotationSpeed = 0.1f * leftWheelSpeed ;
        var rwRotationSpeed = 0.1f * rightWheelSpeed ;
        leftWheelModel.transform.Rotate(Vector3.down, lwRotationSpeed,Space.Self);
        rightWheelModel.transform.Rotate(Vector3.down, rwRotationSpeed, Space.Self);
    }

    private void PowerBoatOn(object sender, EventArgs e)
    {
        isBoatPowered = true;
        Debug.Log(isBoatPowered+ " Powered On");
    }

    private void PowerBoatOff(object sender, EventArgs e)
    {
        isBoatPowered = false;
        Debug.Log(isBoatPowered + " Powered Off");
    }
    
    private void ChangeLeftInput(object sender, ShipUiControls.OnWheelChangeEventArgs e)
    {
        Debug.Log(e.sliderValue);
        leftWheelSpeed = e.sliderValue;
    }
    
    private void ChangeRightInput(object sender, ShipUiControls.OnWheelChangeEventArgs e)
    {
        Debug.Log(e.sliderValue);
        rightWheelSpeed = e.sliderValue;
    }
    private void CalculateSpeed() //Takes the input of both wheel and creates a Vector2 where X is the leftWheel and Y is the rightWheel
    {
        _desiredBoatSpeed = (rightWheelSpeed + leftWheelSpeed) * speedModifier / 60f;
        //Calculate the direction of the spin
        switch (rightWheelSpeed)
        {
            case -2:
                switch (leftWheelSpeed)
                {
                    case -2:
                        _desiredRotationSpeed = 0f * rotationModifier * 2f;
                        break;
                    case -1 :
                        _desiredRotationSpeed = 5f * rotationModifier * 2f;
                        break;
                    case 0:
                        _desiredRotationSpeed = 10f * rotationModifier * 2f;
                        break;
                    case 1:
                        _desiredRotationSpeed = 15f * rotationModifier * 2f;
                        break;
                    case 2:
                        _desiredRotationSpeed = 20f * rotationModifier * 2f;
                        break;
                    default:
                        break;
                }
                break;
            case -1 :
                switch (leftWheelSpeed)
                {
                    case -2:
                        _desiredRotationSpeed = -5f * rotationModifier * 2f;
                        break;
                    case -1 :
                        _desiredRotationSpeed = 0f * rotationModifier * 2f;
                        break;
                    case 0:
                        _desiredRotationSpeed = 5f * rotationModifier * 2f;
                        break;
                    case 1:
                        _desiredRotationSpeed = 10f * rotationModifier * 2f;
                        break;
                    case 2:
                        _desiredRotationSpeed = 15f * rotationModifier * 2f;
                        break;
                    default:
                        break;
                }
                break;
            case 0:
                switch (leftWheelSpeed)
                {
                    case -2:
                        _desiredRotationSpeed = -10f * rotationModifier * 2f;
                        break;
                    case -1 :
                        _desiredRotationSpeed = -5f * rotationModifier * 2f;
                        break;
                    case 0:
                        _desiredRotationSpeed = 0f * rotationModifier * 2f;
                        break;
                    case 1:
                        _desiredRotationSpeed = 5f * rotationModifier * 2f;
                        break;
                    case 2:
                        _desiredRotationSpeed = 10f * rotationModifier * 2f;
                        break;
                    default:
                        break;
                }
                break;
            case 1:
                switch (leftWheelSpeed)
                {
                    case -2:
                        _desiredRotationSpeed = -15f * rotationModifier * 2f;
                        break;
                    case -1 :
                        _desiredRotationSpeed = -10f * rotationModifier * 2f;
                        break;
                    case 0:
                        _desiredRotationSpeed = -5f * rotationModifier * 2f;
                        break;
                    case 1:
                        _desiredRotationSpeed = 0f * rotationModifier * 2f;
                        break;
                    case 2:
                        _desiredRotationSpeed = 5f * rotationModifier * 2f;
                        break;
                    default:
                        break;
                }
                break;
            case 2:
                switch (leftWheelSpeed)
                {
                    case -2:
                        _desiredRotationSpeed = -20f * rotationModifier * 2f;
                        break;
                    case -1 :
                        _desiredRotationSpeed = -15f * rotationModifier * 2f;
                        break;
                    case 0:
                        _desiredRotationSpeed = -10f * rotationModifier * 2f;
                        break;
                    case 1:
                        _desiredRotationSpeed = -5f * rotationModifier * 2f;
                        break;
                    case 2:
                        _desiredRotationSpeed = 0f * rotationModifier * 2f;
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }

        _boatSpeed = Mathf.Lerp(_boatSpeed, _desiredBoatSpeed, Time.deltaTime * 0.5f);
        _rotationSpeed = Mathf.Lerp(_rotationSpeed, _desiredRotationSpeed, Time.deltaTime * 0.5f);
    }

    private void CalculateDirection() // Takes the overallSpeed and add the right force (foward/backward & rotation)
    {
        transform.Translate(transform.forward * _boatSpeed, 0);

        transform.Rotate(Vector3.up, _rotationSpeed, Space.World);
        
    }
}
