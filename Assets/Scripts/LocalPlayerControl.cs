using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using InputTracking = UnityEngine.XR.InputTracking;
using Node = UnityEngine.XR.XRNode;
using UnityEngine.XR;

public class LocalPlayerControl : NetworkBehaviour
{
    public GameObject ovrCamRig;
    public Transform leftHand;
    public Transform rightHand;
    public Camera leftEye;
    public Camera rightEye;
    public float speed;
    Vector3 pos;

    void Start()
    {
        pos = transform.position;
        if (!isLocalPlayer)
        {
            RemoveOtherPlayerComponent();
        }
        else
        {
            EnablePlayerCamera();
        }
    }

    //Remove player Component that are duplicated when a new player join
    private void RemoveOtherPlayerComponent()
    {
        Destroy(ovrCamRig);
        Destroy(this.GetComponentInChildren<RootMotion.FinalIK.VRIK>());
        Destroy(this.GetComponentInChildren<LocomotionController>().gameObject);
        Destroy(GetComponent<SimpleCapsuleWithStickMovement>());
    }

    //Assign right camera to player
    private void EnablePlayerCamera()
    {
        if (leftEye.tag != "MainCamera")
        {
            leftEye.tag = "MainCamera";
            leftEye.enabled = true;
        }
        if (rightEye.tag != "MainCamera")
        {
            rightEye.tag = "MainCamera";
            rightEye.enabled = true;
        }
    }
}
