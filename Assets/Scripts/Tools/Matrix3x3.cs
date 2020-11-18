using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix3x3 
{
    public Vector3[] v = new Vector3[3];

    public Matrix3x3(Vector3 v1,Vector3 v2,Vector3 v3)
    {
        v[0] = v1;
        v[1] = v2;
        v[2] = v3;
    }

    public static Vector3 operator*(Matrix3x3 mtr,Vector3 v)
    {
        float num1 = mtr.v[0].x * v.x+ mtr.v[0].y * v.y+ mtr.v[0].z * v.z;
        float num2 = mtr.v[1].x * v.x+ mtr.v[1].y * v.y+ mtr.v[1].z * v.z;
        float num3 = mtr.v[2].x * v.x+ mtr.v[2].y * v.y+ mtr.v[2].z * v.z;
        return new Vector3(num1, num2, num3);
    }
}
