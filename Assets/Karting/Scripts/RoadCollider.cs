using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadCollider : MonoBehaviour
{
    public RoadColliderGroupManager GroupManager;
    void OnTriggerEnter(Collider car)
    {        
        if (GroupManager != null)
        {
            if (car.gameObject.tag == "Player" || car.gameObject.tag == "AI")
            {
                GroupManager.UpdateTargetState(gameObject, car.transform.parent.gameObject);
            }
        }
    }
}
