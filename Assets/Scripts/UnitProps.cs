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
    public float fireRate;
    private float fireTimer = 0.0f;
    
    private Vector3 mPos;
    private Vector3 direction;
    private Transform Target;
    public float movespeed;
    public float turnSpeed;
    private short shiftbit = 0;

    bool allowfire = true;
    bool TargetAquired = false;
    private Rigidbody Me;
    private SphereCollider rangeCollider;

    [Header("Unit Elements")]
    public Image HealthBar;
    public Transform Projectile;
    public Transform FirePoint;

    void Start()
    {
        Health = StartHealth;
        Me = GetComponent<Rigidbody>();
        rangeCollider = transform.GetChild(2).GetComponent<SphereCollider>();
        rangeCollider.radius = range;
    }

    void FixedUpdate()
    {
        fireTimer -= Time.deltaTime;
        CheckIfMoving();
        FaceTarget();
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

    /*void CalcShorterRoute(Collision col)
    {
        if(mPos.z > col.transform.position.z)
        {
            if(mPos.x > col.transform.position.x)
            {
                float right = Mathf.Sqrt(Mathf.Pow(mPos.x - (transform.position.x + col.gameObject.GetComponent<SphereCollider>().radius), 2) + Mathf.Pow(mPos.z - transform.position.z, 2));
                float left = Mathf.Sqrt(Mathf.Pow(mPos.x - transform.position.x, 2) + Mathf.Pow(mPos.z - (transform.position.z + col.gameObject.GetComponent<SphereCollider>().radius), 2));
                Debug.Log("Left: " + left + " Right: " + right);
                if(left >= right)
                {
                    Me.velocity = new Vector3(-mPos.x * movespeed, 0, mPos.z * movespeed);
                    Me.velocity = new Vector3(mPos.x * movespeed, 0, mPos.z * movespeed);
                }
                else
                {
                   
                    Me.velocity = new Vector3(-mPos.x * movespeed, 0, -mPos.z * movespeed);
                    Me.velocity = new Vector3(mPos.x * movespeed, 0, mPos.z * movespeed);
                }
            }
        }
    }*/

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Sphere")
        {
            
            /*if (shiftbit == 0)
            {
                shiftbit = 1;
                CalcShorterRoute(col);
            }*/
            Damaged();
        }
    }

    private void OnCollisionExit(Collision col)
    {
        ChangePos();
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
            shiftbit = 0;
            Zero();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (TargetAquired == false)
        {
            TargetAquired = true;
            Target = other.GetComponentInParent<Transform>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Here");
        TargetAquired = false;
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.name);
        TargetAquired = true;
        Target = other.transform;
    }

    void Fire(Vector3 pos)
    {

        Vector3 FPPos = FirePoint.position;
        Transform tempBullet = Instantiate(Projectile, FPPos,  FirePoint.rotation);
        tempBullet.GetComponent<Rigidbody>().AddForce((pos.x - transform.position.x) * bulletSpeed, (pos.y - (transform.position.y + 0.20f)) * bulletSpeed, (pos.z - transform.position.z) * bulletSpeed);
    }

    private void FaceTarget()
    {
        Vector3 mDirect = (mPos - transform.position).normalized;
        mDirect.y = 0;

        if (TargetAquired == true)
        {
            direction = Target.position - Me.position;
            direction.Normalize();
            Vector3 tempDirect = direction;
            tempDirect.y -= 0.10f;
            direction.y = 0;
           
            transform.GetChild(0).rotation = Quaternion.Slerp(transform.GetChild(0).rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
            transform.GetChild(0).GetChild(1).rotation = Quaternion.Slerp(transform.GetChild(0).GetChild(1).rotation, Quaternion.LookRotation(tempDirect), (turnSpeed) * Time.deltaTime);


            transform.GetChild(1).rotation = Quaternion.Slerp(transform.GetChild(1).rotation, Quaternion.LookRotation(mDirect), turnSpeed * Time.deltaTime);


            int layerMask = ((1 << LayerMask.NameToLayer("units")) | (1 << LayerMask.NameToLayer("Buildings")));
            RaycastHit hit;
            Debug.DrawRay(FirePoint.position,  tempDirect, Color.red);
            if (Physics.Raycast(FirePoint.position, tempDirect, out hit, rangeCollider.radius/20, layerMask))
            {
                if (fireTimer < 0.0f)
                {
                   
                    fireTimer = fireRate;
                    Fire(Target.position);
                }
            }
        }
        else
        {
            direction = new Vector3(0,0,0);
            direction.Normalize();
            transform.GetChild(0).rotation = Quaternion.Slerp(transform.GetChild(0).rotation, Quaternion.LookRotation(direction), (turnSpeed) * Time.deltaTime);
            transform.GetChild(0).GetChild(1).rotation = Quaternion.Slerp(transform.GetChild(0).GetChild(1).rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);


            transform.GetChild(1).rotation = Quaternion.Slerp(transform.GetChild(1).rotation, Quaternion.LookRotation(mDirect), turnSpeed * Time.deltaTime);
        }
    }

}

