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
    public int bulletSpeed;
    
    private Vector3 mPos;
    private Vector3 direction;
    private Transform Target;
    public float movespeed;
    public float turnSpeed;

    bool allowfire = true;
    bool TargetAquired = false;
    private Rigidbody Me;
    private SphereCollider rangeCollider;

    [Header("Unit Elements")]
    public Image HealthBar;
    public Transform thisUnit;
    public Transform Projectile;
    public Transform FirePoint;

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
        faceTarget();
    }

    void Damaged()
    {
        Health -= 1;
        HealthBar.fillAmount = (float)Health / StartHealth;
    }

    void ChangePos()
    {

        direction = (mPos - transform.position).normalized;
        Me.velocity = new Vector3(direction.x * movespeed, 0, direction.z * movespeed);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Sphere")
        {
            Damaged();
            ChangePos(col.transform.position);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Zero();
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
        else if (Me.position.x <= mPos.x + 0.25 && Me.position.x >= mPos.x - 0.25 && Me.position.z <= mPos.z + 0.25 && Me.position.z >= mPos.z - 0.25)
        {
            Zero();
        }
    }

    void ChangePos(Vector3 other)
    {
        direction = (other - transform.position).normalized;
        Me.velocity = new Vector3(-direction.x * 0.1f, 0, -direction.z * 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (TargetAquired == false)
        {
            TargetAquired = true;
            Target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        TargetAquired = false;
    }

    private void OnTriggerStay(Collider other)
    {
        Target = other.transform;
    }

    void Fire(Vector3 pos)
    {

        Vector3 FPPos = FirePoint.position;
        Transform tempBullet = Instantiate(Projectile, FPPos,  FirePoint.rotation);
        tempBullet.GetComponent<Rigidbody>().AddForce((pos.x - transform.position.x) * bulletSpeed, (pos.y - transform.position.y) * bulletSpeed, (pos.z - transform.position.z) * bulletSpeed);
    }

    void faceTarget()
    {
        if (TargetAquired == true)
        {
            direction = Target.position - Me.position;
            direction.Normalize();
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);

            int layerMask = ((1 << LayerMask.NameToLayer("units")) | (1 << LayerMask.NameToLayer("Buildings")));
    
            RaycastHit hit;


            if (Physics.Raycast(FirePoint.position, direction, out hit, rangeCollider.radius/10, layerMask))
            {
                Fire(Target.position);
            }



        }
        else
        {
            direction = new Vector3(0,0,0);
            direction.Normalize();
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), (turnSpeed) * Time.deltaTime);
        }
    }

   



}

