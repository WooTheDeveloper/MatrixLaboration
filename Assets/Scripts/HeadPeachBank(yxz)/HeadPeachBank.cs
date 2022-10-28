using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadPeachBank : MonoBehaviour
{
    public float headY = 45;
    public float peachX = 45;
    public float bankZ = 30;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            this.transform.rotation = Quaternion.identity;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Quaternion rotHead = Quaternion.Euler(new Vector3(0,headY,0));
            Quaternion rotPeach = Quaternion.Euler(new Vector3(peachX,0,0));
            Quaternion rotBank = Quaternion.Euler(new Vector3(0,0,bankZ));
            Quaternion rot = rotHead * rotPeach * rotBank;
            this.transform.rotation = rot;
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Quaternion rot = Quaternion.Euler(new Vector3(peachX,headY,bankZ));
            this.transform.rotation = rot;
        }

        
    }
}
