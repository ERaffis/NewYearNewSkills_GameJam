using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableConnector : MonoBehaviour
{
    public SphereCollider boatCheckSphere;


    public GameObject CheckCollision()
    {
        GameObject nearestCollider = null;
        float minSqrDistance = Mathf.Infinity;
        
        Collider[] hitColliders = Physics.OverlapSphere(boatCheckSphere.center, boatCheckSphere.radius,6);
        
        Debug.Log(hitColliders.Length);
        foreach (var t in hitColliders)
        {
            Debug.Log(t.name);
            float sqrDistanceToCenter = (boatCheckSphere.center - t.transform.position).sqrMagnitude;

            if (sqrDistanceToCenter < minSqrDistance)
            {
                minSqrDistance = sqrDistanceToCenter;
                nearestCollider = t.gameObject;
            }
        }

        return nearestCollider;
    }
}
