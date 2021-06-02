using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class GiveAuthority : NetworkBehaviour
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
    }

    void FixedUpdate()
    {
        SendPos();
    }

    [Command(requiresAuthority = false)]
    void CmdUpdatePos(Vector3 pos)
    {
        Debug.Log("cmd pos:" + pos);
        syncPos = pos;
        LerpPosition();
    }
    [Command(requiresAuthority = false)]
    void CmdUpdateRotation(Quaternion rotation)
    {
        syncRot = rotation;
        LerpRotation();
    }
    [ClientCallback]
    void SendPos()
    {
        if (oVRGrabbable.isGrabbed)
        {
            if (Vector3.Distance(myTransform.position, lastPos) > threshold)
            {
                Debug.Log("pos send:" + myTransform.position);
                CmdUpdatePos(myTransform.position);
                lastPos = myTransform.position;
            }
            if (Quaternion.Angle(myTransform.rotation, lastRotation) > threshold)
            {
                Debug.Log("pos send:" + myTransform.position);
                CmdUpdateRotation(myTransform.rotation);
                lastRotation = myTransform.rotation;
            }
        }
    }

    [ClientRpc]
    void LerpPosition()
    {
        if (!oVRGrabbable.isGrabbed)
        {
            myTransform.position = syncPos;
        }
    }

    [ClientRpc]
    void LerpRotation()
    {
        if (!oVRGrabbable.isGrabbed)
        {
            myTransform.rotation = syncRot;
        }
    }
}