using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableManager : MonoBehaviour
{
    public Transform boatAttachPoint;
    public Transform cablePlacementPoint;
    public Vector3 lastPosition;

    public GameObject cablePrefab;
    public GameObject buoyPrefab;

    public GameObject cableContainer;
    public LineRenderer activeCable;
    public List<LineRenderer> allCables = new List<LineRenderer>();

    public CableConnector cableConnector;

    public GameObject firstPoint;
    public GameObject lastPoint;
    
    public float desiredTime;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        lastPosition = boatAttachPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        FollowBoat();
        
        //if (Input.GetKeyDown(KeyCode.Mouse0)) StartNewCable();
        //if (Input.GetKeyDown(KeyCode.Mouse1)) StopCable();
        
    }

    private void LateUpdate()
    {
        PlaceCablePoint();
    }

    void PlaceCablePoint()
    {
        if (activeCable == null) return;

        timer += Time.deltaTime;
        
        if (timer > desiredTime)
        {
            var currentPosition = new Vector3(boatAttachPoint.position.x,0,boatAttachPoint.position.z);
            var lastVector = new Vector3(lastPosition.x, 0, lastPosition.z);
            
            var distance = Vector3.Distance(lastVector, currentPosition);
            if (distance > 0.5f)
            {
                activeCable.positionCount++;
                activeCable.SetPosition(activeCable.positionCount -3,cablePlacementPoint.position);
                lastPosition = currentPosition;
            }
            
            timer = 0;
        }
    }

    public void StartNewCable()
    {
        if (activeCable == null)
        { 
            lastPosition = boatAttachPoint.position;
            GameObject startCable = Instantiate(cablePrefab, cableContainer.transform);
            
            activeCable = startCable.GetComponent<LineRenderer>();
            var closetConnector = cableConnector.CheckCollision();
            if (closetConnector != null)
            {
                activeCable.SetPosition(0,closetConnector.transform.position);
                firstPoint = closetConnector;
            }
            else
            {
                activeCable.SetPosition(0,cablePlacementPoint.position);
                PlaceBuoy(0);
            }
        }
    }

    public void PlaceBuoy(int i)
    {
        var newBuoy1 = Instantiate(buoyPrefab, cableContainer.transform);
        newBuoy1.transform.position = cablePlacementPoint.position + new Vector3(0,0.5f,0f);
        
        switch (i)
        {
            //Start Cable
            case 0:
                firstPoint = newBuoy1;
                break;
            //Stop Cable
            case 1:
                break;
            default:
                return;
        }
        
    }

    public void StopCable()
    {
        if (activeCable == null) return;
        
        var closetConnector = cableConnector.CheckCollision();
        if (closetConnector != null && closetConnector.transform.parent.gameObject != firstPoint)
        {
            activeCable.SetPosition(activeCable.positionCount-1,closetConnector.transform.position);
        }
        else
        {
            activeCable.SetPosition(activeCable.positionCount-1,cablePlacementPoint.position);
            activeCable.Simplify(0.25f);
            PlaceBuoy(1);
        }
        //activeCable.BakeMesh(new Mesh());
        //activeCable.gameObject.SetActive(false);
        allCables.Add(activeCable);
        activeCable = null;
        firstPoint = null;
        timer = 0;
    }

    private void FollowBoat()
    {
        if (activeCable == null) return;
        
        activeCable.SetPosition(activeCable.positionCount-1, boatAttachPoint.position);
        activeCable.SetPosition(activeCable.positionCount-2, cablePlacementPoint.position);
        
    }
}
