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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowBoat();

        if (Input.GetKeyDown(KeyCode.Mouse0)) StartNewCable();
        if (Input.GetKeyDown(KeyCode.Mouse1)) StopCable();
        
    }
    void PlaceCablePoint()
    {
        if (activeCable == null) return;
        
        var currentPosition = new Vector3(boatAttachPoint.position.x,0,boatAttachPoint.position.z);
        var lastVector = new Vector3(lastPosition.x, 0, lastPosition.z);
        
        var distance = Vector3.Distance(lastVector, currentPosition);
        if (distance > 0.5f)
        {
            activeCable.positionCount++;
            activeCable.SetPosition(activeCable.positionCount -3,cablePlacementPoint.position);
            
        }
        lastPosition = currentPosition;
        
    }

    public void StartNewCable()
    {
        if (activeCable == null)
        { 
            lastPosition = boatAttachPoint.position;
            GameObject startCable = Instantiate(cablePrefab, cableContainer.transform);
            
            activeCable = startCable.GetComponent<LineRenderer>();
            var closetConnector = cableConnector.CheckCollision();
            Debug.Log(closetConnector);
            if (closetConnector != null)
            {
                activeCable.SetPosition(0,cableConnector.CheckCollision().transform.position - new Vector3(0,-0.5f,0));
            }
            else
            {
                activeCable.SetPosition(0,cablePlacementPoint.position);
                PlaceBuoy();
            }
            
            
            InvokeRepeating(nameof(PlaceCablePoint), 0f, 1f);  //1s delay, repeat every 1s
            
        }
    }

    public void PlaceBuoy()
    {
        var newBuoy1 = Instantiate(buoyPrefab, cableContainer.transform);
        newBuoy1.transform.position = cablePlacementPoint.position + new Vector3(0,0.5f,0f);
    }

    public void StopCable()
    {
        if (activeCable == null) return;
        
        CancelInvoke(nameof(PlaceCablePoint));
        
        var closetConnector = cableConnector.CheckCollision();
        Debug.Log(closetConnector);
        if (closetConnector != null)
        {
            activeCable.SetPosition(activeCable.positionCount-1,cableConnector.CheckCollision().transform.position- new Vector3(0,-0.5f,0));
        }
        else
        {
            activeCable.SetPosition(activeCable.positionCount-1,cablePlacementPoint.position);
            activeCable.Simplify(0.25f);
            PlaceBuoy();
        }
        //activeCable.BakeMesh(new Mesh());
        //activeCable.gameObject.SetActive(false);
        allCables.Add(activeCable);
        activeCable = null;
    }

    private void FollowBoat()
    {
        if (activeCable == null) return;
        
        activeCable.SetPosition(activeCable.positionCount-1, boatAttachPoint.position);
        activeCable.SetPosition(activeCable.positionCount-2, cablePlacementPoint.position);
        
    }
}
