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
    // Update is called once per frame
    void Update()
    {
        if (objectInRange && objOVRGrabbable != null && !objOVRGrabbable.isGrabbed)
        {
            objectToMove.transform.SetPositionAndRotation(modelTransform.position, modelTransform.rotation);
            //empecher le déplacement
        }
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
}
