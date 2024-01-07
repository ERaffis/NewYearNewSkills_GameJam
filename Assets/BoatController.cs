using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    [Header("Boat Power Status")] 
    public bool isBoatPowered;

    [Header("Movement Variables")] 
    [Range(-2,2)] public int leftWheel;
    [Range(-2,2)] public int rightWheel;
    [Range(0,10)] [Tooltip("Speed modifier, the lower the number the slower the boat")]public float speedModifier;
    [Range(0,1)] [Tooltip("Rotation modifier, the lower the number the slower the boat")]public float rotationModifier;
    private float _boatSpeed;
    private float _desiredBoatSpeed;
    private float _rotationSpeed;
    private float _desiredRotationSpeed;

   
    private void Update()
    {
        if (isBoatPowered)
        {
            CalculateSpeed();
            CalculateDirection();
        }
        else
        {
            _boatSpeed = Mathf.Lerp(_boatSpeed, 0, Time.deltaTime * 0.5f);
            _rotationSpeed = Mathf.Lerp(_rotationSpeed, 0, Time.deltaTime * 0.5f);
            CalculateDirection();
        }
    }

    private void CalculateSpeed() //Takes the input of both wheel and creates a Vector2 where X is the leftWheel and Y is the rightWheel
    {
        _desiredBoatSpeed = (rightWheel + leftWheel) * speedModifier / 60f;
        //Calculate the direction of the spin
        switch (rightWheel)
        {
            case -2:
                switch (leftWheel)
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
                switch (leftWheel)
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
                switch (leftWheel)
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
                switch (leftWheel)
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
                switch (leftWheel)
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

        transform.Rotate(Vector3.up, _rotationSpeed);
        
    }
}
