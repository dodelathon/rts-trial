using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private Vector3 mPos;
    private Rigidbody rb;
    private Vector3 direction;
    public float movespeed;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButton(0))
        {
            ChangePos();
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
        
	}

    void ChangePos()
    {
        Debug.Log(Input.mousePosition);
        mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mPos - transform.position).normalized;
        rb.velocity = new Vector3(direction.x * movespeed, direction.y * movespeed, 0);
    }
}
