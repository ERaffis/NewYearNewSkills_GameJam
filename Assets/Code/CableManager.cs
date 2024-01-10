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
    public PowerCable activeCable;
    public List<PowerCable> allCables = new List<PowerCable>();

    public CableConnector cableConnector;
    
    public float desiredTime;
    public float timer;

    public event EventHandler OnCableConnection;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        lastPosition = boatAttachPoint.position;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) StartNewCable();
        if (Input.GetKeyDown(KeyCode.F)) StopCable();
    }
    private void LateUpdate()
    {
        PlaceCablePoint();
        FollowBoat();
    }

    
    public void StartNewCable()
    {
        if (activeCable == null)
        { 
            
            //Instantiates new cable and assigns its LineRenderer to the active cable
            GameObject startCable = Instantiate(cablePrefab, cableContainer.transform);
            
            //Sets the activeCable (currently placing) has the PowerCable of the GameOject
            activeCable = startCable.GetComponent<PowerCable>();
            
            //Checks for connectors close by to attach the first point of the cable
            var closetConnector = cableConnector.CheckCollision();
            
            //If it finds a connector (either a buoy or a land connector)
            if (closetConnector != null)
            {
                var connectionPort = closetConnector.transform.parent.gameObject.GetComponent<ConnectionPort>();
                
                //Sets the start connection point of the cable
                activeCable.AddStartConnection(connectionPort);
                
                //Adds the power cable to the connector
                connectionPort.Connect(activeCable); 
                
                //Sets the position of the start cable point
                activeCable.lineRenderer.SetPosition(0,closetConnector.transform.position);
                
                //Sets the power state of the cable
                activeCable.SetPowerState(connectionPort);
            }
            else
            {
                var connectionPort = PlaceBuoy();
                
                activeCable.lineRenderer.SetPosition(0,cablePlacementPoint.position - new Vector3(0,0.5f,0));
                
                //Sets the start connection point of the cable
                activeCable.AddStartConnection(connectionPort);
                
                //Adds the powercable to the connector
                connectionPort.Connect(activeCable);
                
                //Sets the power state of the cable
                activeCable.SetPowerState(connectionPort);
            }
            
            
             
        }
    }
    public void StopCable()
    {
        if (activeCable == null) return;
        
        //Resets the last position
        lastPosition = boatAttachPoint.position;
            
        
        //Checks for connectors close by to attach the end point of the cable
        var closetConnector = cableConnector.CheckCollision();
            
        //If it finds a connector (either a buoy or a land connector)
        if (closetConnector != null && closetConnector.transform.parent.gameObject != activeCable.firstConnection.gameObject)
        {
            var connectionPort = closetConnector.transform.parent.gameObject.GetComponent<ConnectionPort>();
                
            //Sets the end connection point of the cable
            activeCable.AddEndConnection(connectionPort);
            
            //Adds the power cable to the connector
            connectionPort.Connect(activeCable); 
            
            //Sets the position of the end cable point
            activeCable.lineRenderer.SetPosition(activeCable.lineRenderer.positionCount-1,closetConnector.transform.position); 
            
            //Sets the power state of the cable
            activeCable.SetPowerState(connectionPort);
            
        }
        else
        {
            var connectionPort = PlaceBuoy();
            
            //Sets the start connection point of the cable
            activeCable.AddEndConnection(connectionPort);
                
            //Adds the powercable to the connector
            connectionPort.Connect(activeCable); 
            
            //Sets the position of the end cable point
            activeCable.lineRenderer.SetPosition(activeCable.lineRenderer.positionCount-1,connectionPort.transform.position - new Vector3(0,1f,0));
            
            //Sets the power state of the cable
            activeCable.SetPowerState(connectionPort);
        }
        
        activeCable.firstConnection.CheckForPower(activeCable);
        activeCable.endConnection.CheckForPower(activeCable);
        
        activeCable.lineRenderer.Simplify(0.25f);
        
        allCables.Add(activeCable);
        activeCable = null;
        timer = 0;
        
        OnCableConnection?.Invoke(this, EventArgs.Empty);
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
                activeCable.lineRenderer.positionCount++;
                activeCable.lineRenderer.SetPosition(activeCable.lineRenderer.positionCount -3,cablePlacementPoint.position);
                lastPosition = currentPosition;
            }
            
            timer = 0;
        }
    }
    public ConnectionPort PlaceBuoy()
    {
        var newBuoy = Instantiate(buoyPrefab, cableContainer.transform);
        newBuoy.transform.position = cablePlacementPoint.position + new Vector3(0,0.5f,0f);
        
        return newBuoy.GetComponent<ConnectionPort>();
        
    }

    
    private void FollowBoat()
    {
        if (activeCable == null) return;
        
        activeCable.lineRenderer.SetPosition(activeCable.lineRenderer.positionCount-1, boatAttachPoint.position);
        activeCable.lineRenderer.SetPosition(activeCable.lineRenderer.positionCount-2, cablePlacementPoint.position);
        
    }
}
