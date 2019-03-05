using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitProps : MonoBehaviour
{
    [Header("Unit Stats")]
    public int StartHealth;
    private int Health;
    public int damage;
    public int range;

    private Vector3 mPos;
    private Vector3 direction;
    public float movespeed;

    private Rigidbody Me;
    private SphereCollider rangeCollider;

    [Header("Unit Elements")]
    public Image HealthBar;
    public Transform thisUnit; 

    void Start()
    {
        Health = StartHealth;
        Me = GetComponent<Rigidbody>();
        rangeCollider = thisUnit.GetChild(1).GetComponent<SphereCollider>();
        rangeCollider.radius = range;
    }

    void Update()
    {
        CheckIfMoving();
    }

    void Damaged()
    {
        Health -= 1;
        HealthBar.fillAmount = (float)Health / StartHealth;
    }

    void ChangePos()
    {

        direction = (mPos - transform.position).normalized;
        Me.velocity = new Vector3(direction.x * movespeed, direction.y * movespeed, 0);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Sphere")
        {
            Damaged();
        }
    }

    void Zero()
    {
        Me.velocity = Vector3.zero;
    }

    void CheckIfMoving()
    {
        if (Input.GetMouseButton(0))
        {
            mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ChangePos();
        }
        else if (Me.position.x <= mPos.x + 0.75 && Me.position.x >= mPos.x - 0.75 && Me.position.y <= mPos.y + 0.75 && Me.position.y >= mPos.y - 0.75)
        {
            Zero();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        {
            Debug.Log("Fire");
        }
    }

}

