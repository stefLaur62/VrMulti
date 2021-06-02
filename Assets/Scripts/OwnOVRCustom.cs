using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnOVRCustom : MonoBehaviour
{
    [SerializeField]
    private float headStartHeight;
    private Vector3 headPos;


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
