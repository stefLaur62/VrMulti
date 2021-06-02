using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class UpdateGrabbedObject : NetworkBehaviour
{
    [SyncVar]
    private GameObject grabbedObject;
    [SerializeField]
    private OVRGrabber oVRGrabberLeftHand;
    [SerializeField]
    private OVRGrabber oVRGrabberRightHand;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.isLocalPlayer)
        {
            /*if (oVRGrabberLeftHand.grabbedObject != null)
            {
                CmdGrabObject(oVRGrabberLeftHand.grabbedObject.gameObject, oVRGrabberLeftHand.grabbedObject.GetComponent<NetworkIdentity>().netId);
            }*/
            if (oVRGrabberRightHand.grabbedObject != null)
            {
                CmdGrabObject(oVRGrabberRightHand.grabbedObject.gameObject);
            }
        }
    }

    [Command]
    public void CmdGrabObject(GameObject grabbed)
    {
        //Network
    }
}


/*
 * using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class GiveAuthority : NetworkBehaviour
{
    private OVRGrabbable oVRGrabbable;
    private Transform myTransform;
    private NetworkIdentity networkIdentity;

    [SerializeField] float lerpRate = 0.1f;
    [SyncVar] 
    private Vector3 syncPos;

    private Vector3 lastPos;
    private float threshold = 0.1f;
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
        LerpPosition();
    }
    [Command(requiresAuthority = false)]
    void CmdUpdatePos(Vector3 pos)
    {
        Debug.Log("cmd pos:" + pos);
        syncPos = pos;
        //LerpPosition();
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

    //[ClientRpc]
    void LerpPosition()
    {
        if (!oVRGrabbable.isGrabbed)
        {
            Debug.Log("SyncPos" + syncPos);
            Debug.Log("Pos" + myTransform.position);
            Debug.Log("Lerp:"+Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate));
            myTransform.position = syncPos;
        }
    }
}
*/

/*
    private OVRGrabbable oVRGrabbable;
    private Transform myTransform;
    [SerializeField] float lerpRate = 5;
    [SyncVar] private Vector3 syncPos;

    private Vector3 lastPos;
    private float threshold = 0.5f;


    void Start()
    {
        oVRGrabbable = GetComponent<OVRGrabbable>();
        myTransform = GetComponent<Transform>();
        syncPos = GetComponent<Transform>().position;
    }


    void FixedUpdate()
    {
        TransmitPosition();
        LerpPosition();
    }

    void LerpPosition()
    {
        if (!oVRGrabbable.isGrabbed)
        {
            myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);
        }
    }

    [Command(requiresAuthority = false)]
    void Cmd_ProvidePositionToServer(Vector3 pos)
    {
        syncPos = pos;
    }

    [ClientCallback]
    void TransmitPosition()
    {
        if (oVRGrabbable.isGrabbed && Vector3.Distance(myTransform.position, lastPos) > threshold)
        {
            Cmd_ProvidePositionToServer(myTransform.position);
            lastPos = myTransform.position;
        }
    }
*/