using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject thisBullet;
    private float targetTime = 2;

    void Start()
    {
        
    }

    void Update()
    {
        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            Destroy(thisBullet);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(thisBullet);
    }
}
