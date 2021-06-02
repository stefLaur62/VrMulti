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
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            Destroy(ovrCamRig);
            Destroy(this.GetComponentInChildren<RootMotion.FinalIK.VRIK>());
            Destroy(GetComponent<SimpleCapsuleWithStickMovement>());
        }
        else
        {
            if (leftEye.tag != "MainCamera") {
                leftEye.tag = "MainCamera";
                leftEye.enabled = true;
            }
            if (rightEye.tag != "MainCamera")
            {
                rightEye.tag = "MainCamera";
                rightEye.enabled = true;
            }

            //leftHand.localRotation = InputDevice.;
            /*leftHand.localRotation = InputTracking.GetLocalRotation(Node.LeftHand);
            rightHand.localRotation = InputTracking.GetLocalRotation(Node.RightHand);
            leftHand.localPosition = InputTracking.GetLocalPosition(Node.LeftHand);
            rightHand.localPosition = InputTracking.GetLocalPosition(Node.RightHand);*/


            /*//Handle movement
            Vector2 primaryAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

            if(primaryAxis.y > 0f)
            {
                pos += (primaryAxis.y * transform.forward * Time.deltaTime * speed);
            }
            if(primaryAxis.y < 0f)
            {
                pos += (Mathf.Abs(primaryAxis.y) * -transform.forward * Time.deltaTime * speed);
            }
            if (primaryAxis.x > 0f)
            {
                pos += (primaryAxis.x * transform.right * Time.deltaTime * speed);
            }
            if (primaryAxis.x < 0f)
            {
                pos += (Mathf.Abs(primaryAxis.x) * -transform.right * Time.deltaTime * speed);
            }

            transform.position = pos;

            //Rotation
            Vector3 euler = transform.rotation.eulerAngles;
            Vector2 secondaryAxis = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
            euler.y += secondaryAxis.x;
            transform.rotation = Quaternion.Euler(euler);
            transform.localRotation = Quaternion.Euler(euler);*/
        }
    }
}
