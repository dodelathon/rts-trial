using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUnit : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5.0f;
    public Vector3 offset;

    // Update is called once per frame
    void FixedUpdate()
    {


        transform.position = target.position;

        

    }
}
