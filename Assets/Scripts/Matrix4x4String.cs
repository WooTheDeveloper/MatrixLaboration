using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public struct Matrix4x4String
{
    public string name;
    public string m00;
    public string m01;
    public string m02;
    public string m03;
    public string m10;
    public string m11;
    public string m12;
    public string m13;
    public string m20;
    public string m21;
    public string m22;
    public string m23;
    public string m30;
    public string m31;
    public string m32;
    public string m33;

    public static Matrix4x4String toString(Matrix4x4 m)
    {
        Matrix4x4String result = new Matrix4x4String()
        {
            m00 = m.m00.ToString(),
            m01 = m.m00.ToString(),
            m02 = m.m00.ToString(),
            m03 = m.m00.ToString(),
            m10 = m.m00.ToString(),
            m11 = m.m00.ToString(),
            m12 = m.m00.ToString(),
            m13 = m.m00.ToString(),
            m20 = m.m00.ToString(),
            m21 = m.m00.ToString(),
            m22 = m.m00.ToString(),
            m23 = m.m00.ToString(),
            m30 = m.m30.ToString(),
            m31 = m.m31.ToString(),
            m32 = m.m32.ToString(),
            m33 = m.m33.ToString(),
        };
        return result;
    }

    public static Matrix4x4String Identity => new Matrix4x4String()
    {
        m00 = 1.ToString(),
        m01 = 0.ToString(),
        m02 = 0.ToString(),
        m03 = 0.ToString(),
        m10 = 0.ToString(),
        m11 = 1.ToString(),
        m12 = 0.ToString(),
        m13 = 0.ToString(),
        m20 = 0.ToString(),
        m21 = 0.ToString(),
        m22 = 1.ToString(),
        m23 = 0.ToString(),
        m30 = 0.ToString(),
        m31 = 0.ToString(),
        m32 = 0.ToString(),
        m33 = 1.ToString(),
    };
    
    public static Matrix4x4String Symbolic => new Matrix4x4String()
    {
        m00 = "m00",
        m01 = "m01",
        m02 = "m02",
        m03 = "m03",
        m10 = "m10",
        m11 = "m11",
        m12 = "m12",
        m13 = "m13",
        m20 = "m20",
        m21 = "m21",
        m22 = "m22",
        m23 = "m23",
        m30 = "m30",
        m31 = "m31",
        m32 = "m32",
        m33 = "m33",
    };
    
    public static Matrix4x4String mul(Matrix4x4String mLeft, Matrix4x4String mRight)
    {
        string m00 = mul(mLeft.m00, mRight.m00) + mul(mLeft.m01, mRight.m10) + mul(mLeft.m02, mRight.m20) + mul(mLeft.m03, mRight.m30);
        string m01 = mul(mLeft.m00, mRight.m01) + mul(mLeft.m01, mRight.m11) + mul(mLeft.m02, mRight.m21) + mul(mLeft.m03, mRight.m31);
        string m02 = mul(mLeft.m00, mRight.m02) + mul(mLeft.m01, mRight.m12) + mul(mLeft.m02, mRight.m22) + mul(mLeft.m03, mRight.m32);
        string m03 = mul(mLeft.m00, mRight.m03) + mul(mLeft.m01, mRight.m13) + mul(mLeft.m02, mRight.m23) + mul(mLeft.m03, mRight.m33);
        string m10 = mul(mLeft.m10, mRight.m00) + mul(mLeft.m11, mRight.m10) + mul(mLeft.m12, mRight.m20) + mul(mLeft.m13, mRight.m30);
        string m11 = mul(mLeft.m10, mRight.m01) + mul(mLeft.m11, mRight.m11) + mul(mLeft.m12, mRight.m21) + mul(mLeft.m13, mRight.m31);
        string m12 = mul(mLeft.m10, mRight.m02) + mul(mLeft.m11, mRight.m12) + mul(mLeft.m12, mRight.m22) + mul(mLeft.m13, mRight.m32);
        string m13 = mul(mLeft.m10, mRight.m03) + mul(mLeft.m11, mRight.m13) + mul(mLeft.m12, mRight.m23) + mul(mLeft.m13, mRight.m33);
        string m20 = mul(mLeft.m20, mRight.m00) + mul(mLeft.m21, mRight.m10) + mul(mLeft.m22, mRight.m20) + mul(mLeft.m23, mRight.m30);
        string m21 = mul(mLeft.m20, mRight.m01) + mul(mLeft.m21, mRight.m11) + mul(mLeft.m22, mRight.m21) + mul(mLeft.m23, mRight.m31);
        string m22 = mul(mLeft.m20, mRight.m02) + mul(mLeft.m21, mRight.m12) + mul(mLeft.m22, mRight.m22) + mul(mLeft.m23, mRight.m32);
        string m23 = mul(mLeft.m20, mRight.m03) + mul(mLeft.m21, mRight.m13) + mul(mLeft.m22, mRight.m23) + mul(mLeft.m23, mRight.m33);
        string m30 = mul(mLeft.m30, mRight.m00) + mul(mLeft.m31, mRight.m10) + mul(mLeft.m32, mRight.m20) + mul(mLeft.m33, mRight.m30);
        string m31 = mul(mLeft.m30, mRight.m01) + mul(mLeft.m31, mRight.m11) + mul(mLeft.m32, mRight.m21) + mul(mLeft.m33, mRight.m31);
        string m32 = mul(mLeft.m30, mRight.m02) + mul(mLeft.m31, mRight.m12) + mul(mLeft.m32, mRight.m22) + mul(mLeft.m33, mRight.m32);
        string m33 = mul(mLeft.m30, mRight.m03) + mul(mLeft.m31, mRight.m13) + mul(mLeft.m32, mRight.m23) + mul(mLeft.m33, mRight.m33);
        return new Matrix4x4String()
        {
            m00 = trimAddPrefix(m00),
            m01 = trimAddPrefix(m01),
            m02 = trimAddPrefix(m02),
            m03 = trimAddPrefix(m03),
            m10 = trimAddPrefix(m10),
            m11 = trimAddPrefix(m11),
            m12 = trimAddPrefix(m12),
            m13 = trimAddPrefix(m13),
            m20 = trimAddPrefix(m20),
            m21 = trimAddPrefix(m21),
            m22 = trimAddPrefix(m22),
            m23 = trimAddPrefix(m23),
            m30 = trimAddPrefix(m30),
            m31 = trimAddPrefix(m31),
            m32 = trimAddPrefix(m32),
            m33 = trimAddPrefix(m33),
        };

    }

    public static string mul( string L, string R)
    {
        L = bracketBeforeMul(L);
        R = bracketBeforeMul(R);
        
        if (L=="0" || R == "0")
        {
            return "";
        }

        if (L == "1" && R == "1")
        {
            return "+1";
        }

        if (L =="1")
        {
            return "+" + R; 
        }

        if (R == "1")
        {
            return "+" + L; 
        }

        return "+" + L + "*" + R; 
    }

    public static string trimAddPrefix(string orig)
    {
        while (orig.StartsWith("+"))
        {
            orig = orig.TrimStart(new []{'+'});
        }

        return orig;
    }

    public static string bracketBeforeMul(string orig)
    {
        orig = trimAddPrefix(orig);
        if (orig.Contains("+") || orig.Contains("-"))
        {
            if (!orig.StartsWith("(") || !orig.EndsWith(")") || Regex.Match(orig,@"[)](\s)*[+](\s)*[(]")!=null)
            {
                orig = "(" + orig + ")";
            }
        }
        return orig;
    }
    
    
}
