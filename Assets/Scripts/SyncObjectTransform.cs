using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class SyncObjectTransform : NetworkBehaviour
{
    private OVRGrabbable oVRGrabbable;
    private Transform myTransform;

    private float lerpRate = 30f;

    [SyncVar]
    private Vector3 syncPos;
    [SyncVar]
    private Quaternion syncRot;

    private Vector3 lastPos;
    private float threshold = 0.05f;
    private Quaternion lastRotation;

    void Start()
    {
        oVRGrabbable = GetComponent<OVRGrabbable>();
        myTransform = GetComponent<Transform>();
        syncPos = myTransform.position;
        syncRot = myTransform.rotation;

        syncInterval = 0.05f;
    }

    void FixedUpdate()
    {
        SendPosAndRotation();
    }

    //Change position of object on server
    [Command(requiresAuthority = false)]
    void CmdUpdatePos(Vector3 pos)
    {
        syncPos = pos;
        LerpPosition();
    }

    //Change rotation of object on server
    [Command(requiresAuthority = false)]
    void CmdUpdateRotation(Quaternion rotation)
    {
        syncRot = rotation;
        LerpRotation();
    }

    //If player grab the object then notify server object with new position and rotation
    [ClientCallback]
    void SendPosAndRotation()
    {
        if (oVRGrabbable.isGrabbed)
        {
            if (Vector3.Distance(myTransform.position, lastPos) > threshold)
            {
                CmdUpdatePos(myTransform.position);
                lastPos = myTransform.position;
            }
            if (Quaternion.Angle(myTransform.rotation, lastRotation) > threshold)
            {
                CmdUpdateRotation(myTransform.rotation);
                lastRotation = myTransform.rotation;
            }
        }
    }

    //Update position on client that doesn't grab the object
    [ClientRpc]
    void LerpPosition()
    {
        if (!oVRGrabbable.isGrabbed)
        {
            myTransform.position = syncPos;
        }
    }

    //Update rotation on client that doesn't grab the object
    [ClientRpc]
    void LerpRotation()
    {
        if (!oVRGrabbable.isGrabbed)
        {
            myTransform.rotation = syncRot;
        }
    }
}