using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempScrpt : MonoBehaviour
{
    private readonly int teamNum = 2;
    private bool enemy = false;
    private bool TargetAquired;
    private Transform Target;
    private string TID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (TargetAquired == false)
        {
            other.SendMessage("IsAlly", teamNum);
            if (enemy == true)
            {

                TargetAquired = true;
                Target = other.GetComponentInParent<Transform>();
                TID = other.name;
            }
        }
    }

    private void IsAlly(System.Object[] Temp)
    {
        if ((int)Temp[0] != teamNum)
        {
            enemy = true;
        }
        if ((int)Temp[2] == 0)
        {
            GameObject other = (GameObject)Temp[1];
            System.Object[] sender = { teamNum, gameObject, 1 };
            other.SendMessage("IsAlly", sender);
        }
    }
}
