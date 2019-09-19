using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("Unit Stats")]
    protected readonly int StartHealth;
    protected int Health;
    protected int damage;
    protected int range;
    protected int bulletSpeed;
    protected float fireRate;
    protected float fireTimer = 0.0f;
    protected readonly int teamNum;
    protected string ID;

    protected Transform Target;
    protected string TID;
    protected float movespeed;
    protected float turnSpeed;
    protected int Counter = 0;
    protected bool enemy = false;

    protected bool allowfire = true;
    protected bool TargetAquired = false;
    protected Rigidbody Me;
    protected SphereCollider rangeCollider;

    [Header("Unit Elements")]
    public Image HealthBar;
    public Transform Projectile;
}
