using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnOVRCustom : MonoBehaviour
{
    [SerializeField]
    private float headStartHeight;
    private Vector3 headPos;

    //Replace Head height when player start so camera isn't higher than player
    void ResetHead()
    {
        headPos = transform.position;
        headPos.y = headStartHeight;
        transform.position = headPos;
    }

    void OnEnable()
    {
        ResetHead();
    }
}
