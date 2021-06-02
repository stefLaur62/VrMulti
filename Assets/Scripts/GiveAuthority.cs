using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class GiveAuthority : NetworkBehaviour
{
    private OVRGrabbable oVRGrabbable;
    private Transform myTransform;
    private NetworkIdentity networkIdentity;

    [SerializeField] float lerpRate = 30f;
    [SyncVar]
    private Vector3 syncPos;

    private Vector3 lastPos;
    private float threshold = 0.3f;
    void Start()
    {
        oVRGrabbable = GetComponent<OVRGrabbable>();
        networkIdentity = GetComponent<NetworkIdentity>();
        myTransform = GetComponent<Transform>();
        syncPos = myTransform.position;
    }

    void FixedUpdate()
    {
        SendPos();
        //LerpPosition();
    }
    [Command(requiresAuthority = false)]
    void CmdUpdatePos(Vector3 pos)
    {
        Debug.Log("cmd pos:" + pos);
        syncPos = pos;
        LerpPosition();
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
        }
    }

    [ClientRpc]
    void LerpPosition()
    {
        if (!oVRGrabbable.isGrabbed)
        {
            Debug.Log("SyncPos" + syncPos);
            Debug.Log("Pos" + myTransform.position);
            Debug.Log("Lerp:" + Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate));
            myTransform.position = syncPos;
        }
    }
}