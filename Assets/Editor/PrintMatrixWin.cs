using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PrintMatrixWin : EditorWindow
{
    public Matrix4x4String mA;
    public Matrix4x4String mB;
    public Matrix4x4String mC;
    public Matrix4x4String mResult;

    private void OnEnable()
    {
        ResetMatrices();
    }

    [MenuItem("Tool/StringMatrix")]
    static void OpenWin()
    {
        var win = EditorWindow.GetWindow<PrintMatrixWin>();
        win.Show();
    }

    private void OnGUI()
    {
        GUIMatrix4x4String(ref mA);
        GUIMatrix4x4String(ref mB);
        GUIMatrix4x4String(ref mC);
        GUIMatrix4x4String(ref mResult);
        if (GUILayout.Button("计算mC * mB * mA"))
        {
            mResult = Matrix4x4String.mul(mB, mA);
            mResult = Matrix4x4String.mul(mC,mResult);
        }
    }

    private void ResetMatrices()
    {
        mA = Matrix4x4String.Symbolic;
        mA.name = "mA";
        mB = Matrix4x4String.Identity;
        mB.name = "mB";
        mC = Matrix4x4String.Identity;
        mC.name = "mC";
        mResult = Matrix4x4String.Identity;
    }

    void GUIMatrix4x4String(ref Matrix4x4String m)
    {
        using (new GUILayout.VerticalScope())
        {
            GUILayout.Label(m.name + "------------------------------");
            GUIString4(ref m.m00,ref m.m01,ref m.m02 ,ref m.m03);
            GUIString4(ref m.m10,ref m.m11,ref m.m12 ,ref m.m13);
            GUIString4(ref m.m20,ref m.m21,ref m.m22 ,ref m.m23);
            GUIString4(ref m.m30,ref m.m31,ref m.m32 ,ref m.m33);
            GUILayout.Space(10);
        }
    }

    void GUIString4(ref string s0,ref string s1,ref string s2,ref string s3)
    {
        using (new GUILayout.HorizontalScope())
        {
            s0 = GUILayout.TextField(s0);
            s1 = GUILayout.TextField(s1);
            s2 = GUILayout.TextField(s2);
            s3 = GUILayout.TextField(s3);
        }
    }
}
