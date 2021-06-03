using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePlayer : MonoBehaviour
{

    private VRIK ik;

    void Awake()
    {
        ik = GetComponent<VRIK>();

        float sizeF = (ik.solver.spine.headTarget.position.y - ik.references.root.position.y) / (ik.references.head.position.y - ik.references.root.position.y);
        Debug.Log("Sizef:" + sizeF);
        ik.references.root.localScale *= (sizeF*0.6f);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
