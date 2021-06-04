using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePlayer : MonoBehaviour
{

    private VRIK ik;

    private Vector3 defaultScale;
    void Awake()
    {
        ik = GetComponent<VRIK>();
        defaultScale = ik.references.root.localScale;
        ResetPos();
    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.GetDown(OVRInput.Button.One)){
            ResetPos();
        }
    }

    private void ResetPos()
    { 
        float sizeF = (ik.solver.spine.headTarget.position.y - ik.references.root.position.y) / (ik.references.head.position.y - ik.references.root.position.y);
        ik.references.root.localScale *= sizeF;
        //ik.references.root.localScale =  defaultScale * 0.62f;
    }
}
