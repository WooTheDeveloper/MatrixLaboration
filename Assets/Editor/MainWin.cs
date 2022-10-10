using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainWin : EditorWindow
{
    public Vector3 v3;
    public Vector3 v3Out;
    public Vector4 v4;
    public Matrix4x4 matrix1 = Matrix4x4.identity;
    public Matrix4x4 matrix2 = Matrix4x4.identity;
    public Matrix4x4 matrix3 = Matrix4x4.identity;
    
    [MenuItem("Tool/MatrixWin %T")]
    static void OpenWin()
    {
        var win = GetWindow<MainWin>();
        win.Show(true);
    }

    private void OnGUI()
    {
        GUIVector3();
        GUIVector4();
        GUIMatrix1();
        GUIMatrix2();
        GUIMatrix3();
        GUIVector3Out();
        GUIFunc1();
    }
    
    void GUIFunc1()
    {
        if (GUILayout.Button("euler to matrix ... v3 -> matrix1"))
        {
            var q = Quaternion.Euler(v3);
            matrix1 =  Matrix4x4.Rotate(q);
        }

        if (GUILayout.Button("mul(m2,m1) = m3"))
        {
            matrix3 = matrix2 * matrix1;
        }

        if (GUILayout.Button("inverse(m1) = m2,m3 = worldtoloacal"))
        {
            matrix1 = Camera.main.transform.localToWorldMatrix;
            matrix2 = matrix1.inverse;
            matrix3 = Camera.main.transform.worldToLocalMatrix;
        }

        if (GUILayout.Button("v3 -> rot with matrix1 -> v3 out"))
        {
            v3Out = matrix1.MultiplyVector(v3);
        }

        if (GUILayout.Button("v4 求商和求余 -> v4.z = v4.x/v4.y , v4.w = v4.x % v4.y"))
        {
            v4.z = (int) (v4.x / v4.y);
            v4.w = (v4.x % v4.y);
        }
    }

    void GUIVector3()
    {
        v3 = EditorGUILayout.Vector3Field("Vector3", v3);
    }
    
    void GUIVector3Out()
    {
        v3Out = EditorGUILayout.Vector3Field("Vector3Out", v3Out);
    }
    
    void GUIVector4()
    {
        v4 = EditorGUILayout.Vector4Field("Vector4", v4);
    }

    void GUIMatrix1()
    {
        var m0 = matrix1.GetRow(0);
        var m1 = matrix1.GetRow(1);
        var m2 = matrix1.GetRow(2);
        var m3 = matrix1.GetRow(3);
        
        EditorGUI.BeginChangeCheck();
        GUILayout.Label("Matrix_1");
        m0 = EditorGUILayout.Vector4Field("", m0);
        m1 = EditorGUILayout.Vector4Field("", m1);
        m2 = EditorGUILayout.Vector4Field("", m2);
        m3 = EditorGUILayout.Vector4Field("", m3);
        if (EditorGUI.EndChangeCheck())
        {
            matrix1.SetRow(0,m0);
            matrix1.SetRow(1,m1);
            matrix1.SetRow(2,m2);
            matrix1.SetRow(3,m3);
        }
    }

    void GUIMatrix2()
    {
        var m0 = matrix2.GetRow(0);
        var m1 = matrix2.GetRow(1);
        var m2 = matrix2.GetRow(2);
        var m3 = matrix2.GetRow(3);
        
        EditorGUI.BeginChangeCheck();
        GUILayout.Label("Matrix_2");
        m0 = EditorGUILayout.Vector4Field("", m0);
        m1 = EditorGUILayout.Vector4Field("", m1);
        m2 = EditorGUILayout.Vector4Field("", m2);
        m3 = EditorGUILayout.Vector4Field("", m3);
        if (EditorGUI.EndChangeCheck())
        {
            matrix2.SetRow(0,m0);
            matrix2.SetRow(1,m1);
            matrix2.SetRow(2,m2);
            matrix2.SetRow(3,m3);
        }
    }
    void GUIMatrix3()
    {
        var m0 = matrix3.GetRow(0);
        var m1 = matrix3.GetRow(1);
        var m2 = matrix3.GetRow(2);
        var m3 = matrix3.GetRow(3);
        
        EditorGUI.BeginChangeCheck();
        GUILayout.Label("Matrix_3");
        m0 = EditorGUILayout.Vector4Field("", m0);
        m1 = EditorGUILayout.Vector4Field("", m1);
        m2 = EditorGUILayout.Vector4Field("", m2);
        m3 = EditorGUILayout.Vector4Field("", m3);
        if (EditorGUI.EndChangeCheck())
        {
            matrix3.SetRow(0,m0);
            matrix3.SetRow(1,m1);
            matrix3.SetRow(2,m2);
            matrix3.SetRow(3,m3);
        }
    }

   
    
}
