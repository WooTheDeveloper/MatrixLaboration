using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnOrRowFirst : MonoBehaviour
{
    public Vector3 xAixs = new Vector3(0,0,-1);
    public Vector3 yAixs = new Vector3(0,1,0);
    public Vector3 zAixs = new Vector3(1,0,0);
    public float rot = 90f;

    private void Start()
    {
        var trs = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0, 90, 0), Vector3.one);
        Debug.Log(trs.ToString("0.000"));
        Debug.Log("m20 " + trs.m20);
        Debug.Log("m02 " + trs.m02);
    }
}
