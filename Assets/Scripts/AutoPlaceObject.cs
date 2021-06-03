using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPlaceObject : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToMove;
    private bool objectInRange = false;
    private OVRGrabbable objOVRGrabbable;
    [SerializeField]
    private Transform modelTransform;
    void Start()
    {
        if(objectToMove!=null)
            objOVRGrabbable = objectToMove.GetComponent<OVRGrabbable>();

    }

    void Update()
    {
        PlaceObject();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == objectToMove)
        {
            objectInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == objectToMove)
        {
            objectInRange = false;
        }
    }

    //Place object when in the right area
    private void PlaceObject()
    {
        if (objectInRange && objOVRGrabbable != null && !objOVRGrabbable.isGrabbed)
        {
            objectToMove.transform.SetPositionAndRotation(modelTransform.position, modelTransform.rotation);
            DisableObjectGrab();
        }
    }

    //Disable grab on Object
    private void DisableObjectGrab()
    {
        if (objOVRGrabbable != null && objOVRGrabbable.grabbedBy == null)
        {
            Destroy(objOVRGrabbable);
            Destroy(objectToMove.GetComponent<Rigidbody>());
            Destroy(this);
            this.enabled = false;
        }

    }
}
